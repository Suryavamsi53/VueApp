using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class LookupDataAccess
{
    private string connectionString = "your_connection_string";

    public List<Lookup> GetLookups()
    {
        List<Lookup> lookups = new List<Lookup>();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Lookup_table", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Lookup lookup = new Lookup
                {
                    Lookup_Id = (int)reader["Lookup_Id"],
                    Lookup_Code = (int)reader["Lookup_Code"],
                    Lookup_Type = (string)reader["Lookup_Type"],
                    Lookup_Desc = (string)reader["Lookup_Desc"],
                    Is_Active = (string)reader["Is_Active"]
                };
                lookups.Add(lookup);
            }
        }
        return lookups;
    }

    public void AddLookup(Lookup lookup)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("INSERT INTO Lookup_table (Lookup_Code, Lookup_Type, Lookup_Desc, Is_Active) VALUES (@Lookup_Code, @Lookup_Type, @Lookup_Desc, @Is_Active)", connection);
            command.Parameters.AddWithValue("@Lookup_Code", lookup.Lookup_Code);
            command.Parameters.AddWithValue("@Lookup_Type", lookup.Lookup_Type);
            command.Parameters.AddWithValue("@Lookup_Desc", lookup.Lookup_Desc);
            command.Parameters.AddWithValue("@Is_Active", lookup.Is_Active);
            command.ExecuteNonQuery();
        }
    }

    public void UpdateLookup(Lookup lookup)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("UPDATE Lookup_table SET Lookup_Code = @Lookup_Code, Lookup_Type = @Lookup_Type, Lookup_Desc = @Lookup_Desc, Is_Active = @Is_Active WHERE Lookup_Id = @Lookup_Id", connection);
            command.Parameters.AddWithValue("@Lookup_Code", lookup.Lookup_Code);
            command.Parameters.AddWithValue("@Lookup_Type", lookup.Lookup_Type);
            command.Parameters.AddWithValue("@Lookup_Desc", lookup.Lookup_Desc);
            command.Parameters.AddWithValue("@Is_Active", lookup.Is_Active);
            command.Parameters.AddWithValue("@Lookup_Id", lookup.Lookup_Id);
            command.ExecuteNonQuery();
        }
    }

    public void DeleteLookup(int id)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("DELETE FROM Lookup_table WHERE Lookup_Id = @Lookup_Id", connection);
            command.Parameters.AddWithValue("@Lookup_Id", id);
            command.ExecuteNonQuery();
        }
    }
}