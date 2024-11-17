<template>
  <div>
    <h1>Register</h1>
    <input v-model="fname" placeholder="First Name" />
    <input v-model="lname" placeholder="Last Name" />
    <input v-model="username" placeholder="Username" />
    <input type="password" v-model="password" placeholder="Password" />
    <input v-model="email" placeholder="Email" type="email" />
    <input v-model="date" type="datetime-local" style="width: 100px;" />

    <!-- Role dropdown -->
    <select v-model="role" @change="checkRole">
      <option disabled value="">Select Role</option>
      <option :value="0">Admin</option>
      <option :value="1">User</option>
      <option :value="2">Organization</option>
    </select>

    <!-- Conditional type dropdown for Organization role -->
    <div v-if="role === 2">
      <select v-model="organizationType">
        <option disabled value="">Select Type</option>
        <option :value="1">Company</option>
        <option :value="2">Government</option>
        <option :value="3">Agent</option>
      </select>
    </div>

    <button @click="Register">Register</button>
  </div>
</template>

<script>
import { Register } from '@/scripts/RegisterService';

export default {
  name: 'RegisterComponent',
  data() {
    return {
      username: '',
      password: '',
      email: '',
      fname: '',
      lname:'',
      date: '',
      role: '',
      organizationType: ''
    };
  },
  methods: {
    Register(event) {
      event.preventDefault();
      Register(this.fname,this.lname, this.username, this.password, this.email, this.date, this.role, this.organizationType)
        .then((response) => {
          alert(response.data.username + ' is registered');
        })
        .catch((err) => {
          alert(err.response);
        });
    },
    checkRole() {
      if (this.role !== 2) {
        this.organizationType = 1; 
      }
    }
  }
};
</script>
