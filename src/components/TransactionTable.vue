<template>
    <div>
      <h1>Transaction Table</h1>
      <button @click="showForm = !showForm">Add Transaction</button>
      <div v-if="showForm">
        <h2>{{ formId ? 'Edit Transaction' : 'Add Transaction' }}</h2>
        <form @submit.prevent="saveTransaction">
          <label for="AccountID">Account ID:</label>
          <input v-model="transaction.AccountID" type="number" id="AccountID" required />
          
          <label for="TransAmount">Transaction Amount:</label>
          <input v-model="transaction.TransAmount" type="number" id="TransAmount" />
  
          <label for="TransTypeID">Transaction Type ID:</label>
          <input v-model="transaction.TransTypeID" type="number" id="TransTypeID" />
  
          <label for="TransDate">Transaction Date:</label>
          <input v-model="transaction.TransDate" type="date" id="TransDate" />
  
          <button type="submit">Save</button>
          <button @click="resetForm">Cancel</button>
        </form>
      </div>
      <ul>
        <li v-for="item in transactions" :key="item.TransactionID">
          {{ item.AccountID }} - {{ item.TransAmount }}
          <button @click="editTransaction(item.TransactionID)">Edit</button>
          <button @click="deleteTransaction(item.TransactionID)">Delete</button>
        </li>
      </ul>
    </div>
  </template>
  
  <script>
  export default {
    data() {
      return {
        transactions: [],
        transaction: {
          AccountID: '',
          TransAmount: '',
          TransTypeID: '',
          TransDate: ''
        },
        showForm: false,
        formId: null
      };
    },
    methods: {
      fetchTransactions() {
        fetch('http://localhost:3000/transactions')
          .then(response => response.json())
          .then(data => {
            this.transactions = data;
          });
      },
      saveTransaction() {
        const method = this.formId ? 'PUT' : 'POST';
        const url = this.formId
          ? `http://localhost:3000/transactions/${this.formId}`
          : 'http://localhost:3000/transactions';
  
        fetch(url, {
          method: method,
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(this.transaction)
        }).then(() => {
          this.fetchTransactions();
          this.resetForm();
        });
      },
      editTransaction(id) {
        fetch(`http://localhost:3000/transactions/${id}`)
          .then(response => response.json())
          .then(data => {
            this.transaction = { ...data };
            this.formId = id;
            this.showForm = true;
          });
      },
      deleteTransaction(id) {
        fetch(`http://localhost:3000/transactions/${id}`, {
          method: 'DELETE'
        }).then(() => {
          this.fetchTransactions();
        });
      },
      resetForm() {
        this.transaction = { AccountID: '', TransAmount: '', TransTypeID: '', TransDate: '' };
        this.showForm = false;
        this.formId = null;
      }
    },
    mounted() {
      this.fetchTransactions();
    }
  };
  </script>
  
  <style>
  /* Add your styles here */
  </style>
  






<!--  

new code 


<template>
  <div>
    <h1>Transaction Table</h1>
    <button @click="showForm = !showForm">Add Transaction</button>
    <div v-if="showForm">
      <h2>{{ formId ? 'Edit Transaction' : 'Add Transaction' }}</h2>
      <form @submit.prevent="saveTransaction">
        <label for="AccountID">Account ID:</label>
        <input v-model="transaction.AccountID" type="number" id="AccountID" required />

        <label for="TransAmount">Transaction Amount:</label>
        <input v-model="transaction.TransAmount" type="number" id="TransAmount" />

        <label for="TransTypeID">Transaction Type ID:</label>
        <input v-model="transaction.TransTypeID" type="number" id="TransTypeID" />

        <label for="TransDate">Transaction Date:</label>
        <input v-model="transaction.TransDate" type="date" id="TransDate" />

        <button type="submit">Save</button>
        <button @click="resetForm">Cancel</button>
      </form>
    </div>
    <ul>
      <li v-for="item in transactions" :key="item.TransactionID">
        {{ item.AccountID }} - {{ item.TransAmount }}
        <button @click="editTransaction(item.TransactionID)">Edit</button>
        <button @click="deleteTransaction(item.TransactionID)">Delete</button>
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  data() {
    return {
      transactions: [],
      transaction: {
        AccountID: '',
        TransAmount: '',
        TransTypeID: '',
        TransDate: ''
      },
      showForm: false,
      formId: null
    };
  },
  methods: {
    fetchTransactions() {
      fetch('http://localhost:3000/transactions')
        .then(response => response.json())
        .then(data => {
          this.transactions = data;
        });
    },
    saveTransaction() {
      const method = this.formId ? 'PUT' : 'POST';
      const url = this.formId
        ? `http://localhost:3000/transactions/${this.formId}`
        : 'http://localhost:3000/transactions';

      fetch(url, {
        method: method,
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(this.transaction)
      }).then(() => {
        this.fetchTransactions();
        this.resetForm();
      });
    },
    editTransaction(id) {
      fetch(`http://localhost:3000/transactions/${id}`)
        .then(response => response.json())
        .then(data => {
          this.transaction = { ...data };
          this.formId = id;
          this.showForm = true;
        });
    },
    deleteTransaction(id) {
      fetch(`http://localhost:3000/transactions/${id}`, {
        method: 'DELETE'
      }).then(() => {
        this.fetchTransactions();
      });
    },
    resetForm() {
      this.transaction = { AccountID: '', TransAmount: '', TransTypeID: '', TransDate: '' };
      this.showForm = false;
      this.formId = null;
    }
  },
  mounted() {
    this.fetchTransactions();
  }
};
</script>

