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