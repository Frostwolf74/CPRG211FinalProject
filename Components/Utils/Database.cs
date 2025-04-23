using MySqlConnector;

namespace CPRG211FinalProject;

public class Database
{
    private readonly MySqlConnectionStringBuilder builder = new()
    {
        Server = "localhost",
        Database = "CPRG211Final",
        UserID = "app_user",
        Password = "password"
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

    // Customer Methods

    public void AddCustomer(string firstName, string lastName, string email, string phoneNumber)
    {
        string query = $"INSERT INTO Customer (FirstName, LastName, Email, PhoneNumber) VALUES ('{firstName}', '{lastName}', '{email}', '{phoneNumber}')";
        ExecuteQuery(query);
    }

    public void UpdateCustomer(int id, string firstName, string lastName, string email, string phoneNumber)
    {
        string query = $"UPDATE Customer SET FirstName='{firstName}', LastName='{lastName}', Email='{email}', PhoneNumber='{phoneNumber}' WHERE Id={id}";
        ExecuteQuery(query);
    }

    public void RemoveCustomer(int id)
    {
        string query = $"DELETE FROM Customer WHERE Id={id}";
        ExecuteQuery(query);
    }

    // Membership Methods

    public void AddMembership(int id, string name, string type, int price)
    {
        string query = $"INSERT INTO Membership (Id, Name, Type, Price) VALUES ({id}, '{name}', '{type}', {price})";
        ExecuteQuery(query);
    }

    public void UpdateMembership(int id, string name, string type, int price)
    {
        string query = $"UPDATE Membership SET Name='{name}', Type='{type}', Price={price} WHERE Id={id}";
        ExecuteQuery(query);
    }

    public void RemoveMembership(int id)
    {
        string query = $"DELETE FROM Membership WHERE Id={id}";
        ExecuteQuery(query);
    }

    // Equipment Methods

    public void AddEquipment(string serialNumber, string productNumber, string description, string location)
    {
        string query = $"INSERT INTO Equipment (SerialNumber, ProductNumber, Description, Location) VALUES ('{serialNumber}', '{productNumber}', '{description}', '{location}')";
        ExecuteQuery(query);
    }

    public void UpdateEquipment(string serialNumber, string productNumber, string description, string location)
    {
        string query = $"UPDATE Equipment SET ProductNumber='{productNumber}', Description='{description}', Location='{location}' WHERE SerialNumber='{serialNumber}'";
        ExecuteQuery(query);
    }

    public void RemoveEquipment(string serialNumber)
    {
        string query = $"DELETE FROM Equipment WHERE SerialNumber='{serialNumber}'";
        ExecuteQuery(query);
    }

    // CustomerMembership Methods

    public void AddCustomerMembership(int customerId, int membershipId)
    {
        string query = $"INSERT INTO CustomerMembership (CustomerId, MembershipId) VALUES ({customerId}, {membershipId})";
        ExecuteQuery(query);
    }

    public void RemoveCustomerMembership(int customerId, int membershipId)
    {
        string query = $"DELETE FROM CustomerMembership WHERE CustomerId = {customerId} AND MembershipId = {membershipId}";
        ExecuteQuery(query);
    }

    public async Task<List<(string CustomerName, string MembershipName)>> GetCustomerMemberships()
    {
        var results = new List<(string, string)>();

        string query = @"
            SELECT CONCAT(c.FirstName, ' ', c.LastName) AS CustomerName,
                   m.Name AS MembershipName
            FROM CustomerMembership cm
            JOIN Customer c ON cm.CustomerId = c.Id
            JOIN Membership m ON cm.MembershipId = m.Id;
        ";

        using var connection = OpenConnection();
        using var command = new MySqlCommand(query, connection);
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            string customerName = reader.GetString("CustomerName");
            string membershipName = reader.GetString("MembershipName");
            results.Add((customerName, membershipName));
        }

        return results;
    }
}
