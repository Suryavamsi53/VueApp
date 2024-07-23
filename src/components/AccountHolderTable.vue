<template>
    <div>
      <h1>Account Holder Table</h1>
      <button @click="showForm = !showForm">Add Account Holder</button>
      <div v-if="showForm">
        <h2>{{ formId ? 'Edit Account Holder' : 'Add Account Holder' }}</h2>
        <form @submit.prevent="saveAccountHolder">
          <label for="AccNum">Account Number:</label>
          <input v-model="accountHolder.AccNum" type="text" id="AccNum" required />
          
          <label for="AccTypeID">Account Type ID:</label>
          <input v-model="accountHolder.AccTypeID" type="number" id="AccTypeID" />
  
          <label for="Acc_holders_N">Account Holder Name:</label>
          <input v-model="accountHolder.Acc_holders_N" type="text" id="Acc_holders_N" />
  
          <label for="AC_Balance">Account Balance:</label>
          <input v-model="accountHolder.AC_Balance" type="number" id="AC_Balance" />
  
          <label for="CD">Creation Date:</label>
          <input v-model="accountHolder.CD" type="date" id="CD" />
  
          <button type="submit">Save</button>
          <button @click="resetForm">Cancel</button>
        </form>
      </div>
      <ul>
        <li v-for="item in accountHolders" :key="item.AccountHolderID">
          {{ item.AccNum }} - {{ item.Acc_holders_N }}
          <button @click="editAccountHolder(item.AccountHolderID)">Edit</button>
          <button @click="deleteAccountHolder(item.AccountHolderID)">Delete</button>
        </li>
      </ul>
    </div>
  </template>
  
  <script>
  export default {
    data() {
      return {
        accountHolders: [],
        accountHolder: {
          AccNum: '',
          AccTypeID: '',
          Acc_holders_N: '',
          AC_Balance: '',
          CD: ''
        },
        showForm: false,
        formId: null
      };
    },
    methods: {
      fetchAccountHolders() {
        fetch('http://localhost:3000/account_holders')
          .then(response => response.json())
          .then(data => {
            this.accountHolders = data;
          });
      },
      saveAccountHolder() {
        const method = this.formId ? 'PUT' : 'POST';
        const url = this.formId
          ? `http://localhost:3000/account_holders/${this.formId}`
          : 'http://localhost:3000/account_holders';
  
        fetch(url, {
          method: method,
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(this.accountHolder)
        }).then(() => {
          this.fetchAccountHolders();
          this.resetForm();
        });
      },
      editAccountHolder(id) {
        fetch(`http://localhost:3000/account_holders/${id}`)
          .then(response => response.json())
          .then(data => {
            this.accountHolder = { ...data };
            this.formId = id;
            this.showForm = true;
          });
      },
      deleteAccountHolder(id) {
        fetch(`http://localhost:3000/account_holders/${id}`, {
          method: 'DELETE'
        }).then(() => {
          this.fetchAccountHolders();
        });
      },
      resetForm() {
        this.accountHolder = { AccNum: '', AccTypeID: '', Acc_holders_N: '', AC_Balance: '', CD: '' };
        this.showForm = false;
        this.formId = null;
      }
    },
    mounted() {
      this.fetchAccountHolders();
    }
  };
  </script>
  
  <style>
  /* Add your styles here */
  </style>
  