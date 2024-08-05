import axios from '../axios.js';

methods: {
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
  }
}




<!--    
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
    // config.headers.Authorization = `Bearer ${token}`;
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

-->