// src/router/index.js
import { createRouter, createWebHistory } from 'vue-router';
import LookupTable from '@/components/LookupTable.vue';
import AccountTable from '@/components/AccountTable.vue';
import AccountHolderTable from '@/components/AccountHolderTable.vue';
import TransactionTable from '@/components/TransactionTable.vue';
import AccountHolderTable from '@/components/AccountHolderTable.vue';
// Import other components as needed

Vue.use(router);


// const routes = [
//     { path: '/lookups', component: LookupList },
//     { path: '/accounts', component: AccountList },
//     { path: '/addresses', component: AddressList },
//     { path: '/account-statuses', component: AccountStatusList },
//     // Add other routes as needed
// ];
export default new Router({
    routes: [
      {
        path: '/',
        name: 'Home',
        component: LookupTable,
      },
      {
        path: '/AccountTable',
        name: 'Account',
        component: AccountTable,
      },
      {
        path: '/account-status',
        name: 'AccountStatus',
        component: AccountHolderTable,

        path: '/TransactionTable',
        name: 'Transaction',
        component: TransactionTable,
      },
    ],

// const router = createRouter({
//     history: createWebHistory(),
//     routes,

});
// export default router;
