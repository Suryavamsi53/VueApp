using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class TransactionDataAccess
{
    private string connectionString = "your_connection_string";

    public List<Transaction> GetTransactions()
    {
        List<Transaction> transactions = new List<Transaction>();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Transaction_table", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Transaction transaction = new Transaction
                {
                    TransactionID = (int)reader["TransactionID"],
                    AccountID = (int)reader["AccountID"],
                    TransAmount = (decimal)reader["TransAmount"],
                    TransTypeID = (int)reader["TransTypeID"],
                    TransDate = (DateTime)reader["TransDate"]
                };
                transactions.Add(transaction);
            }
        }
        return transactions;
    }

    public void AddTransaction(Transaction transaction)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("INSERT INTO Transaction_table (AccountID, TransAmount, TransTypeID, TransDate) VALUES (@AccountID, @TransAmount, @TransTypeID, @TransDate)", connection);
            command.Parameters.AddWithValue("@AccountID", transaction.AccountID);
            command.Parameters.AddWithValue("@TransAmount", transaction.TransAmount);
            command.Parameters.AddWithValue("@TransTypeID", transaction.TransTypeID);
            command.Parameters.AddWithValue("@TransDate", transaction.TransDate);
            command.ExecuteNonQuery();
        }
    }

    public void UpdateTransaction(Transaction transaction)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("UPDATE Transaction_table SET AccountID = @AccountID, TransAmount = @TransAmount, TransTypeID = @TransTypeID, TransDate = @TransDate WHERE TransactionID = @TransactionID", connection);
            command.Parameters.AddWithValue("@AccountID", transaction.AccountID);
            command.Parameters.AddWithValue("@TransAmount", transaction.TransAmount);
            command.Parameters.AddWithValue("@TransTypeID", transaction.TransTypeID);
            command.Parameters.AddWithValue("@TransDate", transaction.TransDate);
            command.Parameters.AddWithValue("@TransactionID", transaction.TransactionID);
            command.ExecuteNonQuery();
        }
    }

    public void DeleteTransaction(int id)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("DELETE FROM Transaction_table WHERE TransactionID = @TransactionID", connection);
            command.Parameters.AddWithValue("@TransactionID", id);
            command.ExecuteNonQuery();
        }
    }
}