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
          <input v-model="newLookup.Lookup_Type" placeholder="Lookup Type">
          <input v-model="newLookup.Lookup_Desc" placeholder="Lookup Description">
          <input v-model="newLookup.Is_Active" placeholder="Is Active">
          <input v-model="newLookup.Created_By" placeholder="Created By">
          <input v-model="newLookup.Created_Date" placeholder="Created Date">
          <button class="action-button submit-button" type="submit">Add</button>
          <button class="action-button cancel-button" type="button" @click="cancelAdd">Cancel</button>
        </form>
      </div>

      <!-- Update Lookup Form -->
      <div class="form-container" v-if="showUpdateForm">
        <h2>Update Lookup</h2>
        <form @submit.prevent="updateLookup">
          <input v-model="selectedLookup.Lookup_Id" placeholder="Lookup ID" readonly>
          <input v-model="selectedLookup.Lookup_Code" placeholder="Lookup Code">
          <input v-model="selectedLookup.Lookup_Type" placeholder="Lookup Type">
          <input v-model="selectedLookup.Lookup_Desc" placeholder="Lookup Description">
          <input v-model="selectedLookup.Is_Active" placeholder="Is Active">
          <button class="action-button submit-button" type="submit">Update</button>
          <button class="action-button cancel-button" type="button" @click="cancelUpdate">Cancel</button>
        </form>
      </div>

      <!-- Delete Lookup Form -->
      <div class="form-container" v-if="showDeleteForm">
        <h2>Delete Lookup</h2>
        <form @submit.prevent="deleteLookupById">
          <input v-model="currentLookupId" placeholder="Enter Lookup ID to Delete">
          <button class="action-button submit-button" type="submit">Delete</button>
          <button class="action-button cancel-button" type="button" @click="cancelDelete">Cancel</button>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      lookups: [],
      newLookup: {
        Lookup_Id: '',
        Lookup_Type: '',
        Lookup_Desc: '',
        Is_Active: '',
        Created_By: '',
        Created_Date: ''
      },
      selectedLookup: {},
      showCrudButtons: false,
      showTable: false,
      showAddForm: false,
      showUpdateForm: false,
      showDeleteForm: false,
      errorMsg: '',
      pageSize: 10,
      currentPage: 1
    };
  },
  computed: {
    paginatedLookups() {
      const start = (this.currentPage - 1) * this.pageSize;
      return this.lookups.slice(start, start + this.pageSize);
    },
    totalPages() {
      return Math.ceil(this.lookups.length / this.pageSize);
    }
  },
  methods: {
    showLookupTable() {
      this.showCrudButtons = true;
      this.showTable = false;
      this.showAddForm = false;
      this.showUpdateForm = false;
      this.showDeleteForm = false;
    },
    toggleForm(formType) {
      this.showAddForm = formType === 'add';
      this.showDeleteForm = formType === 'delete';
      this.showUpdateForm = false;
      this.errorMsg = '';
    },
    toggleTable() {
      this.showTable = !this.showTable;
    },
    fetchLookups() {
      fetch('http://localhost:3000/lookups')
        .then(response => response.json())
        .then(data => {
          this.lookups = data;
        })
        .catch(error => {
          this.errorMsg = 'Error fetching lookups';
        });
    },
    addLookup() {
      fetch('http://localhost:3000/lookups', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(this.newLookup)
      })
      .then(() => {
        this.fetchLookups();
        this.resetForm();
      })
      .catch(error => {
        this.errorMsg = 'Error adding lookup';
      });
    },
    updateLookup() {
      fetch(`http://localhost:3000/lookups/${this.selectedLookup.Lookup_Id}`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(this.selectedLookup)
      })
      .then(() => {
        this.fetchLookups();
        this.resetForm();
      })
      .catch(error => {
        this.errorMsg = 'Error updating lookup';
      });
    },
    deleteLookupById() {
      fetch(`http://localhost:3000/lookups/${this.currentLookupId}`, {
        method: 'DELETE'
      })
      .then(() => {
        this.fetchLookups();
        this.resetForm();
      })
      .catch(error => {
        this.errorMsg = 'Error deleting lookup';
      });
    },
    editLookup(lookup) {
      this.selectedLookup = { ...lookup };
      this.showUpdateForm = true;
      this.showAddForm = false;
      this.showDeleteForm = false;
      this.errorMsg = '';
    },
    deleteLookup(lookupId) {
      this.currentLookupId = lookupId;
      this.showDeleteForm = true;
      this.showUpdateForm = false;
      this.showAddForm = false;
      this.errorMsg = '';
    },
    resetForm() {
      this.newLookup = {
        Lookup_Id: '',
        Lookup_Type: '',
        Lookup_Desc: '',
        Is_Active: '',
        Created_By: '',
        Created_Date: ''
      };
      this.selectedLookup = {};
      this.showAddForm = false;
      this.showUpdateForm = false;
      this.showDeleteForm = false;
      this.errorMsg = '';
    },
    prevPage() {
      if (this.currentPage > 1) {
        this.currentPage--;
      }
    },
    nextPage() {
      if (this.currentPage < this.totalPages) {
        this.currentPage++;
      }
    }
  },
  mounted() {
    this.fetchLookups();
  }
};
</script>

