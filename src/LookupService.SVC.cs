// LookupService.svc.cs
public class LookupService : ILookupService
{
    private string connectionString = "your_connection_string_here";

    public List<Lookup> GetLookups()
    {
        List<Lookup> lookups = new List<Lookup>();
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Lookups", con);
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                lookups.Add(new Lookup
                {
                    Lookup_Id = (int)rdr["Lookup_Id"],
                    Lookup_Type = (string)rdr["Lookup_Type"],
                    Lookup_Desc = (string)rdr["Lookup_Desc"],
                    Is_Active = (string)rdr["Is_Active"],
                    Created_By = (string)rdr["Created_By"],
                    Created_Date = (DateTime)rdr["Created_Date"]
                });
            }
        }
        return lookups;
    }

    public void AddLookup(Lookup lookup)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Lookups (Lookup_Id, Lookup_Type, Lookup_Desc, Is_Active, Created_By, Created_Date) VALUES (@Lookup_Id, @Lookup_Type, @Lookup_Desc, @Is_Active, @Created_By, @Created_Date)", con);
            cmd.Parameters.AddWithValue("@Lookup_Id", lookup.Lookup_Id);
            cmd.Parameters.AddWithValue("@Lookup_Type", lookup.Lookup_Type);
            cmd.Parameters.AddWithValue("@Lookup_Desc", lookup.Lookup_Desc);
            cmd.Parameters.AddWithValue("@Is_Active", lookup.Is_Active);
            cmd.Parameters.AddWithValue("@Created_By", lookup.Created_By);
            cmd.Parameters.AddWithValue("@Created_Date", lookup.Created_Date);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public void UpdateLookup(Lookup lookup)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand("UPDATE Lookups SET Lookup_Type=@Lookup_Type, Lookup_Desc=@Lookup_Desc, Is_Active=@Is_Active WHERE Lookup_Id=@Lookup_Id", con);
            cmd.Parameters.AddWithValue("@Lookup_Id", lookup.Lookup_Id);
            cmd.Parameters.AddWithValue("@Lookup_Type", lookup.Lookup_Type);
            cmd.Parameters.AddWithValue("@Lookup_Desc", lookup.Lookup_Desc);
            cmd.Parameters.AddWithValue("@Is_Active", lookup.Is_Active);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public void DeleteLookup(int lookupId)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Lookups WHERE Lookup_Id=@Lookup_Id", con);
            cmd.Parameters.AddWithValue("@Lookup_Id", lookupId);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
}