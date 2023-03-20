using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Trips.Controllers;
using Trips.Models;
using Trips.Models;

namespace Trips.Controllers;
public class AgentController : Controller
{
    private readonly MySqlConnection _mySqlConnection;
    private readonly ILogger<CustomerController> _logger;

    public AgentController(MySqlConnection mySqlConnection, ILogger<CustomerController> logger)
    {
        _mySqlConnection = mySqlConnection;
        _logger = logger;
    }


    [HttpGet]
    public ActionResult Index()
    {
        List<Agent> agents = new();

        try
        {
            _mySqlConnection.Open();
            var query =
                """ 
                SELECT
                    agents.id,
                    agents.first_name,
                    agents.last_name,
                    agents.phone,
                    agents.email,
                    travel_agencies.title AS agency_title
                FROM
                    agents
                LEFT JOIN travel_agencies ON agents.fk_travel_agency = travel_agencies.id
                """;

            using var command = new MySqlCommand(query, _mySqlConnection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                agents.Add(new Agent
                {
                    Id = reader.GetInt32("id"),
                    FirstName = reader.GetString("first_name"),
                    LastName = reader.GetString("last_name"),
                    PhoneNumber = reader.GetString("phone"),
                    Email = reader.GetString("email"),
                    FkTravelAgency = reader.GetString("agency_title"),
                });
            }
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
            _mySqlConnection.Open();
            var query =
                $$""" 
                SELECT
                    *
                FROM 
                    agents
                WHERE
                    id = {{id}}
                """;

            using var command = new MySqlCommand(query, _mySqlConnection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                agent = new Agent
                {
                    Id = reader.GetInt32("id"),
                    FirstName = reader.GetString("first_name"),
                    LastName = reader.GetString("last_name"),
                    PhoneNumber = reader.GetString("phone"),
                    Email = reader.GetString("email"),
                    FkTravelAgency = reader.GetString("fk_travel_agency"),
                };
            }
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
            _mySqlConnection.Open();
            var query =
                $$""" 
                SELECT
                    *
                FROM 
                    agents
                WHERE
                    id = {{id}}
                """;

            using var command = new MySqlCommand(query, _mySqlConnection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                agent = new Agent
                {
                    Id = reader.GetInt32("id"),
                    FirstName = reader.GetString("first_name"),
                    LastName = reader.GetString("last_name"),
                    PhoneNumber = reader.GetString("phone"),
                    Email = reader.GetString("email"),
                    FkTravelAgency = reader.GetString("fk_travel_agency"),
                };
            }
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
            //ModelisRepo.Delete(id);

            //deletion success, redired to list form
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
