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

        return View(customers);
    }
}