using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Trips.Models;
using TripsAgency.Database;
using TripsAgency.Repositories;

namespace Trips.Controllers;
public class AgentController : Controller
{
    private readonly AgentsRepository _agentsRepository;
    private readonly ILogger<CustomerController> _logger;

    public AgentController(AgentsRepository agentsRepository, ILogger<CustomerController> logger)
    {
        _agentsRepository = agentsRepository;
        _logger = logger;
    }


    [HttpGet]
    public ActionResult Index()
    {
        List<Agent> agents = new();

        try
        {
            agents = _agentsRepository.GetAgents();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }


        return View(agents);
    }

    [HttpGet]
    public ActionResult Edit(int id)
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
    public ActionResult Edit(int id, Agent agent)
    {
        if (ModelState.IsValid)
        {
            //ModelisRepo.Update(agent);
            //TODO Update repo

            //save success, go back to the entity list
            return RedirectToAction("Index");
        }

        //form field validation failed, go back to the form
        //PopulateSelections(agent);

        return View(agent);
    }

    [HttpGet]
    public ActionResult Create()
    {
        var agent = new Agent();

        return View(agent);
    }

    [HttpPost]
    public ActionResult Create(Agent agent)
    {
        if (ModelState.IsValid)
        {
            //ModelisRepo.Insert(agent);

            //save success, go back to the entity list
            return RedirectToAction("Index");
        }

        return View(agent);
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

}
