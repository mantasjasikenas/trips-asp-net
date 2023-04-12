using System.Data;
using System.Globalization;
using MySql.Data.MySqlClient;
using Trips.Models;
using TripsAgency.Database;
using TripsAgency.Extensions;

namespace TripsAgency.Repositories;

public class AgentOrdersRepository : BaseRepository
{
    private readonly AgentsRepository _agentsRepository;
    private readonly CustomersRepository _customersRepository;
    private readonly OrdersRepository _ordersRepository;

    public AgentOrdersRepository(DbContext dbContext, AgentsRepository agentsRepository,
        CustomersRepository customersRepository,
        OrdersRepository ordersRepository) : base(dbContext)
    {
        _agentsRepository = agentsRepository;
        _customersRepository = customersRepository;
        _ordersRepository = ordersRepository;
    }

    public AgentOrdersL GetAgentOrders(int id)
    {
        var agent = _agentsRepository.GetAgent(id);
        var orders = GetOrdersList(id) ?? new List<OrderL>();
        return new AgentOrdersL
        {
            Agent = agent,
            Orders = orders
        };
    }

    private List<OrderL> GetOrdersList(int agentId)
    {
        var query = $""" 
                SELECT
                    orders.id,
                    orders.adults_count,
                    orders.children_count,
                    orders.final_price,
                    order_status.name AS status,
                    size.name AS luggage_size,
                    CONCAT(agents.first_name, ' ', agents.last_name) AS fk_agent, 
                    CONCAT(customers.first_name, ' ', customers.last_name) AS fk_customer,
                    trips.destination AS fk_trip
                FROM
                    orders
                LEFT JOIN order_status ON orders.status = order_status.id 
                LEFT JOIN size ON orders.luggage_size = size.id
                LEFT JOIN trips ON orders.fk_trip = trips.nr
                LEFT JOIN agents ON orders.fk_agent = agents.id
                LEFT JOIN customers ON orders.fk_customer = customers.personal_id
                WHERE 
                    orders.fk_agent = {agentId}
                """;

        using var connection = _dbContext.CreateConnection();
        using var command = new MySqlCommand(query, connection);
        using var adapter = new MySqlDataAdapter(command);

        var dataTable = new DataTable();
        adapter.Fill(dataTable);

        return dataTable.ToList<OrderL>();
    }

    public AgentOrdersE GetAgentOrdersEdit(int id)
    {
        var agent = _agentsRepository.GetAgent(id);
        var orders = GetOrdersEdit(id) ?? new List<OrderE>();

        return new AgentOrdersE
        {
            Agent = agent,
            Orders = orders
        };
    }

    private List<OrderE> GetOrdersEdit(int agentId)
    {
        var query = $""" 
                SELECT
                    orders.id,
                    orders.adults_count,
                    orders.children_count,
                    orders.final_price,
                    order_status.id AS status,
                    size.id AS luggage_size,
                    agents.id AS fk_agent, 
                    customers.personal_id AS fk_customer,
                    trips.nr AS fk_trip
                FROM
                    orders
                LEFT JOIN order_status ON orders.status = order_status.id 
                LEFT JOIN size ON orders.luggage_size = size.id
                LEFT JOIN trips ON orders.fk_trip = trips.nr
                LEFT JOIN agents ON orders.fk_agent = agents.id
                LEFT JOIN customers ON orders.fk_customer = customers.personal_id
                WHERE 
                    orders.fk_agent = {agentId}
                """;

        using var connection = _dbContext.CreateConnection();
        using var command = new MySqlCommand(query, connection);
        using var adapter = new MySqlDataAdapter(command);

        var dataTable = new DataTable();
        adapter.Fill(dataTable);

        return dataTable.ToList<OrderE>();
    }

    public void UpdateAgentOrders(AgentOrdersE agentOrdersE)
    {
        var agent = agentOrdersE.Agent;
        var modifiedOrders = agentOrdersE.Orders;
        var initialOrders = GetOrdersEdit(agent.Id);
        var removedOrders = initialOrders
                            .Except(modifiedOrders, new OrderEComparer())
                            .ToList();

        _agentsRepository.UpdateAgent(agent.Id, agent);

        foreach (var order in modifiedOrders)
        {
            if (order.Id == 0)
                _ordersRepository.InsertOrder(order);
            else
                _ordersRepository.UpdateOrder(order);
        }

        foreach (var order in removedOrders)
        {
            _ordersRepository.DeleteOrder(order.Id);
        }
    }

    public void InsertAgentOrders(AgentOrdersE agentOrdersE)
    {
        var agent = agentOrdersE.Agent;
        var orders = agentOrdersE.Orders;

        _agentsRepository.InsertAgent(agent);
        
        var agentId = Convert.ToInt32(_agentsRepository.GetLastInsertId());

        foreach (var order in orders)
        {
            order.FkAgentId = agentId;
            _ordersRepository.InsertOrder(order);
        }
    }

    public IEnumerable<OrderStatus> GetOrderStatuses()
    {
        return Select("order_status").ToList<OrderStatus>();
    }

    public IEnumerable<LuggageSize> GetLuggageSizes()
    {
        return Select("size").ToList<LuggageSize>();
    }

    public IEnumerable<Customer> GetCustomers()
    {
        return _customersRepository.GetCustomers();
    }

    public IEnumerable<Trip> GetTrips()
    {
        return Select("trips").ToList<Trip>();
    }
}