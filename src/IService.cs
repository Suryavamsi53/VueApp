Here's a full setup to connect your Vue.js component (`Lookup.vue`) with a WCF service using ADO.NET and Axios.

### 1. Create WCF Service

#### `IService.cs`
```csharp
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

[ServiceContract]
public interface IService
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
```




#### `Service.svc`
```csharp
using System.Collections.Generic;
using System.Data.SqlClient;

public class Service : IService
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
```

#### `Lookup.cs`
```csharp
public class Lookup
{
    public int Lookup_Id { get; set; }
    public string Lookup_Type { get; set; }
    public string Lookup_Desc { get; set; }
    public string Is_Active { get; set; }
    public string Created_By { get; set; }
    public DateTime Created_Date { get; set; }
}
```

### 2. Configure CORS for WCF Service

#### `Web.config`
```xml
<configuration>
  <system.webServer>
    <modules runAllManagedModulesForAllRequests="true"/>
    <httpProtocol>
      <customHeaders>
        <add name="Access-Control-Allow-Origin" value="*"/>
        <add name="Access-Control-Allow-Methods" value="GET, POST, PUT, DELETE, OPTIONS"/>
        <add name="Access-Control-Allow-Headers" value="Content-Type"/>
      </customHeaders>
    </httpProtocol>
  </system.webServer>
  <system.serviceModel>
    <services>
      <service name="YourNamespace.Service">
        <endpoint address="" binding="webHttpBinding" contract="YourNamespace.IService" behaviorConfiguration="web"/>
      </service>
    </services>
    <behaviors>
      <endpointBehaviors>
        <behavior name="web">
          <webHttp/>
        </behavior>
      </endpointBehaviors>
      <serviceBehaviors>
        <behavior>
          <serviceMetadata httpGetEnabled="true" />
          <serviceDebug includeExceptionDetailInFaults="false" />
        </behavior>
      </serviceBehaviors>
    </behaviors>
  </system.serviceModel>
</configuration>
```

### 3. Configure Axios in Vue.js

#### `frontend/src/axios.js`
```javascript
import axios from 'axios';

// Create an axios instance
const axiosInstance = axios.create({
  baseURL: 'http://localhost:5000', // Replace with your WCF service base URL
  timeout: 10000, // Request timeout
  headers: {
    'Content-Type': 'application/json',
  },
});

// Add a request interceptor
axiosInstance.interceptors.request.use(
  config => {
    // Do something before request is sent, like adding authorization headers
    return config;
  },
  error => {
    // Do something with request error
    return Promise.reject(error);
  }
);

// Add a response interceptor
axiosInstance.interceptors.response.use(
  response => response,
  error => {
    // Handle response errors
    return Promise.reject(error);
  }
);

export default axiosInstance;
```

### 4. Vue.js Component: `Lookup.vue`

```vue
<template>
  <div>
    <!-- Navbar -->
    <nav class="navbar">
      <div class="container">
        <!-- Lookup Table Button -->
        <button class="table-button" @click="showLookupTable">Lookup Table</button>

        <!-- CRUD Operation Buttons -->
        <div v-if="showCrudButtons" class="crud-buttons">
          <button class="crud-button" @click="toggleForm('add')">Add Data</button>
          <button class="crud-button" @click="toggleForm('delete')">Delete Data</button>
          <button class="crud-button" @click="toggleTable">Display</button>
        </div>
      </div>
    </nav>

    <!-- Content Area -->
    <div class="content">
      <!-- Error message display -->
      <h3 v-if="errorMsg" class="error-message">{{ errorMsg }}</h3>

      <!-- Data Table -->
      <div class="data-container" v-if="showTable">
        <table class="data-table">
          <thead>
            <tr>
              <th>Lookup Id</th>
              <th>Lookup Type</th>
              <th>Lookup Desc</th>
              <th>Is Active</th>
              <th>Created By</th>
              <th>Created Date</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="lookup in paginatedLookups" :key="lookup.Lookup_Id">
              <td>{{ lookup.Lookup_Id }}</td>
              <td>{{ lookup.Lookup_Type }}</td>
              <td>{{ lookup.Lookup_Desc }}</td>
              <td>{{ lookup.Is_Active }}</td>
              <td>{{ lookup.Created_By }}</td>
              <td>{{ lookup.Created_Date }}</td>
              <td>
                <button class="action-button edit-button" @click="editLookup(lookup)">Edit</button>
                <button class="action-button delete-button" @click="deleteLookup(lookup.Lookup_Id)">Delete</button>
              </td>
            </tr>
          </tbody>
        </table>
        <div class="pagination" v-if="lookups.length > pageSize">
          <button class="pagination-button" @click="prevPage" :disabled="currentPage === 1">Previous</button>
          <button class="pagination-button" @click="nextPage" :disabled="currentPage === totalPages">Next</button>
          <span>Page {{ currentPage }} of {{ totalPages }}</span>
        </div>
      </div>

      <!-- Add Lookup Form -->
      <div class="form-container" v-if="showAddForm">
        <h2>Add New Lookup</h2>
        <form @submit.prevent="addLookup">
          <input v-model="newLookup.Lookup_Id" placeholder="Lookup ID">
          <input v-model="newLookup.Lookup_Type" placeholder="Lookup