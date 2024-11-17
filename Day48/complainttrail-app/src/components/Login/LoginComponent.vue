
<template>
    <div>
      <h1>Login</h1>
      <input v-model="username" placeholder="Username" />
      <input type="password" v-model="password" placeholder="Password" />
      <button @click="login">Login</button>
      <nav>
        <router-link to="register">Register</router-link> 
      </nav>
      <nav>
        <router-link to="complaint">complaint-app</router-link> 
      </nav>
    </div>
  </template>
  
 
  <script>
import { login } from '@/scripts/LoginService';

export default {
  name: 'LoginComponent',
  data() {
    return {
      username: '',
      password: '',
    };
  },
  methods: {
    login(event) {
      event.preventDefault();
      login(this.username, this.password)
        .then((response) => {
          sessionStorage.setItem("token", response.data.token);
          alert(response.data.username + ' is logged in');
        })
        .catch((err) => {
          alert(err.response.data);
        });
    },
  },
};
  </script>
  