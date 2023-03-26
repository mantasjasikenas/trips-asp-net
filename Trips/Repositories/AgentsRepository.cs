using MySql.Data.MySqlClient;
using System.Data;
using Trips.Models;
using TripsAgency.Database;
using TripsAgency.Extensions;

namespace TripsAgency.Repositories;

public class AgentsRepository : BaseRepository
{
    public AgentsRepository(DbContext dbContext) : base(dbContext)
    {

    }

    public List<Agent> GetAgents()
    {
        var query =
                """ 
                SELECT
                    agents.id,
                    agents.first_name,
                    agents.last_name,
                    agents.phone,
                    agents.email,
                    travel_agencies.title AS fk_travel_agency 
                FROM
                    agents
                LEFT JOIN travel_agencies ON agents.fk_travel_agency = travel_agencies.id
                """;

        using var connection = _dbContext.CreateConnection();
        using var command = new MySqlCommand(query, connection);
        using var adapter = new MySqlDataAdapter(command);

        var dataTable = new DataTable();
        adapter.Fill(dataTable);

        return dataTable.ToList<Agent>();
    }

    public Agent GetAgent(int id)
    {
        var query =
                $$""" 
                SELECT
                    agents.id,
                    agents.first_name,
                    agents.last_name,
                    agents.phone,
                    agents.email,
                    travel_agencies.title AS fk_travel_agency
                FROM
                    agents
                LEFT JOIN travel_agencies ON agents.fk_travel_agency = travel_agencies.id
                WHERE
                    agents.id = {{id}}
                """;

        using var connection = _dbContext.CreateConnection();
        using var command = new MySqlCommand(query, connection);
        using var adapter = new MySqlDataAdapter(command);

        var dataTable = new DataTable();
        adapter.Fill(dataTable);

        return dataTable.Rows[0].To<Agent>();
    }

    public void DeleteAgent(int id)
    {
        base.Delete("agents", $"agents.id = {id}");
    }
}
