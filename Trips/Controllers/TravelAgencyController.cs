using Microsoft.AspNetCore.Mvc;
using Trips.Models;

namespace TripsAgency.Controllers;

public class TravelAgencyController : Controller
{
    private readonly ILogger<TravelAgencyController> _logger;

    public TravelAgencyController(ILogger<TravelAgencyController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet]
    public ActionResult Index()
    {
        List<TravelAgency> agencies = new();

        try
        {
            // Get the data from the database
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }


        return View(agencies);
    }
}