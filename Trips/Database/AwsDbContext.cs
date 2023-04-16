using MySql.Data.MySqlClient;

namespace TripsAgency.Database;

public class AwsDbContext : IDbContext
{
    private readonly ILogger<AwsDbContext> _logger;
    private readonly string _connectionString;

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