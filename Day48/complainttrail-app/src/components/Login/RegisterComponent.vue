
  
  <template>
    <div>
      <h1>Register</h1>
      <input v-model="name" placeholder="Name" />
      <input v-model="username" placeholder="Username" />
      <input type="password" v-model="password" placeholder="Password" />
      <input v-model="email" placeholder="email" type="email" />
      <input v-model="date" type="datetime-local" style="width: 100px;"/>
      <!-- <input v-model="role" placeholder="role" type="" /> -->

     
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
      email:'',
      name:'',
      date:''
    };
  },
  methods: {
    Register(event) {
      event.preventDefault();
      Register(this.name,this.username, this.password,this.email,this.date)
        .then((response) => {
          alert(response.data.username + ' is registered');
        })
        .catch((err) => {
          alert(err.response);
        });
    },
  },
};
  </script>

  

  

  