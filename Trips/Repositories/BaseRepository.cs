using System.Data;
using MySql.Data.MySqlClient;
using TripsAgency.Extensions;

namespace TripsAgency.Database;

public class BaseRepository
{
    protected readonly DbContext _dbContext;

    public BaseRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public void Insert(string tableName, Dictionary<string, string> data)
    {
        var columns = string.Join(", ", data.Keys);
        var values = string.Join(", ", data.Values.Select(x => $"\"{x}\""));

        var query = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";

        using var connection = _dbContext.CreateConnection();
        using var command = new MySqlCommand(query, connection);
        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Update(string tableName, Dictionary<string, string> data, string whereCondition)
    {
        var setValues = string.Join(", ", data.Select(x => $"{x.Key}=\"{x.Value}\""));
        var query = $"UPDATE {tableName} SET {setValues} WHERE {whereCondition}";

        using var connection = _dbContext.CreateConnection();
        using var command = new MySqlCommand(query, connection);
        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Delete(string tableName, string whereCondition)
    {
        var query = $"DELETE FROM {tableName} WHERE {whereCondition}";

        using var connection = _dbContext.CreateConnection();
        using var command = new MySqlCommand(query, connection);
        connection.Open();
        command.ExecuteNonQuery();
    }

    public DataTable Select(string tableName)
    {
        var query = $"SELECT * FROM {tableName}";

        using var connection = _dbContext.CreateConnection();
        using var command = new MySqlCommand(query, connection);
        using var adapter = new MySqlDataAdapter(command);

        var dataTable = new DataTable();
        adapter.Fill(dataTable);

        return dataTable;
    }


    public void ExecuteNonQuery(string query)
    {
        using var connection = _dbContext.CreateConnection();
        using var command = new MySqlCommand(query, connection);

        connection.Open();
        command.ExecuteNonQuery();
    }

    public ulong GetLastInsertId()
    {
        var query = """SELECT LAST_INSERT_ID() AS id LIMIT 1""";

        using var connection = _dbContext.CreateConnection();
        using var command = new MySqlCommand(query, connection);

        using var adapter = new MySqlDataAdapter(command);

        var dataTable = new DataTable();
        adapter.Fill(dataTable);

        var id = dataTable.Rows[0].Field<ulong>("id");

        return id;
    }
}