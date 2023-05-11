using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Trips.Models;
using TripsAgency.Repositories;

namespace TripsAgency.Controllers;

public class ReportsController : Controller
{
    private readonly ReportsRepository _reportsRepository;

    public ReportsController(ReportsRepository reportsRepository)
    {
        _reportsRepository = reportsRepository;
    }

    [HttpGet]
    [Route("Reports/Trips")]
    public IActionResult Trips(TripsReport tripsReport)
    {
        PopulateSelections();

        var tripType = tripsReport.TripType;
        var minPassengers = tripsReport.MinPassengers;
        var minOrders = tripsReport.MinOrders;
        var minPrice = tripsReport.MinPrice;
        
        try
        {
            var trips = _reportsRepository.GetTripReports(tripType, minPassengers, minOrders, minPrice);


            var report = new TripsReport
            {
                Trips = trips,
                TotalAdults = trips.Sum(it => it.TotalAdults),
                TotalChildren = trips.Sum(it => it.TotalChildren),
                TotalPrice = trips.Sum(it => it.TotalPrice),
                TotalOrders = trips.Sum(it => it.OrdersCount)
            };

            return View(report);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }


        return View(new TripsReport());
    }
    
    [HttpGet]
    public ActionResult ResetForm()
    {
        return RedirectToAction("Trips");
    }


    private void PopulateSelections()
    {
        var tripTypes = _reportsRepository.GetTripTypes();

        ViewData["trip_types"] =
            tripTypes
                .Select(it => new SelectListItem
                {
                    Value = it.Item2,
                    Text = $"{it.Item1} - {it.Item2}"
                });
    }
}