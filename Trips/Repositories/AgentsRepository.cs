using System.Data;
using MySql.Data.MySqlClient;
using Trips.Models;
using TripsAgency.Database;
using TripsAgency.Extensions;

namespace TripsAgency.Repositories;

public class AgentsRepository : BaseRepository
{
    public AgentsRepository(IDbContext dbContext) : base(dbContext)
    {
    }

    public List<Agent> GetAgents()
    {
        const string query = """ 
                SELECT
                    agents.id,
                    agents.first_name,
                    agents.last_name,
                    agents.phone,
                    agents.email,
                    agents.fk_travel_agency AS fk_travel_agency_id,
                    travel_agencies.title AS fk_travel_agency_title 
                FROM
                    agents
                LEFT JOIN travel_agencies ON agents.fk_travel_agency = travel_agencies.id
                """;

        using var connection = DbContext.CreateConnection();
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
                    agents.fk_travel_agency AS fk_travel_agency_id,
                    travel_agencies.title AS fk_travel_agency_title
                FROM
                    agents
                LEFT JOIN travel_agencies ON agents.fk_travel_agency = travel_agencies.id
                WHERE
                    agents.id = {{id}}
                """;

        using var connection = DbContext.CreateConnection();
        using var command = new MySqlCommand(query, connection);
        using var adapter = new MySqlDataAdapter(command);

        var dataTable = new DataTable();
        adapter.Fill(dataTable);

        return dataTable.Rows[0].To<Agent>();
    }

    public void DeleteAgent(int id)
    {
        Delete("agents", $"agents.id = {id}");
    }

    public void UpdateAgent(int id, Agent agent)
    {
        var query =
            $"""
            UPDATE agents 
            SET 
                first_name = '{agent.FirstName}', 
                last_name = '{agent.LastName}', 
                phone = '{agent.Phone}', 
                email = '{agent.Email}',
                fk_travel_agency = '{agent.FkTravelAgencyId}'
            WHERE agents.id = {id}
            """;

        ExecuteNonQuery(query);
    }

    public void InsertAgent(Agent agent)
    {
        var query =
            $"""
            INSERT INTO agents (first_name, last_name, phone, email, fk_travel_agency)
            VALUES ('{agent.FirstName}', '{agent.LastName}', '{agent.Phone}', '{agent.Email}', '{agent.FkTravelAgencyId}')
            """;

        ExecuteNonQuery(query);
    }

    public IEnumerable<TravelAgency> GetTravelAgencies()
    {
        return Select("travel_agencies").ToList<TravelAgency>();
    }


    public int GetAgentId(Agent agent)
    {
        var query =
            $"""
            SELECT id 
            FROM agents 
            WHERE first_name = '{agent.FirstName}' AND last_name = '{agent.LastName}' AND phone = '{agent.Phone}' AND email = '{agent.Email}' AND fk_travel_agency = '{agent.FkTravelAgencyId}'
            """;

        using var connection = DbContext.CreateConnection();
        using var command = new MySqlCommand(query, connection);
        using var adapter = new MySqlDataAdapter(command);

        var dataTable = new DataTable();
        adapter.Fill(dataTable);

        return dataTable.Rows[0].Field<int>("id");
    }
}