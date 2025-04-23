using MySqlConnector;

namespace CPRG211FinalProject;

public class Database
{
    private readonly MySqlConnectionStringBuilder builder = new()
    {
        Server = "localhost",
        Database = "demo1",
        UserID = "root",
        Password = "admin"
    };

    public MySqlConnection OpenConnection()
    {
        var connection = new MySqlConnection(builder.ConnectionString);
        try
        {
            connection.Open();
            return connection;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }

    public void CloseConnection(MySqlConnection connection)
    {
        try
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }

    public void ExecuteQuery(string query)
    {
        using var connection = OpenConnection();
        using var command = new MySqlCommand(query, connection);
        try
        {
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }

    public async Task<List<Dictionary<string, object>>> ExecuteQueryWithResult(string query)
    {
        var results = new List<Dictionary<string, object>>();

        using var connection = OpenConnection();
        using var command = new MySqlCommand(query, connection);
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var row = new Dictionary<string, object>();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                row[reader.GetName(i)] = await reader.IsDBNullAsync(i) ? null : reader.GetValue(i);
            }

            results.Add(row);
        }

        return results;
    }
}
