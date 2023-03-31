using MySql.Data.MySqlClient;
using System.Data;

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
        var columns = String.Join(", ", data.Keys);
        var values = String.Join(", ", data.Values.Select(x => $"\"{x}\""));

        var query = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";

        using var connection = _dbContext.CreateConnection();
        using var command = new MySqlCommand(query, connection);
        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Update(string tableName, Dictionary<string, string> data, string whereCondition)
    {
        var setValues = String.Join(", ", data.Select(x => $"{x.Key}=\"{x.Value}\""));
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
}