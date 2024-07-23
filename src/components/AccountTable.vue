<template>
    <div>
      <h1>Account Table</h1>
      <button @click="showForm = !showForm">Add Account</button>
      <div v-if="showForm">
        <h2>{{ formId ? 'Edit Account' : 'Add Account' }}</h2>
        <form @submit.prevent="saveAccount">
          <label for="AccountNumber">Account Number:</label>
          <input v-model="account.AccountNumber" type="text" id="AccountNumber" required />
          
          <label for="AccountStatus_id">Account Status ID:</label>
          <input v-model="account.AccountStatus_id" type="number" id="AccountStatus_id" />
  
          <button type="submit">Save</button>
          <button @click="resetForm">Cancel</button>
        </form>
      </div>
      <ul>
        <li v-for="item in accounts" :key="item.AccId">
          {{ item.AccountNumber }} - {{ item.AccountStatus_id }}
          <button @click="editAccount(item.AccId)">Edit</button>
          <button @click="deleteAccount(item.AccId)">Delete</button>
        </li>
      </ul>
    </div>
  </template>
  
  <script>
  export default {
    data() {
      return {
        accounts: [],
        account: {
          AccountNumber: '',
          AccountStatus_id: ''
        },
        showForm: false,
        formId: null
      };
    },
    methods: {
      fetchAccounts() {
        fetch('http://localhost:3000/accounts')
          .then(response => response.json())
          .then(data => {
            this.accounts = data;
          });
      },
      saveAccount() {
        const method = this.formId ? 'PUT' : 'POST';
        const url = this.formId
          ? `http://localhost:3000/accounts/${this.formId}`
          : 'http://localhost:3000/accounts';
  
        fetch(url, {
          method: method,
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(this.account)
        }).then(() => {
          this.fetchAccounts();
          this.resetForm();
        });
      },
      editAccount(id) {
        fetch(`http://localhost:3000/accounts/${id}`)
          .then(response => response.json())
          .then(data => {
            this.account = { ...data };
            this.formId = id;
            this.showForm = true;
          });
      },
      deleteAccount(id) {
        fetch(`http://localhost:3000/accounts/${id}`, {
          method: 'DELETE'
        }).then(() => {
          this.fetchAccounts();
        });
      },
      resetForm() {
        this.account = { AccountNumber: '', AccountStatus_id: '' };
        this.showForm = false;
        this.formId = null;
      }
    },
    mounted() {
      this.fetchAccounts();
    }
  };
  </script>
  
  <style>
  /* Add your styles here */
  </style>
  