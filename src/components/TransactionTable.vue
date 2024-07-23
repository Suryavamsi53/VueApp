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

