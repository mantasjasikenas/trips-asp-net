﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using Trips.Models;
using TripsAgency.Repositories;

namespace Trips.Controllers;

public class AgentController : Controller
{
    private readonly AgentsRepository _agentsRepository;
    private readonly ILogger<AgentController> _logger;
    private readonly OrdersRepository _ordersRepository;

    public AgentController(AgentsRepository agentsRepository, OrdersRepository ordersRepository,
        ILogger<AgentController> logger)
    {
        _agentsRepository = agentsRepository;
        _ordersRepository = ordersRepository;
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
        var agent = _agentsRepository.GetAgent(id);
        PopulateSelections(agent);

        return View(agent);
    }

    [HttpPost]
    public ActionResult Edit(int id, Agent agent)
    {
        if (!ModelState.IsValid) return View(agent);

        _agentsRepository.UpdateAgent(id, agent);

        return RedirectToAction("Index");
    }

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
            _ordersRepository.CascadeDeleteAgentOrders(id);
            _agentsRepository.DeleteAgent(id);

            return RedirectToAction("Index");
        }
        catch (MySqlException ex)
        {
            _logger.LogError(ex.Message);
            ViewData["deletionError"] = "Cannot delete agent with orders because of foreign key constraint.";

            var agent = _agentsRepository.GetAgent(id);

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
    }
}