<style scoped>
.navbar {
  background-color: #333;
  color: white;
  padding: 1rem;
}

.container {
  display: flex;
  justify-content: flex-start; /* Align items to the left */
  align-items: center; /* Center items vertically */
  gap: 10px; /* Add some spacing between items */
}

.table-button {
  padding: 10px;
  background-color: #007bff;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s;
}

.crud-buttons {
  display: flex;
  gap: 10px;
}

.crud-button {
  padding: 10px;
  background-color: #007bff;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s;
}

/* .crud-button:hover {
  background-color: #555;
} */

.content {
  padding: 2rem;
}

.error-message {
  color: red;
}

.data-container {
  margin-top: 2rem;
}

.data-table {
  width: 100%;
  border-collapse: collapse;
}

.data-table th,
.data-table td {
  border: 1px solid #ddd;
  padding: 0.75rem;
}

.action-button {
  padding: 0.5rem 1rem;
  border: none;
  cursor: pointer;
  transition: background-color 0.3s;
}

.edit-button {
  background-color: #4CAF50;
  color: white;
}

.edit-button:hover {
  background-color: #45a049;
}

.delete-button {
  background-color: #f44336;
  color: white;
}

.delete-button:hover {
  background-color: #da190b;
}

.pagination {
  margin-top: 1rem;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.pagination-button {
  padding: 0.5rem 1rem;
  background-color: #888;
  color: white;
  border: none;
  cursor: pointer;
  transition: background-color 0.3s;
}

.pagination-button:disabled {
  background-color: #ccc;
  cursor: not-allowed;
}

.pagination-button:not(:disabled):hover {
  background-color: #555;
}

.form-container {
  margin-top: 2rem;
}

.form-container h2 {
  margin-bottom: 1rem;
}

.form-container input {
  display: block;
  margin-bottom: 1rem;
  padding: 0.5rem;
  width: 100%;
  box-sizing: border-box;
}

.submit-button {
  background-color: #4CAF50;
  color: white;
}

.submit-button:hover {
  background-color: #45a049;
}

.cancel-button {
  background-color: #f44336;
  color: white;
}

.cancel-button:hover {
  background-color: #da190b;
}
</style>





<!-- <template>
  <div>
    
    <nav class="navbar">
      <div class="container">
     
        <div v-if="showCrudButtons" class="crud-buttons">
          <button class="crud-button" @click="toggleForm('add')">Add Data</button>
          <button class="crud-button" @click="toggleForm('delete')">Delete Data</button>
          <button class="crud-button" @click="toggleTable">Display</button>
        </div>
      </div>
    </nav>

   
    <div class="content">
 
      <h3 v-if="errorMsg" class="error-message">{{ errorMsg }}</h3>
 
      <div class="data-container" v-if="showTable">
        <table class="data-table">
          <thead>
            <tr>
              <th>Lookup Id</th>
              <th>Lookup Type</th>
              <th>Lookup Desc</th>
              <th>Is Active</th>
              <th>CreatedcBy</th>
              <th>CreatedcDate</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            <tr v-bind="Lookup_Table in paginatedLookups" :key="lookup.Lookup_Id">
              <td>{{ Lookup_Table.Lookup_id }}</td>
              <td>{{ Lookup_Table.Lookup_type }}</td>
              <td>{{ Lookup_Table.Lookup_desc }}</td>
              <td>{{ Lookup_Table.is_active }}</td>
              <td>{{ Lookup_Table.createdby }}</td>
              <td>{{ Lookup_Table.createdDATE }}</td>
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

    
      <div class="form-container" v-if="showAddForm">
        <h2>Add New Lookup</h2>
        <form @submit.prevent="Lookup_Table">
          <input v-model="Lookup_Table.Lookup_Id" placeholder="Lookup ID">
          <input v-model="Lookup_Table.Lookup_Type" placeholder="Lookup Type">
          <input v-model="Lookup_Table.Lookup_Desc" placeholder="Lookup Desc">
          <input v-model="Lookup_Table.Is_Active" placeholder="Is Active">
          <input v-model="Lookup_Table.createdby" placeholder="Created By">
          <input v-model="Lookup_Table.createdDATE" placeholder="Created Date">
          <button class="action-button submit-button" type="submit">Add</button>
          <button class="action-button cancel-button" type="button" @click="cancelAdd">Cancel</button>
        </form>
      </div>

   
      <div class="form-container" v-if="showUpdateForm">
        <h2>Update Lookup</h2>
        <form @submit.prevent="updateLookup">
          <input v-model="newLookup.Lookup_Id" placeholder="Lookup ID" readonly>
          <input v-model="newLookup.Lookup_Cod" placeholder="Lookup Code">
          <input v-model="newLookup.Lookup_Type" placeholder="Lookup Type">
          <input v-model="newLookup.Lookup_Desc" placeholder="Lookup Description">
          <input v-model="newLookup.Is_Active" placeholder="Is Active">
          <button class="action-button submit-button" type="submit">Update</button>
          <button class="action-button cancel-button" type="button" @click="cancelUpdate">Cancel</button>
        </form>
      </div>

    
      <div class="form-container" v-if="showDeleteForm">
        <h2>Delete Lookup</h2>
        <form @submit.prevent="deleteLookupById">
          <input v-model="currentLookupId" placeholder="Enter Lookup ID to Delete">
          <button class="action-button submit-button" type="submit">Delete</button>
          <button class="action-button cancel-button" type="button" @click="cancelDelete">Cancel</button>
        </form>
      </div>
    </div>
  </div>
</template> -->

 


<!-- <template>
    <div>
      <h1>Lookup Table</h1>
      <button @click="showForm = !showForm">Add Lookup</button>
      <div v-if="showForm">
        <h2>{{ formId ? 'Edit Lookup' : 'Add Lookup' }}</h2>
        <form @submit.prevent="saveLookup">
          <label for="Lookup_Code">Lookup Code:</label>
          <input v-model="lookup.Lookup_Code" type="number" id="Lookup_Code" required />
          
          <label for="Lookup_Type">Lookup Type:</label>
          <input v-model="lookup.Lookup_Type" type="text" id="Lookup_Type" />
  
          <label for="Lookup_Desc">Lookup Description:</label>
          <input v-model="lookup.Lookup_Desc" type="text" id="Lookup_Desc" />
  
          <label for="Is_Active">Is Active:</label>
          <input v-model="lookup.Is_Active" type="text" id="Is_Active" />
  
          <button type="submit">Save</button>
          <button @click="resetForm">Cancel</button>
        </form>
      </div>
      <ul>
        <li v-for="item in lookups" :key="item.Lookup_Id">
          {{ item.Lookup_Code }} - {{ item.Lookup_Type }}
          <button @click="editLookup(item.Lookup_Id)">Edit</button>
          <button @click="deleteLookup(item.Lookup_Id)">Delete</button>
        </li>
      </ul>
    </div>
  </template>
   -->
  <!-- <script>
  export default {
    data() {
      return {
        lookups: [],
        lookup: {
          Lookup_Code: '',
          Lookup_Type: '',
          Lookup_Desc: '',
          Is_Active: ''
        },
        showForm: false,
        formId: null
      };
    },
    methods: {
      fetchLookups() {
        fetch('http://localhost:3000/lookups')
          .then(response => response.json())
          .then(data => {
            this.lookups = data;
          });
      },
      saveLookup() {
        const method = this.formId ? 'PUT' : 'POST';
        const url = this.formId
          ? `http://localhost:3000/lookups/${this.formId}`
          : 'http://localhost:3000/lookups';
  
        fetch(url, {
          method: method,
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(this.lookup)
        }).then(() => {
          this.fetchLookups();
          this.resetForm();
        });
      },
      editLookup(id) {
        fetch(`http://localhost:3000/lookups/${id}`)
          .then(response => response.json())
          .then(data => {
            this.lookup = { ...data };
            this.formId = id;
            this.showForm = true;
          });
      },
      deleteLookup(id) {
        fetch(`http://localhost:3000/lookups/${id}`, {
          method: 'DELETE'
        }).then(() => {
          this.fetchLookups();
        });
      },
      resetForm() {
        this.lookup = { Lookup_Code: '', Lookup_Type: '', Lookup_Desc: '', Is_Active: '' };
        this.showForm = false;
        this.formId = null;
      }
    },
    mounted() {
      this.fetchLookups();
    }
  };
  </script> -->
  
  <!-- <style scoped>
  .navbar {
    background-color: #333;
    color: white;
    padding: 1rem;
  }
  
  .container {
    display: flex;
    justify-content: flex-start; /* Align items to the left */
    align-items: center; /* Center items vertically */
    gap: 10px; /* Add some spacing between items */
  }
  
  .crud-buttons {
    display: flex;
    gap: 10px;
  }
  
  .crud-button {
    padding: 10px;
    background-color: #007bff;
    color: white;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    transition: background-color 0.3s;
  }
  
  /* .crud-button:hover {
    background-color: #555;
  } */
  
  .content {
    padding: 2rem;
  }
  
  .error-message {
    color: red;
  }
  
  .data-container {
    margin-top: 2rem;
  }
  
  .data-table {
    width: 100%;
    border-collapse: collapse;
  }
  
  .data-table th, .data-table td {
    border: 1px solid #ddd;
    padding: 0.75rem;
  }
  
  .action-button {
    padding: 0.5rem 1rem;
    border: none;
    cursor: pointer;
    transition: background-color 0.3s;
  }
  
  .edit-button {
    background-color: #4CAF50;
    color: white;
  }
  
  .edit-button:hover {
    background-color: #45a049;
  }
  
  .delete-button {
    background-color: #f44336;
    color: white;
  }
  
  .delete-button:hover {
    background-color: #da190b;
  }
  
  .pagination {
    margin-top: 1rem;
    display: flex;
    justify-content: space-between;
    align-items: center;
  }
  
  .pagination-button {
    padding: 0.5rem 1rem;
    background-color: #888;
    color: white;
    border: none;
    cursor: pointer;
    transition: background-color 0.3s;
  }
  
  .pagination-button:disabled {
    background-color: #ccc;
    cursor: not-allowed;
  }
  
  .pagination-button:not(:disabled):hover {
    background-color: #555;
  }
  
  .form-container {
    margin-top: 2rem;
  }
  
  .form-container h2 {
    margin-bottom: 1rem;
  }
  
  .form-container input {
    display: block;
    margin-bottom: 1rem;
    padding: 0.5rem;
    width: 100%;
    box-sizing: border-box;
  }
  
  .submit-button {
    background-color: #4CAF50;
    color: white;
  }
  
  .submit-button:hover {
    background-color: #45a049;
  }
  
  .cancel-button {
    background-color: #f44336;
    color: white;
  }
  
  .cancel-button:hover {
    background-color: #da190b;
  }
  </style>
  
   -->





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
          <input v-model="newLookup.Lookup_Type" placeholder="Lookup Type">
          <input v-model="newLookup.Lookup_Desc" placeholder="Lookup Description">
          <input v-model="newLookup.Is_Active" placeholder="Is Active">
          <input v-model="newLookup.Created_By" placeholder="Created By">
          <input v-model="newLookup.Created_Date" placeholder="Created Date">
          <button class="action-button submit-button" type="submit">Add</button>
          <button class="action-button cancel-button" type="button" @click="cancelAdd">Cancel</button>
        </form>
      </div>

      <!-- Update Lookup Form -->
      <div class="form-container" v-if="showUpdateForm">
        <h2>Update Lookup</h2>
        <form @submit.prevent="updateLookup">
          <input v-model="selectedLookup.Lookup_Id" placeholder="Lookup ID" readonly>
          <input v-model="selectedLookup.Lookup_Type" placeholder="Lookup Type">
          <input v-model="selectedLookup.Lookup_Desc" placeholder="Lookup Description">
          <input v-model="selectedLookup.Is_Active" placeholder="Is Active">
          <button class="action-button submit-button" type="submit">Update</button>
          <button class="action-button cancel-button" type="button" @click="cancelUpdate">Cancel</button>
        </form>
      </div>

      <!-- Delete Lookup Form -->
      <div class="form-container" v-if="showDeleteForm">
        <h2>Delete Lookup</h2>
        <form @submit.prevent="deleteLookupById">
          <input v-model="currentLookupId" placeholder="Enter Lookup ID to Delete">
          <button class="action-button submit-button" type="submit">Delete</button>
          <button class="action-button cancel-button" type="button" @click="cancelDelete">Cancel</button>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
import axios from '../axios.js'; // Import the configured axios instance

export default {
  data() {
    return {
      lookups: [],
      newLookup: {
        Lookup_Id: '',
        Lookup_Type: '',
        Lookup_Desc: '',
        Is_Active: '',
        Created_By: '',
        Created_Date: ''
      },
      selectedLookup: {},
      showCrudButtons: false,
      showTable: false,
      showAddForm: false,
      showUpdateForm: false,
      showDeleteForm: false,
      errorMsg: '',
      pageSize: 10,
      currentPage: 1
    };
  },
  computed: {
    paginatedLookups() {
      const start = (this.currentPage - 1) * this.pageSize;
      return this.lookups.slice(start, start + this.pageSize);
    },
    totalPages() {
      return Math.ceil(this.lookups.length / this.pageSize);
    }
  },
  methods: {
    showLookupTable() {
      this.showCrudButtons = true;
      this.showTable = false;
      this.showAddForm = false;
      this.showUpdateForm = false;
      this.showDeleteForm = false;
    },
    toggleForm(formType) {
      this.showAddForm = formType === 'add';
      this.showDeleteForm = formType === 'delete';
      this.showUpdateForm = false;
      this.errorMsg = '';
    },
    toggleTable() {
      this.showTable = !this.showTable;
    },
    fetchLookups() {
      axios.get('/lookups')
        .then(response => {
          this.lookups = response.data;
        })
        .catch(error => {
          this.errorMsg = 'Error fetching lookups';
        });
    },
    addLookup() {
      axios.post('/lookups', this.newLookup)
        .then(() => {
          this.fetchLookups();
          this.resetForm();
        })
        .catch(error => {
          this.errorMsg = 'Error adding lookup';
        });
    },
    updateLookup() {
      axios.put(`/lookups/${this.selectedLookup.Lookup_Id}`, this.selectedLookup)
        .then(() => {
          this.fetchLookups();
          this.resetForm();
        })
        .catch(error => {
          this.errorMsg = 'Error updating lookup';
        });
    },
    deleteLookupById() {
      axios.delete(`/lookups/${this.currentLookupId}`)
        .then(() => {
          this.fetchLookups();
          this.resetForm();
        })
        .catch(error => {
          this.errorMsg = 'Error deleting lookup';
        });
    },
    editLookup(lookup) {
      this.selectedLookup = { ...lookup };
      this.showUpdateForm = true;
      this.showAddForm = false;
      this.showDeleteForm = false;
      this.errorMsg = '';
    },
    deleteLookup(lookupId) {
      this.currentLookupId = lookupId;
      this.showDeleteForm = true;
      this.showUpdateForm = false;
      this.showAddForm = false;
      this.errorMsg = '';
    },
    resetForm() {
      this.newLookup = {
        Lookup_Id: '',
        Lookup_Type: '',
        Lookup_Desc: '',
        Is_Active: '',
        Created_By: '',
        Created_Date: ''
      };
      this.selectedLookup = {};
      this.showAddForm = false;
      this.showUpdateForm = false;
      this.showDeleteForm = false;
      this.errorMsg = '';
    },
    prevPage() {
      if (this.currentPage > 1) {
        this.currentPage--;
      }
    },
    nextPage() {
      if (this.currentPage < this.totalPages) {
        this.currentPage++;
      }
    }
  },
  mounted() {
    this.fetchLookups();
  }
};
</script>

<style>
/* Add your styles here */