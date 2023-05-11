using MySql.Data.MySqlClient;

namespace TripsAgency.Database;

public class AwsDbContext : IDbContext
{
    private readonly string _connectionString;
    private readonly ILogger<AwsDbContext> _logger;

    public AwsDbContext(IConfiguration configuration, ILogger<AwsDbContext> logger)
    {
        _logger = logger;
        _connectionString = configuration["Trips:AwsDbRemoteConnection"]!;
        _logger.LogInformation("AWS DB Context created");
    }

    public MySqlConnection CreateConnection()
    {
        return new MySqlConnection(_connectionString);
    }
}