using MySql.Data.MySqlClient;

namespace TripsAgency.Database;

public class DbContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public DbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration["Trips:AwsDbRemoteConnection"]!;
    }

    public MySqlConnection CreateConnection()
    {
        return new MySqlConnection(_connectionString);
    }
}