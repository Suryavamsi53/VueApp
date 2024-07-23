<template>
  <div class="container">
    <header>
      <h1>Data Driven UI</h1>
    </header>
    <nav>
      <button class="show-table-button" @click="showTable('LookupTable')">Lookup Table</button>
      <button class="show-table-button" @click="showTable('AccountStatusTable')">Account Status Table</button>
      <button class="show-table-button" @click="showTable('AddressTable')">Address Table</button>
      <button class="show-table-button" @click="showTable('AccountTypeTable')">Account Type Table</button>
    </nav>
    <div>
      <LookupTable v-if="currentTable === 'LookupTable'" />
      <AccountStatusTable v-if="currentTable === 'AccountStatusTable'" />
      <AddressTable v-if="currentTable === 'AddressTable'" />
      <AccountTypeTable v-if="currentTable === 'AccountTypeTable'" />
    </div>
  </div>
</template>

<script>
import LookupTable from './components/LookupTable.vue';
import AccountStatusTable from './components/AccountStatusTable.vue';
import AddressTable from './components/AddressTable.vue';
import AccountTypeTable from './components/AccountTypeTable.vue';

export default {
  name: 'App',
  components: {
    LookupTable,
    AccountStatusTable,
    AddressTable,
    AccountTypeTable,
  },
  data() {
    return {
      currentTable: '', // Initialize currentTable
    };
  },
  methods: {
    showTable(table) {
      this.currentTable = table;
    },
  },
};
</script>

<style scoped>
.container {
  font-family: sans-serif;
  background-color: #f5f5f5;
  color: #333;
  padding: 20px;
  margin-bottom: 50px; /* added margin to make room for fixed footer */
}

header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 20px;
}

h1 {
  margin: 0;
  margin-top: 50px;
}

.show-table-button {
  background-color: #666;
  color: white;
  border: none;
  padding: 0.5rem 1rem;
  margin-right: 0.5rem;
  cursor: pointer;
}

.show-table-button:hover {
  background-color: #555;
}
</style>



<!-- <template>
  <div class="container">
    <header>
      <h1>Data Driven UI</h1>
       Remove commented router-link section if not needed 
    </header>
    <nav>
      <button class="show-table-button" @click="showTable('LookupTable')">Lookup Table</button>
      <button class="show-table-button" @click="showTable('AccountHolderTable')">Account Status Table</button>
      <button class="show-table-button" @click="showTable('Address_Table')">Address Table</button>
      <button class="show-table-button" @click="showTable('accountType')">Account Type Table</button>
    </nav>
    <div>
      <LookupTable v-if="currentTable === 'Lookup_Table'" />
      <AccountTable v-if="currentTable === 'Account_Status'" />
      <AccountHolderTable v-if="currentTable === 'Address_Table'" />
      <TransactionTable v-if="currentTable === 'accountType'" />
    </div>
  </div>
</template>

<script>
 

import AccountHolderTable from './components/AccountHolderTable.vue';
import AccountTable from './components/AccountTable.vue';
import LookupTable from './components/LookupTable.vue';
import TransactionTable from './components/TransactionTable.vue';
 

export default {
  name: 'App',
  components: {
    LookupTable,
    AccountTable,
    AccountHolderTable,
    TransactionTable,

 
   
  },
  data() {
    return {
      currentTable: '', // Initialize currentTable
    };
  },
  methods: {
    showTable(table) {
      this.currentTable = table;
    },
  },
};
</script>

<style scoped>
.container {
  font-family: sans-serif;
  background-color: #f5f5f5;
  color: #333;
  padding: 20px;
  margin-bottom: 50px; /* added margin to make room for fixed footer */
}

header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 20px;
}

h1 {
  margin: 0;
  margin-top: 50px;
}

.show-table-button {
  background-color: #666;
  color: white;
  border: none;
  padding: 0.5rem 1rem;
  margin-right: 0.5rem;
  cursor: pointer;
}

.show-table-button:hover {
  background-color: #555;
}

.crud-buttons {
  margin-top: 1rem;
}

.crud-button {
  background-color: #888;
  color: white;
  border: none;
  padding: 0.5rem 1rem;
  margin-right: 0.5rem;
  cursor: pointer;
}

