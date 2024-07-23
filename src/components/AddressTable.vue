<template>
    <div>
      <h1>Address Table</h1>
      <button @click="showForm = !showForm">Add Address</button>
      <div v-if="showForm">
        <h2>{{ formId ? 'Edit Address' : 'Add Address' }}</h2>
        <form @submit.prevent="saveAddress">
          <label for="AccountID">Account ID:</label>
          <input v-model="address.AccountID" type="number" id="AccountID" required />
          
          <label for="AddressTypeID">Address Type ID:</label>
          <input v-model="address.AddressTypeID" type="number" id="AddressTypeID" />
  
          <label for="Address">Address:</label>
          <input v-model="address.Address" type="text" id="Address" />
  
          <button type="submit">Save</button>
          <button @click="resetForm">Cancel</button>
        </form>
      </div>
      <ul>
        <li v-for="item in addresses" :key="item.AddressID">
          {{ item.AccountID }} - {{ item.Address }}
          <button @click="editAddress(item.AddressID)">Edit</button>
          <button @click="deleteAddress(item.AddressID)">Delete</button>
        </li>
      </ul>
    </div>
  </template>
  
  <script>
  export default {
    data() {
      return {
        addresses: [],
        address: {
          AccountID: '',
          AddressTypeID: '',
          Address: ''
        },
        showForm: false,
        formId: null
      };
    },
    methods: {
      fetchAddresses() {
        fetch('http://localhost:3000/addresses')
          .then(response => response.json())
          .then(data => {
            this.addresses = data;
          });
      },
      saveAddress() {
        const method = this.formId ? 'PUT' : 'POST';
        const url = this.formId
          ? `http://localhost:3000/addresses/${this.formId}`
          : 'http://localhost:3000/addresses';
  
        fetch(url, {
          method: method,
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(this.address)
        }).then(() => {
          this.fetchAddresses();
          this.resetForm();
        });
      },
      editAddress(id) {
        fetch(`http://localhost:3000/addresses/${id}`)
          .then(response => response.json())
          .then(data => {
            this.address = { ...data };
            this.formId = id;
            this.showForm = true;
          });
      },
      deleteAddress(id) {
        fetch(`http://localhost:3000/addresses/${id}`, {
          method: 'DELETE'
        }).then(() => {
          this.fetchAddresses();
        });
      },
      resetForm() {
        this.address = { AccountID: '', AddressTypeID: '', Address: '' };
        this.showForm = false;
        this.formId = null;
      }
    },
    mounted() {
      this.fetchAddresses();
    }
  };
  </script>
  
  <style>
  /* Add your styles here */
  </style>
  