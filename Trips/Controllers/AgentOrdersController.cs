using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using Trips.Models;
using TripsAgency.Repositories;

namespace TripsAgency.Controllers;

public class AgentOrdersController : Controller
{
    private readonly AgentOrdersRepository _agentOrdersRepository;
    private readonly AgentsRepository _agentsRepository;
    private readonly ILogger<AgentOrdersController> _logger;
    private readonly OrdersRepository _ordersRepository;

    public AgentOrdersController(AgentOrdersRepository agentOrdersRepository,
        AgentsRepository agentsRepository,
        OrdersRepository ordersRepository,
        ILogger<AgentOrdersController> logger)
    {
        _agentOrdersRepository = agentOrdersRepository;
        _agentsRepository = agentsRepository;
        _ordersRepository = ordersRepository;
        _logger = logger;
    }


    [HttpGet]
    [Route("AgentOrders/{id:int}")]
    public ActionResult Index(int id)
    {
        try
        {
            var agentOrders = _agentOrdersRepository.GetAgentOrders(id);

            return View(agentOrders);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }


        return View(new AgentOrdersL());
    }

    [HttpGet]
    [Route("AgentOrders/Create")]
    public ActionResult Create()
    {
        var agentOrders = new AgentOrdersE
        {
            Orders = new List<OrderE>(),
            Agent = new Agent()
        };
        PopulateSelections();

        return View(agentOrders);
    }

    [HttpPost]
    [Route("AgentOrders/Create")]
    public ActionResult Create(AgentOrdersE agentOrdersE)
    {
        agentOrdersE.Orders ??= new List<OrderE>();

        if (!ModelState.IsValid)
        {
            PopulateSelections();
            return View(agentOrdersE);
        }

        var newAgentid = _agentOrdersRepository.InsertAgentOrders(agentOrdersE);

        return RedirectToAction("Index", new {id = newAgentid});
    }

    [HttpGet]
    public ActionResult Edit(int id)
    {
        var agentOrders = _agentOrdersRepository.GetAgentOrdersEdit(id);
        PopulateSelections();

        return View(agentOrders);
    }

    [HttpPost]
    public ActionResult Edit(int id, AgentOrdersE agentOrdersE)
    {
        agentOrdersE.Orders ??= new List<OrderE>();

        if (!ModelState.IsValid)
        {
            PopulateSelections();
            return View(agentOrdersE);
        }

        _agentOrdersRepository.UpdateAgentOrders(agentOrdersE);

        return RedirectToAction("Index", new {id});
    }

    [HttpGet]
    public ActionResult ResetEdit(int id)
    {
        return RedirectToAction("Edit", new {id});
    }

    [HttpGet]
    public ActionResult CancelEdit(int id)
    {
        return RedirectToAction("Index", new {id});
    }

    [HttpGet]
    public ActionResult CancelCreate()
    {
        return RedirectToAction("Index", "Agent");
    }

    [HttpPost]
    [Route("AgentOrders/Edit/{id:int}/AddNewOrder")]
    public ActionResult AddNewOrderEdit(int id, AgentOrdersE agentOrdersE)
    {
        ModelState.Clear();

        agentOrdersE.Orders ??= new List<OrderE>();
        agentOrdersE.Orders.Add(new OrderE
        {
            FkAgentId = agentOrdersE.Agent.Id
        });


        PopulateSelections();

        return View("Edit", agentOrdersE);
    }

    [HttpPost]
    [Route("AgentOrders/Create/AddNewOrder")]
    public ActionResult AddNewOrderCreate(AgentOrdersE agentOrdersE)
    {
        ModelState.Clear();

        agentOrdersE.Orders ??= new List<OrderE>();
        agentOrdersE.Orders.Add(new OrderE
        {
            FkAgentId = agentOrdersE.Agent.Id
        });


        PopulateSelections();

        return View("Create", agentOrdersE);
    }

    [HttpPost]
    [Route("AgentOrders/Edit/{id:int}/DeleteOrder/{index:int}")]
    public ActionResult DeleteOrder(int id, int index, AgentOrdersE agentOrdersE, string returnTo)
    {
        agentOrdersE.Orders = agentOrdersE.Orders?.Where((_, i) => i != index).ToList() ??
                              new List<OrderE>();

        //agentOrdersE.Orders?.RemoveAt(index);
        PopulateSelections();
        ModelState.Clear();

        return View(returnTo, agentOrdersE);
    }

    [HttpGet]
    [Route("AgentOrders/{id:int}/DeleteOrders")]
    public ActionResult DeleteOrders(int id)
    {
        _ordersRepository.CascadeDeleteAgentOrders(id);

        return RedirectToAction("Index", new {id});
    }

    [HttpPost]
    public ActionResult DeleteConfirm(int id)
    {
        try
        {
            _ordersRepository.CascadeDeleteAgentOrders(id);
            return RedirectToAction("Index", new {id});
        }
        catch (MySqlException ex)
        {
            _logger.LogError(ex.Message);
            ViewData["deletionError"] = "Cannot delete agent with orders because of foreign key constraint.";

            var orders = _agentOrdersRepository.GetAgentOrders(id);

            return View("Delete", orders);
        }
    }

    [HttpGet]
    [Route("AgentOrders/Delete/{id:int}")]
    public ActionResult Delete(int id)
    {
        var orders = _agentOrdersRepository.GetAgentOrders(id);

        return View(orders);
    }


    private void PopulateSelections()
    {
        var statuses = _agentOrdersRepository.GetOrderStatuses();
        var luggageSizes = _agentOrdersRepository.GetLuggageSizes();
        var agents = _agentsRepository.GetAgents();
        var customers = _agentOrdersRepository.GetCustomers();
        var travelAgencies = _agentsRepository.GetTravelAgencies();


        ViewData["travelAgencies"] =
            travelAgencies.Select(it => new SelectListItem
            {
                Value = it.Id.ToString(),
                Text = $"{it.Id} - {it.Title}"
            });

        ViewData["agents"] =
            agents.Select(it => new SelectListItem
            {
                Value = it.Id.ToString(),
                Text = $"{it.Id} - {it.FirstName} {it.LastName}"
            });

        ViewData["customers"] =
            customers.Select(it => new SelectListItem
            {
                Value = it.PersonalCode.ToString(),
                Text = $"{it.PersonalCode} - {it.FirstName} {it.LastName}"
            });

        ViewData["statuses"] =
            statuses.Select(it => new SelectListItem
            {
                Value = it.Id.ToString(),
                Text = $"{it.Id} - {it.Status}"
            });

        ViewData["luggageSizes"] =
            luggageSizes.Select(it => new SelectListItem
            {
                Value = it.Id.ToString(),
                Text = $"{it.Id} - {it.Size}"
            });

        ViewData["trips"] =
            _agentOrdersRepository.GetTrips()
                                  .Select(it => new SelectListItem
                                  {
                                      Value = it.Nr.ToString(),
                                      Text = $"{it.Nr} - {it.Destination}"
                                  });
    }
}