.crud-button:hover {
  background-color: #777;
}
</style> -->

























<!--This is main code -->

<!-- <template>
  <div class="container">
    <header>
      <h1>Data Driven UI</h1>
      
    </header>
    <nav>
      <button class="show-table-button" @click="showTable('LookupTable')">Lookup Table</button>
      <button class="show-table-button" @click="showTable('AccountTable')">Account Status Table</button>
      <button class="show-table-button" @click="showTable('AccountHolderTable')">Address Table</button>
      <button class="show-table-button" @click="showTable('TransactionTable')">Account Type Table</button>
    </nav>
    <div>
      <Lookup_Table v-if="currentTable === 'Lookup_Table'" />
      <Account_Status v-if="currentTable === 'Account_Status'" />
      <Address_Table v-if="currentTable === 'Address_Table'" />
      <AccountTypeTable v-if="currentTable === 'accountType'" />
    </div>
  </div>
  <LookupTable>
  </LookupTable>
  <AccountTable></AccountTable>
  <AccountHolderTable></AccountHolderTable>
 <TransactionTable></TransactionTable>
</template>

<script>
 
import AccountHolderTable from './components/AccountHolderTable.vue';
import AccountTable from './components/AccountTable.vue';
import LookupTable from './components/LookupTable.vue';
import TransactionTable from './components/TransactionTable.vue';
 

export default {
  name: 'App',
  components: {
    LookupTable,
    AccountTable,
    AccountHolderTable,
    TransactionTable,

 
   
  },
  data() {
    return {
      currentTable: '', // Initialize currentTable
    };
  },
  methods: {
    showTable(table) {
      this.currentTable = table;
    },
  },
};
</script>

<style scoped>
.container {
  font-family: sans-serif;
  background-color: #f5f5f5;
  color: #333;
  padding: 20px;
  margin-bottom: 50px; /* added margin to make room for fixed footer */
}

header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 20px;
}

h1 {
  margin: 0;
  margin-top: 50px;
}

.show-table-button {
  background-color: #666;
  color: white;
  border: none;
  padding: 0.5rem 1rem;
  margin-right: 0.5rem;
  cursor: pointer;
}

.show-table-button:hover {
  background-color: #555;
}

.crud-buttons {
  margin-top: 1rem;
}

.crud-button {
  background-color: #888;
  color: white;
  border: none;
  padding: 0.5rem 1rem;
  margin-right: 0.5rem;
  cursor: pointer;
}

.crud-button:hover {
  background-color: #777;
}
</style> -->




























<!-- <template>
  <div id="app">
    <header>
      <nav>
        <ul>
          <li><button @click="navigate('LookupTable')">Lookups</button></li>
          <li><button @click="navigate('AccountTable')">Accounts</button></li>
          <li><button @click="navigate('addresses')">Addresses</button></li>
          <li><button @click="navigate('account-holders')">Account Holders</button></li>
          <li><button @click="navigate('transactions')">Transactions</button></li>
        </ul>
      </nav>
    </header>
    <main>
      <router-view></router-view>
    </main>
   
  </div>
  <LookupTable></LookupTable>
  <AccountHolderTable></AccountHolderTable>
  <AccountTable></AccountTable>
</template>

<script>
import AccountHolderTable from './components/AccountHolderTable.vue';
import AccountTable from './components/AccountTable.vue';
import LookupTable from './components/LookupTable.vue';

export default {
  name: 'App',
  components: {
   LookupTable,
   AccountTable,
   AccountHolderTable,
  },

  methods: {

    navigate(page) {
      this.$router.push({ name: page });
    }
  }
};
</script>

<style>
/* Add your CSS styles here */
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}

header {
  background: #42b983;
  color: white;
  padding: 1rem;
}

nav ul {
  list-style: none;
  padding: 0;
}

nav li {
  display: inline;
  margin: 0 10px;
}

button {
  background: none;
  border: none;
  color: white;
  cursor: pointer;
}

footer {
  background: #35495e;
  color: white;
  padding: 1rem;
  position: absolute;
  bottom: 0;
  width: 100%;
}
</style> -->
