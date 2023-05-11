using MySql.Data.MySqlClient;

namespace TripsAgency.Database;

public class LocalDbContext : IDbContext
{
    private readonly string _connectionString;
    private readonly ILogger<LocalDbContext> _logger;

    public LocalDbContext(IConfiguration configuration, ILogger<LocalDbContext> logger)
    {
        _logger = logger;
        _connectionString = configuration["Trips:DockerDbLocalConnection"]!;
        _logger.LogInformation("Local DB Context created");
    }

    public MySqlConnection CreateConnection()
    {
        return new MySqlConnection(_connectionString);
    }
}