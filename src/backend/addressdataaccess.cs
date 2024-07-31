using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class AddressDataAccess
{
    private string connectionString = "your_connection_string";

    public List<Address> GetAddresses()
    {
        List<Address> addresses = new List<Address>();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Address_table", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Address address = new Address
                {
                    AddressID = (int)reader["AddressID"],
                    AccountID = (int)reader["AccountID"],
                    AddressTypeID = (int)reader["AddressTypeID"],
                    AddressDetail = (string)reader["Address"]
                };
                addresses.Add(address);
            }
        }
        return addresses;
    }

    public void AddAddress(Address address)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("INSERT INTO Address_table (AccountID, AddressTypeID, Address) VALUES (@AccountID, @AddressTypeID, @Address)", connection);
            command.Parameters.AddWithValue("@AccountID", address.AccountID);
            command.Parameters.AddWithValue("@AddressTypeID", address.AddressTypeID);
            command.Parameters.AddWithValue("@Address", address.AddressDetail);
            command.ExecuteNonQuery();
        }
    }

    public void UpdateAddress(Address address)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("UPDATE Address_table SET AccountID = @AccountID, AddressTypeID = @AddressTypeID, Address = @Address WHERE AddressID = @AddressID", connection);
            command.Parameters.AddWithValue("@AccountID", address.AccountID);
            command.Parameters.AddWithValue("@AddressTypeID", address.AddressTypeID);
            command.Parameters.AddWithValue("@Address", address.AddressDetail);
            command.Parameters.AddWithValue("@AddressID", address.AddressID);
            command.ExecuteNonQuery();
        }
    }

    public void DeleteAddress(int id)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("DELETE FROM Address_table WHERE AddressID = @AddressID", connection);
            command.Parameters.AddWithValue("@AddressID", id);
            command.ExecuteNonQuery();
        }
    }
}