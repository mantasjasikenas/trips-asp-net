using MySql.Data.MySqlClient;

namespace TripsAgency.Database;

public interface IDbContext
{
    MySqlConnection CreateConnection();
}