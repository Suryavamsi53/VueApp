using System.Collections.Generic;
using System.Data.SqlClient;
using WcfService.Models;

public class DataService : IDataService
{
    private readonly string _connectionString = "Your Connection String Here";

    public List<Lookup> GetLookups()
    {
        var lookups = new List<Lookup>();
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("SELECT * FROM Lookups", connection);
            connection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                lookups.Add(new Lookup
                {
                    Lookup_Id = (int)reader["Lookup_Id"],
                    Lookup_Type = (string)reader["Lookup_Type"],
                    Lookup_Desc = (string)reader["Lookup_Desc"],
                    Is_Active = (string)reader["Is_Active"],
                    Created_By = (string)reader["Created_By"],
                    Created_Date = (DateTime)reader["Created_Date"]
                });
            }
        }
        return lookups;
    }

    public void AddLookup(Lookup lookup)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("INSERT INTO Lookups (Lookup_Id, Lookup_Type, Lookup_Desc, Is_Active, Created_By, Created_Date) VALUES (@Lookup_Id, @Lookup_Type, @Lookup_Desc, @Is_Active, @Created_By, @Created_Date)", connection);
            command.Parameters.AddWithValue("@Lookup_Id", lookup.Lookup_Id);
            command.Parameters.AddWithValue("@Lookup_Type", lookup.Lookup_Type);
            command.Parameters.AddWithValue("@Lookup_Desc", lookup.Lookup_Desc);
            command.Parameters.AddWithValue("@Is_Active", lookup.Is_Active);
            command.Parameters.AddWithValue("@Created_By", lookup.Created_By);
            command.Parameters.AddWithValue("@Created_Date", lookup.Created_Date);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public void UpdateLookup(Lookup lookup)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("UPDATE Lookups SET Lookup_Type = @Lookup_Type, Lookup_Desc = @Lookup_Desc, Is_Active = @Is_Active WHERE Lookup_Id = @Lookup_Id", connection);
            command.Parameters.AddWithValue("@Lookup_Id", lookup.Lookup_Id);
            command.Parameters.AddWithValue("@Lookup_Type", lookup.Lookup_Type);
            command.Parameters.AddWithValue("@Lookup_Desc", lookup.Lookup_Desc);
            command.Parameters.AddWithValue("@Is_Active", lookup.Is_Active);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public void DeleteLookup(int lookupId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("DELETE FROM Lookups WHERE Lookup_Id = @Lookup_Id", connection);
            command.Parameters.AddWithValue("@Lookup_Id", lookupId);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}