using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Trips.Controllers;
using Trips.Models;

namespace TripsAgency.Controllers; 
public class TravelAgencyController : Controller {

    private readonly MySqlConnection _mySqlConnection;
    private readonly ILogger<CustomerController> _logger;

    public TravelAgencyController(MySqlConnection mySqlConnection, ILogger<CustomerController> logger) {
        _mySqlConnection = mySqlConnection;
        _logger = logger;
    }


    [HttpGet]
    public ActionResult Index() {
        List<TravelAgency> agencies = new();

        try {
            _mySqlConnection.Open();
            var query =
                """ 
                SELECT *
                FROM travel_agencies
                """;

            using var command = new MySqlCommand(query, _mySqlConnection);
            using var reader = command.ExecuteReader();

            while (reader.Read()) {
                agencies.Add(new TravelAgency {
                    Id = reader.GetInt32("id"),
                    Title = reader.GetString("title"),
                    PhoneNumber = reader.GetString("phone"),
                    Manager = reader.GetString("manager"),
                    Street = reader.GetString("street"),
                    Town = reader.GetString("town"),
                    PostCode = reader.GetString("postcode"),
                    Country = reader.GetString("country"),
                });
            }
        }
        catch (Exception ex) {
            _logger.LogError(ex.Message);
        }


        return View(agencies);
    }

}
