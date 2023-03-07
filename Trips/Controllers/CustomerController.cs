using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Trips.Models;

namespace Trips.Controllers;

[Controller]
public class CustomerController : Controller
{
    private readonly MySqlConnection _mySqlConnection;
    private readonly ILogger<CustomerController> _logger;

    public CustomerController(MySqlConnection mySqlConnection, ILogger<CustomerController> logger)
    {
        _mySqlConnection = mySqlConnection;
        _logger = logger;
    }

    // GET: CustomerController
    [HttpGet]
    public ActionResult Index()
    {
        List<Customer> customers = new();

        try
        {
            _mySqlConnection.Open();
            var query = @"SELECT * FROM `customers`";
            using var command = new MySqlCommand(query, _mySqlConnection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                customers.Add(new Customer
                {
                    PersonalCode = reader.GetInt32("personal_id"),
                    FirstName = reader.GetString("first_name"),
                    LastName = reader.GetString("last_name"),
                    BirthDate = reader.GetDateTime("birth_date"),
                    PhoneNumber = reader.GetString("phone"),
                    Email = reader.GetString("email"),
                    Street = reader.GetString("street"),
                    Town = reader.GetString("town"),
                    PostalCode = reader.GetString("postcode"),
                });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }


        return View(customers);
    }

    /*// GET: CustomerController/Details/5
    public ActionResult Details(int id) {
        return View();
    }

    // GET: CustomerController/Create
    public ActionResult Create() {
        return View();
    }

    // POST: CustomerController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(IFormCollection collection) {
        try {
            return RedirectToAction(nameof(Index));
        }
        catch {
            return View();
        }
    }

    // GET: CustomerController/Edit/5
    public ActionResult Edit(int id) {
        return View();
    }

    // POST: CustomerController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection) {
        try {
            return RedirectToAction(nameof(Index));
        }
        catch {
            return View();
        }
    }

    // GET: CustomerController/Delete/5
    public ActionResult Delete(int id) {
        return View();
    }

    // POST: CustomerController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection) {
        try {
            return RedirectToAction(nameof(Index));
        }
        catch {
            return View();
        }
    }*/
}