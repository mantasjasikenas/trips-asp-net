using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Trips.Controllers;
using Trips.Models;
using TripsAgency.Database;
using TripsAgency.Extensions;

namespace TripsAgency.Controllers;
public class TravelAgencyController : Controller
{
    private readonly ILogger<CustomerController> _logger;

    public TravelAgencyController(ILogger<CustomerController> logger)
    {
        _logger = logger;
    }


    [HttpGet]
    public ActionResult Index()
    {
        List<TravelAgency> agencies = new();

        try
        {
           /* agencies = _db
                    .Select("travel_agencies")
                    .ToList<TravelAgency>();*/
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }


        return View(agencies);
    }

}
