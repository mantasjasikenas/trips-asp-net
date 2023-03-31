using Trips.Models;
using TripsAgency.Database;
using TripsAgency.Extensions;

namespace TripsAgency.Repositories;

public class CustomersRepository : BaseRepository
{
    public CustomersRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public List<Customer> GetCustomers()
    {
        return Select("customers").ToList<Customer>();
    }
}