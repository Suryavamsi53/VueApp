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
  