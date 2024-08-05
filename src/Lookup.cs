using System;

namespace WcfService.Models
{
    public class Lookup
    {
        public int Lookup_Id { get; set; }
        public string Lookup_Type { get; set; }
        public string Lookup_Desc { get; set; }
        public string Is_Active { get; set; }
        public string Created_By { get; set; }
        public DateTime Created_Date { get; set; }
    }
}


ILookupService.cs

using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

[ServiceContract]
public interface ILookupService
{
    [OperationContract]
    [WebGet(UriTemplate = "lookups", ResponseFormat = WebMessageFormat.Json)]
    List<Lookup> GetLookups();

    [OperationContract]
    [WebInvoke(Method = "POST", UriTemplate = "lookups", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    void AddLookup(Lookup lookup);

    [OperationContract]
    [WebInvoke(Method = "PUT", UriTemplate = "lookups/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    void UpdateLookup(string id, Lookup lookup);

    [OperationContract]
    [WebInvoke(Method = "DELETE", UriTemplate = "lookups/{id}", ResponseFormat = WebMessageFormat.Json)]
    void DeleteLookup(string id);
}



LookupService.svc.cs

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ServiceModel;
using System.ServiceModel.Web;

public class LookupService : ILookupService
{
    private readonly string connectionString = "your_connection_string_here";

    public List<Lookup> GetLookups()
    {
        List<Lookup> lookups = new List<Lookup>();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM Lookups";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                lookups.Add(new Lookup
                {
                    Lookup_Id = reader.GetInt32(0),
                    Lookup_Type = reader.GetString(1),
                    Lookup_Desc = reader.GetString(2),
                    Is_Active = reader.GetString(3),
                    Created_By = reader.GetString(4),
                    Created_Date = reader.GetDateTime(5)
                });
            }
        }
        return lookups;
    }

    public void AddLookup(Lookup lookup)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Lookups (Lookup_Id, Lookup_Type, Lookup_Desc, Is_Active, Created_By, Created_Date) VALUES (@Lookup_Id, @Lookup_Type, @Lookup_Desc, @Is_Active, @Created_By, @Created_Date)";
            SqlCommand command = new SqlCommand(query, connection);
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

    public void UpdateLookup(string id, Lookup lookup)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "UPDATE Lookups SET Lookup_Type = @Lookup_Type, Lookup_Desc = @Lookup_Desc, Is_Active = @Is_Active WHERE Lookup_Id = @Lookup_Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Lookup_Id", id);
            command.Parameters.AddWithValue("@Lookup_Type", lookup.Lookup_Type);
            command.Parameters.AddWithValue("@Lookup_Desc", lookup.Lookup_Desc);
            command.Parameters.AddWithValue("@Is_Active", lookup.Is_Active);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public void DeleteLookup(string id)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM Lookups WHERE Lookup_Id = @Lookup_Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Lookup_Id", id);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}

startup.cs


using CoreWCF;
using Microsoft.Extensions.DependencyInjection;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddServiceModelServices();
        services.AddServiceModelConfigurationManager();
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.UseServiceModel(serviceBuilder =>
            {
                serviceBuilder.AddService<LookupService>();
                serviceBuilder.AddServiceEndpoint<LookupService, ILookupService>(new WSHttpBinding(), "/LookupService");
            });
        });
    }
}




import axios from 'axios';

// Create an Axios instance with the base URL pointing to your WCF service endpoint
const instance = axios.create({
  baseURL: 'http://localhost:5000/LookupService', // Replace with your actual service URL
  headers: {
    'Content-Type': 'application/json'
  }
});

// Define the API methods

// Get all lookups
export const getLookups = () => {
  return instance.get('/');
};

// Add a new lookup
export const addLookup = (lookup) => {
  return instance.post('/', lookup);
};

// Update an existing lookup
export const updateLookup = (id, lookup) => {
  return instance.put(`/${id}`, lookup);
};

// Delete a lookup by ID
export const deleteLookup = (id) => {
  return instance.delete(`/${id}`);
};

export default instance;
