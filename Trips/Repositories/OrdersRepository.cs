using System.Globalization;
using Trips.Models;
using TripsAgency.Database;
using TripsAgency.Extensions;

namespace TripsAgency.Repositories;

public class OrdersRepository : BaseRepository
{
    public OrdersRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public IEnumerable<OrderE> GetOrders()
    {
        return Select("orders").ToList<OrderE>();
    }

    public void UpdateOrder(OrderE orderE)
    {
        var query =
            $"""
            UPDATE orders
            SET
                adults_count = '{orderE.AdultsCount}',
                children_count = '{orderE.ChildrenCount}',
                final_price = '{orderE.FinalPrice?.ToString("F", CultureInfo.InvariantCulture)}',
                status = '{orderE.StatusId}',
                luggage_size = '{orderE.LuggageSizeId}',
                fk_agent = '{orderE.FkAgentId}',
                fk_customer = '{orderE.FkCustomerId}',
                fk_trip = '{orderE.FkTripId}'
            WHERE
                id = '{orderE.Id}'
            """;

        ExecuteNonQuery(query);
    }

    public void InsertOrder(OrderE orderE)
    {
        var query =
            $"""
            INSERT INTO orders
            (
                adults_count,
                children_count,
                final_price,
                status,
                luggage_size,
                fk_agent,
                fk_customer,
                fk_trip
            )
            VALUES
            (
                '{orderE.AdultsCount}',
                '{orderE.ChildrenCount}',
                '{orderE.FinalPrice?.ToString("F", CultureInfo.InvariantCulture)}',
                '{orderE.StatusId}',
                '{orderE.LuggageSizeId}',
                '{orderE.FkAgentId}',
                '{orderE.FkCustomerId}',
                '{orderE.FkTripId}'
            )
            """;

        ExecuteNonQuery(query);
    }

    public void CascadeDeleteOrder(int orderId)
    {
        var query = $"""
            DELETE FROM payments
            WHERE fk_bill IN (
                SELECT bills.nr FROM bills WHERE fk_order = {orderId}
            );
            
            DELETE FROM bills WHERE fk_order = {orderId};

            DELETE FROM orders WHERE id = {orderId};
            """;

        ExecuteNonQuery(query);
    }

    public void CascadeDeleteAgentOrders(int agentId)
    {
        var query = $"""
            DELETE FROM payments
            WHERE fk_bill IN (
                SELECT bills.nr FROM bills WHERE fk_order IN (
                    SELECT orders.id FROM orders WHERE fk_agent = {agentId}
                )
            );
            
            DELETE FROM bills WHERE fk_order IN (
                SELECT orders.id FROM orders WHERE fk_agent = {agentId}
            );

            DELETE FROM orders WHERE fk_agent = {agentId};
            """;


        ExecuteNonQuery(query);
    }
}