<style>
/* Add your styles here */
</style>

-->




<!-- 

const express = require('express');
const sql = require('mssql');
const app = express();

app.use(express.json());

const config = {
  user: 'yourUsername',
  password: 'yourPassword',
  server: 'yourServer',
  database: 'yourDatabase',
  options: {
    encrypt: true, // Use this if you're on Windows Azure
    trustServerCertificate: true // Use this if your SQL server has a self-signed certificate
  }
};

app.get('/transactions', async (req, res) => {
  try {
    await sql.connect(config);
    const result = await sql.query('SELECT * FROM Transaction_table');
    res.json(result.recordset);
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

app.post('/transactions', async (req, res) => {
  const { AccountID, TransAmount, TransTypeID, TransDate } = req.body;
  try {
    await sql.connect(config);
    const result = await sql.query(`
      INSERT INTO Transaction_table (AccountID, TransAmount, TransTypeID, TransDate)
      VALUES (@AccountID, @TransAmount, @TransTypeID, @TransDate)
    `, {
      AccountID, TransAmount, TransTypeID, TransDate
    });
    res.json(result);
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

app.put('/transactions/:id', async (req, res) => {
  const { id } = req.params;
  const { AccountID, TransAmount, TransTypeID, TransDate } = req.body;
  try {
    await sql.connect(config);
    const result = await sql.query(`
      UPDATE Transaction_table
      SET AccountID = @AccountID, TransAmount = @TransAmount, TransTypeID = @TransTypeID, TransDate = @TransDate
      WHERE TransactionID = @id
    `, {
      AccountID, TransAmount, TransTypeID, TransDate, id
    });
    res.json(result);
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

app.delete('/transactions/:id', async (req, res) => {
  const { id } = req.params;
  try {
    await sql.connect(config);
    const result = await sql.query(`
      DELETE FROM Transaction_table WHERE TransactionID = @id
    `, { id });
    res.json(result);
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

const PORT = process.env.PORT || 3000;
app.listen(PORT, () => {
  console.log(`Server is running on port ${PORT}`);
});

-->



new code 2

<template>
  <div>
    <h1>Transaction Table</h1>
    <button @click="showForm = !showForm">{{ showForm ? 'Cancel' : 'Add Transaction' }}</button>
    <div v-if="showForm">
      <h2>{{ formId ? 'Edit Transaction' : 'Add Transaction' }}</h2>
      <form @submit.prevent="saveTransaction">
        <label for="AccountID">Account ID:</label>
        <input v-model="transaction.AccountID" type="number" id="AccountID" required />

        <label for="TransTypeID">Transaction Type ID:</label>
        <input v-model="transaction.TransTypeID" type="number" id="TransTypeID" required />

        <label for="Amount">Amount:</label>
        <input v-model="transaction.Amount" type="number" step="0.01" id="Amount" required />

        <label for="Date">Date:</label>
        <input v-model="transaction.Date" type="date" id="Date" required />

        <label for="Transaction_type">Transaction Type:</label>
        <input v-model="transaction.Transaction_type" type="text" id="Transaction_type" />

        <button type="submit">Save</button>
        <button @click="resetForm">Cancel</button>
      </form>
    </div>
    <ul>
      <li v-for="item in transactions" :key="item.TransID">
        {{ item.AccountID }} - {{ item.Amount }} - {{ item.Date }}
        <button @click="editTransaction(item.TransID)">Edit</button>
        <button @click="deleteTransaction(item.TransID)">Delete</button>
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  data() {
    return {
      transactions: [],
      transaction: {
        AccountID: '',
        TransTypeID: '',
        Amount: '',
        Date: '',
        Transaction_type: ''
      },
      showForm: false,
      formId: null
    };
  },
  methods: {
    fetchTransactions() {
      fetch('http://localhost:3000/transactions')
        .then(response => response.json())
        .then(data => {
          this.transactions = data;
        });
    },
    saveTransaction() {
      const method = this.formId ? 'PUT' : 'POST';
      const url = this.formId
        ? `http://localhost:3000/transactions/${this.formId}`
        : 'http://localhost:3000/transactions';

      fetch(url, {
        method: method,
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(this.transaction)
      }).then(() => {
        this.fetchTransactions();
        this.resetForm();
      });
    },
    editTransaction(id) {
      fetch(`http://localhost:3000/transactions/${id}`)
        .then(response => response.json())
        .then(data => {
          this.transaction = { ...data };
          this.formId = id;
          this.showForm = true;
        });
    },
    deleteTransaction(id) {
      fetch(`http://localhost:3000/transactions/${id}`, {
        method: 'DELETE'
      }).then(() => {
        this.fetchTransactions();
      });
    },
    resetForm() {
      this.transaction = { AccountID: '', TransTypeID: '', Amount: '', Date: '', Transaction_type: '' };
      this.showForm = false;
      this.formId = null;
    }
  },
  mounted() {
    this.fetchTransactions();
  }
};
</script>

<style>
/* Add your styles here */
</style>



asper SQL query


const express = require('express');
const sql = require('mssql');
const app = express();

app.use(express.json());

const config = {
  user: 'yourUsername',
  password: 'yourPassword',
  server: 'yourServer',
  database: 'yourDatabase',
  options: {
    encrypt: true, // Use this if you're on Windows Azure
    trustServerCertificate: true // Use this if your SQL server has a self-signed certificate
  }
};

app.get('/transactions', async (req, res) => {
  try {
    await sql.connect(config);
    const result = await sql.query('SELECT * FROM Transaction_Table');
    res.json(result.recordset);
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

app.post('/transactions', async (req, res) => {
  const { AccountID, TransTypeID, Amount, Date, Transaction_type } = req.body;
  try {
    await sql.connect(config);
    const result = await sql.query(`
      INSERT INTO Transaction_Table (AccountID, TransTypeID, Amount, Date, Transaction_type)
      VALUES (@AccountID, @TransTypeID, @Amount, @Date, @Transaction_type)
    `, {
      AccountID, TransTypeID, Amount, Date, Transaction_type
    });
    res.json(result);
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

app.put('/transactions/:id', async (req, res) => {
  const { id } = req.params;
  const { AccountID, TransTypeID, Amount, Date, Transaction_type } = req.body;
  try {
    await sql.connect(config);
    const result = await sql.query(`
      UPDATE Transaction_Table
      SET AccountID = @AccountID, TransTypeID = @TransTypeID, Amount = @Amount, Date = @Date, Transaction_type = @Transaction_type
      WHERE TransID = @id
    `, {
      AccountID, TransTypeID, Amount, Date, Transaction_type, id
    });
    res.json(result);
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

app.delete('/transactions/:id', async (req, res) => {
  const { id } = req.params;
  try {
    await sql.connect(config);
    const result = await sql.query(`DELETE FROM Transaction_Table WHERE TransID = @id`, { id });
    res.json(result);
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

const PORT = process.env.PORT || 3000;
app.listen(PORT, () => {
  console.log(`Server is running on port ${PORT}`);
});


updated view 



To align the `TransactionList.vue` with the styling and functionality of your `Lookup` component, here's a version of the `TransactionList.vue` that follows a similar structure. This example includes a navbar, CRUD operation buttons, a data table for displaying transactions, and forms for adding, editing, and deleting transactions.

### `TransactionList.vue`

```vue
<template>
  <div>
    <!-- Navbar -->
    <nav class="navbar">
      <div class="container">
        <!-- Transaction Table Button -->
        <button class="table-button" @click="showTransactionTable">Transaction Table</button>

        <!-- CRUD Operation Buttons -->
        <div v-if="showCrudButtons" class="crud-buttons">
          <button class="crud-button" @click="toggleForm('add')">Add Transaction</button>
          <button class="crud-button" @click="toggleForm('delete')">Delete Transaction</button>
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
              <th>TransID</th>
              <th>AccountID</th>
              <th>TransTypeID</th>
              <th>Amount</th>
              <th>Date</th>
              <th>Transaction Type</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="transaction in paginatedTransactions" :key="transaction.TransID">
              <td>{{ transaction.TransID }}</td>
              <td>{{ transaction.AccountID }}</td>
              <td>{{ transaction.TransTypeID }}</td>
              <td>{{ transaction.Amount }}</td>
              <td>{{ transaction.Date }}</td>
              <td>{{ transaction.Transaction_type }}</td>
              <td>
                <button class="action-button edit-button" @click="editTransaction(transaction)">Edit</button>
                <button class="action-button delete-button" @click="deleteTransaction(transaction.TransID)">Delete</button>
              </td>
            </tr>
          </tbody>
        </table>
        <div class="pagination" v-if="transactions.length > pageSize">
          <button class="pagination-button" @click="prevPage" :disabled="currentPage === 1">Previous</button>
          <button class="pagination-button" @click="nextPage" :disabled="currentPage === totalPages">Next</button>
          <span>Page {{ currentPage }} of {{ totalPages }}</span>
        </div>
      </div>

      <!-- Add Transaction Form -->
      <div class="form-container" v-if="showAddForm">
        <h2>Add New Transaction</h2>
        <form @submit.prevent="addTransaction">
          <input v-model="newTransaction.AccountID" placeholder="Account ID" required>
          <input v-model="newTransaction.TransTypeID" placeholder="Transaction Type ID" required>
          <input v-model="newTransaction.Amount" placeholder="Amount" type="number" step="0.01" required>
          <input v-model="newTransaction.Date" placeholder="Date" type="date" required>
          <input v-model="newTransaction.Transaction_type" placeholder="Transaction Type" required>
          <button class="action-button submit-button" type="submit">Add</button>
          <button class="action-button cancel-button" type="button" @click="cancelAdd">Cancel</button>
        </form>
      </div>

      <!-- Update Transaction Form -->
      <div class="form-container" v-if="showUpdateForm">
        <h2>Update Transaction</h2>
        <form @submit.prevent="updateTransaction">
          <input v-model="selectedTransaction.TransID" placeholder="TransID" readonly>
          <input v-model="selectedTransaction.AccountID" placeholder="Account ID" required>
          <input v-model="selectedTransaction.TransTypeID" placeholder="Transaction Type ID" required>
          <input v-model="selectedTransaction.Amount" placeholder="Amount" type="number" step="0.01" required>
          <input v-model="selectedTransaction.Date" placeholder="Date" type="date" required>
          <input v-model="selectedTransaction.Transaction_type" placeholder="Transaction Type" required>
          <button class="action-button submit-button" type="submit">Update</button>
          <button class="action-button cancel-button" type="button" @click="cancelUpdate">Cancel</button>
        </form>
      </div>

      <!-- Delete Transaction Form -->
      <div class="form-container" v-if="showDeleteForm">
        <h2>Delete Transaction</h2>
        <form @submit.prevent="deleteTransactionById">
          <input v-model="currentTransactionId" placeholder="Enter TransID to Delete" required>
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
      transactions: [],
      newTransaction: {
        AccountID: '',
        TransTypeID: '',
        Amount: '',
        Date: '',
        Transaction_type: ''
      },
      selectedTransaction: {},
      currentTransactionId: '',
      showForm: false,
      showTable: true,
      showAddForm: false,
      showUpdateForm: false,
      showDeleteForm: false,
      showCrudButtons: true,
      errorMsg: '',
      currentPage: 1,
      pageSize: 10
    };
  },
  computed: {
    paginatedTransactions() {
      const start = (this.currentPage - 1) * this.pageSize;
      return this.transactions.slice(start, start + this.pageSize);
    },
    totalPages() {
      return Math.ceil(this.transactions.length / this.pageSize);
    }
  },
  methods: {
    fetchTransactions() {
      fetch('http://localhost:3000/transactions')
        .then(response => response.json())
        .then(data => {
          this.transactions = data;
        })
        .catch(error => {
          this.errorMsg = error.message;
        });
    },
    addTransaction() {
      fetch('http://localhost:3000/transactions', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(this.newTransaction)
      })
        .then(() => {
          this.fetchTransactions();
          this.resetForm();
        })
        .catch(error => {
          this.errorMsg = error.message;
        });
    },
    editTransaction(transaction) {
      this.selectedTransaction = { ...transaction };
      this.showUpdateForm = true;
      this.showAddForm = false;
      this.showDeleteForm = false;
      this.showTable = false;
      this.showCrudButtons = false;
    },
    updateTransaction() {
      fetch(`http://localhost:3000/transactions/${this.selectedTransaction.TransID}`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(this.selectedTransaction)
      })
        .then(() => {
          this.fetchTransactions();
          this.resetForm();
        })
        .catch(error => {
          this.errorMsg = error.message;
        });
    },
    deleteTransaction(id) {
      fetch(`http://localhost:3000/transactions/${id}`, {
        method: 'DELETE'
      })
        .then(() => {
          this.fetchTransactions();
        })
        .catch(error => {
          this.errorMsg = error.message;
        });
    },
    deleteTransactionById() {
      this.deleteTransaction(this.currentTransactionId);
      this.resetForm();
    },
    toggleForm(formType) {
      this.resetForm();
      if (formType === 'add') {
        this.showAddForm = true;
      } else if (formType === 'delete') {
        this.showDeleteForm = true;
      }
      this.showTable = false;
      this.showCrudButtons = false;
    },
    toggleTable() {
      this.showTable = !this.showTable;
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
    },
    resetForm() {
      this.newTransaction = { AccountID: '', TransTypeID: '', Amount: '', Date: '', Transaction_type: '' };
      this.selectedTransaction = {};
      this.currentTransactionId = '';
      this.showAddForm = false;
      this.showUpdateForm = false;
      this.showDeleteForm = false;
      this.showTable = true;
      this.showCrudButtons = true;
    }
  },
  mounted() {
    this.fetchTransactions();
  }
};
</script>


<style scoped>
/* Navbar styles */
.navbar {
  background-color: #4CAF50; /* Dark green background */
  padding: 1rem;
  display: flex;
  justify-content: space-between;
  align-items: center;
  color: white;
}

.table-button, .crud-button {
  background-color: white;
  color: #4CAF50;
  border: none;
  padding: 0.5rem 1rem;
  cursor: pointer;
  border-radius: 4px;
  font-size: 1rem;
  margin-right: 10px;
}

.table-button:hover, .crud-button:hover {
  background-color: #45a049; /* Slightly darker green */
  color: white;
}

/* Content area styles */
.content {
  padding: 20px;
}

/* Error message styles */
.error-message {
  color: red;
  font-size: 1.2rem;
  margin-bottom: 20px;
}

/* Data table styles */
.data-container {
  margin-top: 20px;
  overflow-x: auto;
}

.data-table {
  width: 100%;
  border-collapse: collapse;
}

.data-table th, .data-table td {
  border: 1px solid #ddd;
  padding: 8px;
  text-align: left;
}

.data-table th {
  background-color: #f2f2f2;
  color: #333;
}

/* Action button styles */
.action-button {
  background-color: #4CAF50;
  color: white;
  border: none;
  padding: 5px 10px;
  cursor: pointer;
  border-radius: 4px;
  font-size: 0.9rem;
  margin-right: 5px;
}

.action-button:hover {
  background-color: #45a049;
}

/* Form container styles */
.form-container {
  margin-top: 20px;
}

form {
  display: flex;
  flex-direction: column;
}

form input {
  margin-bottom: 10px;
  padding: 8px;
  border: 1px solid #ddd;
  border-radius: 4px;
}

form button {
  padding: 10px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

.submit-button {
  background-color: #4CAF50;
  color: white;
}

.cancel-button {
  background-color: #f44336;
  color: white;
  margin-top: 10px;
}

.submit-button:hover, .cancel-button:hover {
  opacity: 0.9;
}

/* Pagination styles */
.pagination {
  display: flex;
  justify-content: center;
  align-items: center;
  margin-top: 20px;
}

.pagination-button {
  background-color: #4CAF50;
  color: white;
  border: none;
  padding: 5px 10px;
  cursor: pointer;
  border-radius: 4px;
  margin: 0 5px;
}

.pagination-button:disabled {
  background-color: #ddd;
  cursor: not-allowed;
}

.pagination-button:hover:not(:disabled) {
  background-color: #45a049;
}
</style>