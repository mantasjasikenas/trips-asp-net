using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Trips.Models;
using TripsAgency.Repositories;

namespace TripsAgency.Controllers;

public class AgentOrdersController : Controller
{
    private readonly AgentOrdersRepository _agentOrdersRepository;
    private readonly AgentsRepository _agentsRepository;
    private readonly ILogger<AgentOrdersController> _logger;

    public AgentOrdersController(AgentOrdersRepository agentOrdersRepository,
        AgentsRepository agentsRepository,
        ILogger<AgentOrdersController> logger)
    {
        _agentOrdersRepository = agentOrdersRepository;
        _agentsRepository = agentsRepository;
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
            Orders = new List<OrderE>
            {
                new()
            },
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

        _agentOrdersRepository.InsertAgentOrders(agentOrdersE);

        return RedirectToAction("Index", new {id = agentOrdersE.Agent.Id});
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
        return RedirectToAction("Edit", new {id}); //View(agentOrders);
    }

    [HttpPost]
    [Route("AgentOrders/Edit/{id:int}/AddNewOrder")]
    public ActionResult AddNewOrder(int id, AgentOrdersE agentOrdersE, string returnTo)
    {
        agentOrdersE.Orders ??= new List<OrderE>();

        agentOrdersE.Orders.Add(new OrderE
        {
            FkAgentId = agentOrdersE.Agent.Id
        });
        PopulateSelections();

        return View(returnTo, agentOrdersE); //"Edit"
    }

    [HttpPost]
    [Route("AgentOrders/Edit/{id:int}/DeleteOrder/{index:int}")]
    public ActionResult DeleteOrder(int id, int index, AgentOrdersE agentOrdersE, string returnTo)
    {
        agentOrdersE.Orders.RemoveAt(index);
        PopulateSelections();

        return View(returnTo, agentOrdersE); //"Edit"
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

    /*
    [HttpGet]
    public ActionResult Create()
    {
        var agent = new Agent();
        PopulateSelections(agent);

        return View(agent);
    }

    [HttpPost]
    public ActionResult Create(Agent agent)
    {
        if (!ModelState.IsValid)
        {
            PopulateSelections(agent);
            return View(agent);
        }

        _agentsRepository.InsertAgent(agent);

        //save success, go back to the entity list
        return RedirectToAction("Index");
    }

    [HttpGet]
    public ActionResult Delete(int id)
    {
        Agent agent = new();
        try
        {
            agent = _agentsRepository.GetAgent(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }

        return View(agent);
    }


    [HttpPost]
    public ActionResult DeleteConfirm(int id)
    {
        try
        {
            _agentsRepository.DeleteAgent(id);

            return RedirectToAction("Index");
        }
        catch (MySqlException)
        {
            ViewData["deletionNotPermitted"] = true;

            var agent = new Agent();

            return View("Delete", agent);
        }
    }

    private void PopulateSelections(Agent agent)
    {
        var travelAgencies = _agentsRepository.GetTravelAgencies();

        ViewData["travelAgencies"] =
            travelAgencies.Select(it => new SelectListItem
                          {
                              Value = it.Id.ToString(),
                              Text = $"{it.Id} - {it.Title}",
                              Selected = it.Id == agent.FkTravelAgencyId
                          })
                          .ToList();
    }*/
}