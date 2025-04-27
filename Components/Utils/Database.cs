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

    public void ExecuteNonQuery(string query)
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

    public async Task<List<Dictionary<string, object?>>> ExecuteQueryWithResult(string query)
    {
        var results = new List<Dictionary<string, object?>>();

        using var connection = OpenConnection();
        using var command = new MySqlCommand(query, connection);
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var row = new Dictionary<string, object?>();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                row[reader.GetName(i).ToUpper()] = await reader.IsDBNullAsync(i) ? null : reader.GetValue(i);
            }

            results.Add(row);
        }

        return results;
    }

    public async Task<int> GetLastInsertedCustomerId()
    {
        using var connection = OpenConnection();
        using var command = new MySqlCommand("SELECT LAST_INSERT_ID();", connection);
        var result = await command.ExecuteScalarAsync();
        return Convert.ToInt32(result);
    }

    
    // Customer Methods
    

    public int AddCustomer(string firstName, string lastName, string email, string phoneNumber)
    {
        using var connection = OpenConnection();
        using var command = new MySqlCommand($@"
        INSERT INTO Customer (FirstName, LastName, Email, PhoneNumber)
        VALUES (@FirstName, @LastName, @Email, @PhoneNumber);
        SELECT LAST_INSERT_ID();
    ", connection);

        command.Parameters.AddWithValue("@FirstName", firstName);
        command.Parameters.AddWithValue("@LastName", lastName);
        command.Parameters.AddWithValue("@Email", email);
        command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

        try
        {
            var result = command.ExecuteScalar();
            return Convert.ToInt32(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while inserting customer: {ex.Message}");
            throw;
        }
    }



    public void UpdateCustomer(int id, string firstName, string lastName, string email, string phoneNumber)
    {
        string query = $"UPDATE Customer SET FirstName='{firstName}', LastName='{lastName}', Email='{email}', PhoneNumber='{phoneNumber}' WHERE Id={id}";
        ExecuteNonQuery(query);
    }

    public void RemoveCustomer(int id)
    {
        try
        {
            RemoveAllCustomerMemberships(id);
            string query = $"DELETE FROM Customer WHERE Id={id}";
            ExecuteNonQuery(query);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while removing customer: {ex.Message}");
            throw;
        }
    }

    
    // CustomerMembership Methods
    

    public void AddCustomerMembership(int customerId, int membershipId)
    {
        string query = $"INSERT INTO CustomerMembership (CustomerId, MembershipId) VALUES ({customerId}, {membershipId})";
        ExecuteNonQuery(query);
    }

    public void RemoveCustomerMembership(int customerId, int membershipId)
    {
        string query = $"DELETE FROM CustomerMembership WHERE CustomerId={customerId} AND MembershipId={membershipId}";
        ExecuteNonQuery(query);
    }

    public void RemoveAllCustomerMemberships(int customerId)
    {
        string query = $"DELETE FROM CustomerMembership WHERE CustomerId={customerId}";
        ExecuteNonQuery(query);
    }

    public async Task<List<(int CustomerId, int MembershipId)>> GetCustomerMembershipIds()
    {
        var results = new List<(int, int)>();
        string query = "SELECT CustomerId, MembershipId FROM CustomerMembership";

        var rows = await ExecuteQueryWithResult(query);

        foreach (var row in rows)
        {
            int customerId = Convert.ToInt32(row["CUSTOMERID"]);
            int membershipId = Convert.ToInt32(row["MEMBERSHIPID"]);
            results.Add((customerId, membershipId));
        }

        return results;
    }

    public async Task<int> GetMembershipIdByName(string name)
    {
        using var connection = OpenConnection();
        using var command = new MySqlCommand($@"
        SELECT Id FROM Memberships WHERE Name = @Name
    ", connection);

        command.Parameters.AddWithValue("@Name", name);

        try
        {
            var result = await command.ExecuteScalarAsync();
            return result != null ? Convert.ToInt32(result) : -1;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while getting membership id: {ex.Message}");
            throw;
        }
    }

    
    // Membership Methods
 
    public void AddMembership(int id, string name, string type, double price)
    {
        string query = $"INSERT INTO Memberships (Id, Name, Type, Price) VALUES ({id}, '{name}', '{type}', {price})";
        ExecuteNonQuery(query);
    }

    public void UpdateMembership(int id, string name, string type, double price)
    {
        string query = $"UPDATE Memberships SET Name='{name}', Type='{type}', Price={price} WHERE Id={id}";
        ExecuteNonQuery(query);
    }

    public void RemoveMembership(int id)
    {
        string query = $"DELETE FROM Memberships WHERE Id={id}";
        ExecuteNonQuery(query);
    }

    
    // Equipment Methods
    

    public void AddEquipment(string serialNumber, string productNumber, string description, string location)
    {
        string query = $"INSERT INTO Equipment (SerialNumber, ProductNumber, Description, Location) VALUES ('{serialNumber}', '{productNumber}', '{description}', '{location}')";
        ExecuteNonQuery(query);
    }

    public void UpdateEquipment(string serialNumber, string productNumber, string description, string location)
    {
        string query = $"UPDATE Equipment SET ProductNumber='{productNumber}', Description='{description}', Location='{location}' WHERE SerialNumber='{serialNumber}'";
        ExecuteNonQuery(query);
    }

    public void RemoveEquipment(string serialNumber)
    {
        string query = $"DELETE FROM Equipment WHERE SerialNumber='{serialNumber}'";
        ExecuteNonQuery(query);
    }
}
