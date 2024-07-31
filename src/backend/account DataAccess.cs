using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class AccountDataAccess
{
    private string connectionString = "your_connection_string";

    public List<Account> GetAccounts()
    {
        List<Account> accounts = new List<Account>();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Account_table", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Account account = new Account
                {
                    AccId = (int)reader["AccId"],
                    AccountNumber = (string)reader["AccountNumber"],
                    AccountStatus_id = (int)reader["AccountStatus_id"]
                };
                accounts.Add(account);
            }
        }
        return accounts;
    }

    public void AddAccount(Account account)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("INSERT INTO Account_table (AccountNumber, AccountStatus_id) VALUES (@AccountNumber, @AccountStatus_id)", connection);
            command.Parameters.AddWithValue("@AccountNumber", account.AccountNumber);
            command.Parameters.AddWithValue("@AccountStatus_id", account.AccountStatus_id);
            command.ExecuteNonQuery();
        }
    }

    public void UpdateAccount(Account account)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("UPDATE Account_table SET AccountNumber = @AccountNumber, AccountStatus_id = @AccountStatus_id WHERE AccId = @AccId", connection);
            command.Parameters.AddWithValue("@AccountNumber", account.AccountNumber);
            command.Parameters.AddWithValue("@AccountStatus_id", account.AccountStatus_id);
            command.Parameters.AddWithValue("@AccId", account.AccId);
            command.ExecuteNonQuery();
        }
    }

    public void DeleteAccount(int id)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("DELETE FROM Account_table WHERE AccId = @AccId", connection);
            command.Parameters.AddWithValue("@AccId", id);
            command.ExecuteNonQuery();
        }
    }
}