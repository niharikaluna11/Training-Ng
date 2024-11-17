<template>
    <div>
      <h1>complaint -all</h1>
       <button @click="get()">Click To Fetch</button>
        <a>

        </a>
    </div>
  </template>
  
  <script>
import { getComplaintCategories } from '@/scripts/ComplainService';



  export default {
    name: 'GetAllComplaintComponent',
    data() {
      return {
        complaints: [],
      get() {
      getComplaintCategories().then(response => {
      this.complaints = response.data;
      console.log(this.complaints);
    
    })
    }
      };
    },  
  };
  </script>
  