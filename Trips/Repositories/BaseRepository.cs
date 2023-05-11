using System.Data;
using MySql.Data.MySqlClient;

namespace TripsAgency.Database;

public class BaseRepository
{
    protected readonly IDbContext DbContext;

    protected BaseRepository(IDbContext dbContext)
    {
        DbContext = dbContext;
    }

    protected DataTable Select(string tableName)
    {
        var query = $"SELECT * FROM {tableName}";

        using var connection = DbContext.CreateConnection();
        using var command = new MySqlCommand(query, connection);
        using var adapter = new MySqlDataAdapter(command);

        var dataTable = new DataTable();
        adapter.Fill(dataTable);

        return dataTable;
    }

    protected void Delete(string tableName, string whereCondition)
    {
        var query = $"DELETE FROM {tableName} WHERE {whereCondition}";

        using var connection = DbContext.CreateConnection();
        using var command = new MySqlCommand(query, connection);
        connection.Open();
        command.ExecuteNonQuery();
    }

    protected void ExecuteNonQuery(string query)
    {
        using var connection = DbContext.CreateConnection();
        using var command = new MySqlCommand(query, connection);

        connection.Open();
        command.ExecuteNonQuery();
    }

    public ulong GetLastInsertId()
    {
        const string query = """SELECT LAST_INSERT_ID() AS id LIMIT 1""";

        using var connection = DbContext.CreateConnection();
        using var command = new MySqlCommand(query, connection);

        using var adapter = new MySqlDataAdapter(command);

        var dataTable = new DataTable();
        adapter.Fill(dataTable);

        var id = dataTable.Rows[0].Field<ulong>("id");

        return id;
    }
}