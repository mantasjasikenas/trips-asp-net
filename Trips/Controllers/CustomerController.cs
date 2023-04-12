using Microsoft.AspNetCore.Mvc;
using TripsAgency.Repositories;

namespace TripsAgency.Controllers;

[Controller]
public class CustomerController : Controller
{
    private readonly ILogger<CustomerController> _logger;
    private readonly OrdersRepository _ordersRepository;
    private readonly CustomersRepository _repository;

    public CustomerController(OrdersRepository ordersRepository, CustomersRepository repository,
        ILogger<CustomerController> logger)
    {
        _ordersRepository = ordersRepository;
        _repository = repository;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult Index()
    {
        var customers = _repository.GetCustomers();
        var orders = _ordersRepository.GetOrders();

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