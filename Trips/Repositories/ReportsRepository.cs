using System.Data;
using MySql.Data.MySqlClient;
using Trips.Models;
using TripsAgency.Database;
using TripsAgency.Extensions;

namespace TripsAgency.Repositories;

public class ReportsRepository : BaseRepository
{
    public ReportsRepository(IDbContext dbContext) : base(dbContext)
    {
    }

    public IList<TripDetails> GetTripReports(string? tripType, int? minPassengers,
        int? minOrders,
        double? minPrice)
    {
        tripType = tripType is null ? "NULL" : $"'{tripType}'";
        minPassengers ??= 0;
        minOrders ??= 0;
        minPrice ??= 0;


        var query =
            $$"""
            SELECT CONCAT(ta.id, ' - ', ta.title)             AS travel_agency,
                   CONCAT(trips.nr, ' - ', trips.destination) AS trip,
                   tour_type.name                             AS trip_type,
                   DATEDIFF(trips.end_date, trips.start_date) AS trip_duration,
                   IFNULL(hotel_details.title, '-')           AS hotel_name,
                   IFNULL(ord.count, 0)                       AS orders_count,
                   IFNULL(ord.children, 0)                    AS total_children,
                   IFNULL(ord.adults, 0)                      AS total_adults,
                   IFNULL(ord.price, 0)                       AS total_price,
                   IFNULL(group_total.count, 0)               AS group_total_orders,
                   IFNULL(group_total.children, 0)            AS group_total_children,
                   IFNULL(group_total.adults, 0)              AS group_total_adults,
                   IFNULL(group_total.price, 0)               AS group_total_price
            FROM trips
                    # hotel details
                     LEFT JOIN
                         (SELECT hotels.title,
                                 hotels.id,
                                 hotels_services.fk_hotel
                          FROM hotels
                                   INNER JOIN hotels_services ON hotels.id = hotels_services.fk_hotel) AS hotel_details
                    ON trips.fk_hotel_service = hotel_details.id
                
                     # travel agency
                     LEFT JOIN travel_agencies ta on ta.id = trips.fk_travel_agency
                
                     # orders
                     LEFT JOIN (SELECT orders.fk_trip,
                                       COUNT(orders.id)           AS count,
                                       SUM(orders.children_count) AS children,
                                       SUM(orders.adults_count)   AS adults,
                                       SUM(orders.final_price)    AS price
                                FROM orders
                                GROUP BY orders.fk_trip) AS ord
                               ON trips.nr = ord.fk_trip
                     INNER JOIN tour_type ON trips.type = tour_type.id

                     LEFT JOIN (
                                SELECT t.fk_travel_agency         AS agency_id,
                                       t.nr                       AS trip_id,
                                       COUNT(orders.id)           AS count,
                                       SUM(orders.children_count) AS children,
                                       SUM(orders.adults_count)   AS adults,
                                       SUM(orders.final_price)    AS price
                                FROM orders
                                         INNER JOIN trips t on orders.fk_trip = t.nr
                                WHERE t.nr IN (SELECT orders.fk_trip
                                               FROM orders
                                               INNER JOIN trips tr ON orders.fk_trip = tr.nr
                                               INNER JOIN tour_type AS tt ON tr.type = tt.id
                                               GROUP BY orders.fk_trip, tt.name
                                               HAVING (SUM(orders.children_count) + SUM(orders.adults_count)) >= {{minPassengers}}
                                                  AND (COUNT(orders.id) >= {{minOrders}})
                                                  AND (SUM(orders.final_price) >= {{minPrice}})
                                                  AND (tt.name = IFNULL({{tripType}}, tt.name))
                                               )
                                GROUP BY t.fk_travel_agency) AS group_total
                        ON trips.fk_travel_agency = group_total.agency_id
            
            WHERE ((IFNULL(ord.children, 0) + IFNULL(ord.adults, 0)) >= {{minPassengers}})
              AND (IFNULL(ord.count, 0) >= {{minOrders}})
              AND (tour_type.name = IFNULL({{tripType}}, tour_type.name))
              AND (IFNULL(ord.price, 0) >= {{minPrice}})
            GROUP BY trips.nr
            ORDER BY travel_agency, trip;
            """;

        using var connection = DbContext.CreateConnection();
        using var command = new MySqlCommand(query, connection);
        using var adapter = new MySqlDataAdapter(command);

        var dataTable = new DataTable();
        adapter.Fill(dataTable);


        return dataTable.ToList<TripDetails>();
    }

    public IEnumerable<(string, string)> GetTripTypes()
    {
        var dataTable = Select("tour_type");

        var result = dataTable.Rows
                              .Cast<DataRow>()
                              .Select(row => (row[0].ToString() ?? string.Empty, row[1].ToString() ?? string.Empty))
                              .ToList();


        return result ?? new List<(string, string)>();
    }
}