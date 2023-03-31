using System.Data;
using MySql.Data.MySqlClient;
using Trips.Models;
using TripsAgency.Database;
using TripsAgency.Extensions;

namespace TripsAgency.Repositories;

public class OrdersRepository : BaseRepository
{
    private readonly AgentsRepository _agentsRepository;
    private readonly CustomersRepository _customersRepository;

    public OrdersRepository(DbContext dbContext, AgentsRepository agentsRepository,
        CustomersRepository customersRepository) : base(dbContext)
    {
        _agentsRepository = agentsRepository;
        _customersRepository = customersRepository;
    }

    public IEnumerable<Order> GetOrders()
    {
        return Select("orders").ToList<Order>();
    }

    public IEnumerable<OrderStatus> GetOrderStatuses()
    {
        return Select("order_status").ToList<OrderStatus>();
    }

    public IEnumerable<LuggageSize> GetLuggageSizes()
    {
        return Select("size").ToList<LuggageSize>();
    }

    public IEnumerable<Agent> GetAgents()
    {
        return _agentsRepository.GetAgents();
    }

    public IEnumerable<Customer> GetCustomers()
    {
        return _customersRepository.GetCustomers();
    }

    public List<Trip> GetTrips()
    {
        return Select("trips").ToList<Trip>();
    }
}