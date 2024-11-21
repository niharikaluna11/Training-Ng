<script>
import { toast } from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';

import { login } from '@/scripts/LoginService';
import { jwtDecode } from 'jwt-decode';



export default {
  name: 'LoginComponent',
  data() {
    return {
      username: '',
      password: '',
      login: async () => {
        event.preventDefault();
        console.log(this.username, this.password)
        const data = await login(this.username, this.password);
        console.log(data);
        if (data.status == 200) {
          this.username = '';
          this.password = '';

          // Store the token in sessionStorage
          sessionStorage.setItem("token", data.data.data.token);

          // Retrieve the token from sessionStorage
          const token = sessionStorage.getItem("token");
          toast.success(
            `${data.data.data.username} is logged in`,
            {
              rtl: true,
              limit: 2,
              position: toast.POSITION.TOP_CENTER,
            }
          );

          if (token) {
            try {
              // Decode the token
              const decode = jwtDecode(token);
              console.log("Decoded Token:", decode);

              // Validate the token expiration
              const currentTime = Math.floor(Date.now() / 1000); // Current time in seconds
              if (decode.exp && decode.exp < currentTime) {
                console.error('Token has expired');
                sessionStorage.removeItem('token');
                this.$router.push('/login'); // Redirect to login
              } else {
                // Check the user's role
                if (decode.role === 'Admin') {
                  console.log('Welcome, ABHI K LIE USER BUT ADMIN!');
                  this.$router.push('/UserDashboard'); // Redirect Admin users
                } else if (decode.role === 'User') {
                  console.log('Welcome, User!');
                  this.$router.push('/UserDashboard'); // Redirect regular users
                }
                else {
                  console.log('Welcome, ABHI K LIE USER BUT ORG!');
                  this.$router.push('/UserDashboard'); // Redirect regular users
                }
              }
            } catch (error) {
              console.error("Error decoding token:", error);
            }
          } else {
            console.error("No token found in sessionStorage");
          }




        }
        else {
          console.log(data);
          // alert("Login failed: " + data.response.data.errorMessage);
          toast.error(
            `${data.response.data.errorMessage}`,
            {
              rtl: true,
              limit: 2,
              position: toast.POSITION.TOP_RIGHT,
            },
          );
        }
      }
    };
  },
  computed: {
    isFormValid() {
      return this.username.trim() !== "" && this.password.trim() !== "";
    },
  },
  methods: {

  },
};
</script>

<template>

  <div class="login-page">
    <header class="header">
      <div class="logo">
        <img src="@/Images/logo_transparent.png" alt="Customer Service Logo" />
      </div>
      <nav class="navbar">
        <a href="#">
          <router-link to="/HomePage" class="nav-link active text-light" aria-current="page">
            Home
          </router-link></a>
      </nav>
    </header>
    <main class="main-content">
      <div class="graphic">
        <img src="@/Images/loginPage.jpg" alt="Login Page Illustration" />
      </div>
      <section class="hero">
        <h1 class="welcome-message" style="font-size: 3em; color: #9484c4;">Welcome Back!</h1>

        <div class="form-group">
          <input v-model="username" type="text" placeholder="Username or Email Address" required class="input-field" />
        </div>
        <div class="form-group">
          <input v-model="password" type="password" placeholder="Password" required class="input-field" />
        </div>

        <div class="form-options">
          <div class="checkbox-group">
            <input type="checkbox" id="keep-logged-in" />
            <label for="keep-logged-in">Keep me logged in</label>
          </div>
          <router-link to="forgot-password" class="forgot-password-link">Forgot Password?</router-link>
        </div>

        <button :disabled="!isFormValid" @click="login" class="login-button">
          Login
        </button>

        <p class="signup-message">
          Don’t have an account yet?
          <router-link to="register" class="signup-link">Sign Up</router-link>
        </p>
      </section>
    </main>


  </div>
</template>





<style scoped>
/* General Styles */
* {
  margin: 0;
  padding: 0;
  font-family: sans-serif;
  box-sizing: border-box;
}

body {
  font-family: "Arial", sans-serif;
  color: #333;
}

/* Consolidated Hero Section */
.hero {
  display: flex;
  flex-direction: column;
  align-items: center;
  text-align: center;
  max-width: 400px;
  margin: auto;
  padding: 20px;
}

.hero h1 {
  font-size: 2.5rem;
  margin-bottom: 30px;
  font-weight: bold;
  color: #333;
}

.hero .form-group {
  margin-bottom: 15px;
  width: 100%;
}

.welcome-message {
  font-size: 2.5rem;
  margin-bottom: 30px;
  font-weight: bold;
  color: #333;
}

.form-group {
  margin-bottom: 15px;
  width: 100%;
}

.input-field {
  width: 100%;
  padding: 10px;
  font-size: 1rem;
  border: 1px solid #ccc;
  border-radius: 5px;
  outline: none;
}

.input-field:focus {
  border-color: #9484c4;
  box-shadow: 0 0 5px rgba(148, 132, 196, 0.5);
}

.form-options {
  display: flex;
  justify-content: space-between;
  align-items: center;
  width: 100%;
  margin-bottom: 15px;
}

.checkbox-group {
  display: flex;
  align-items: center;
}

.checkbox-group input[type="checkbox"] {
  margin-right: 8px;
}

.forgot-password-link {
  text-decoration: none;
  font-size: 0.9rem;
  color: #9484c4;
}

.forgot-password-link:hover {
  text-decoration: underline;
}

.login-button {
  width: 100%;
  padding: 12px;
  background-color: #9484c4;
  color: white;
  font-size: 1.1rem;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s;
}

.login-button:hover {
  background-color: #7d6abf;
}

.login-button:disabled {
  background-color: #ccc;
  cursor: not-allowed;
}

.signup-message {
  margin-top: 15px;
  font-size: 0.9rem;
  color: #555;
}

.signup-link {
  color: #9484c4;
  font-weight: bold;
  text-decoration: none;
}

.signup-link:hover {
  text-decoration: underline;
}

/* Responsive Design */
@media (max-width: 768px) {
  .hero {
    max-width: 100%;
  }

  .welcome-message {
    font-size: 2rem;
  }

  .login-button {
    font-size: 1rem;
  }
}

@media (max-width: 480px) {
  .welcome-message {
    font-size: 1.8rem;
  }

  .input-field {
    font-size: 0.9rem;
  }

  .login-button {
    font-size: 0.9rem;
  }
}

/* Header */
.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 10px 20px;
  background-color: #fff;
  height: 6rem;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.logo {
  font-size: 1.5em;
  font-weight: bold;
  color: #555;
  background: transparent;
}

.logo img {
  width: 120px;
  height: auto;
  transition: transform 0.5s;
}

.logo img:hover {
  transform: scale(1.1);
}

/* Main Content */
.main-content {
  display: flex;
  align-items: center;
  justify-content: space-around;
  flex-wrap: wrap;
  margin: 5px auto;
  padding: 5px 5px;
  max-width: 1200px;
  gap: 5px;
}

/* Graphic Section */
.graphic img {
  max-width: 100%;
  height: auto;
  border-radius: 10px;
}


.form-group {
  margin-bottom: 15px;
}

.input-field {
  width: 100%;
  padding: 10px;
  font-size: 1rem;
  border: 1px solid #ccc;
  border-radius: 5px;
  outline: none;
}

.input-field:focus {
  border-color: #9484c4;
  box-shadow: 0 0 5px rgba(148, 132, 196, 0.5);
}

.login-button {
  background-color: #9484c4;
  color: white;

  font-size: 1rem;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s;
}

.login-button:hover {
  background-color: #7d6abf;
}

.login-button:disabled {
  background-color: #ccc;
  cursor: not-allowed;
}

/* Navigation */
.navigation {
  display: flex;
  align-items: center;
  gap: 20px;
}

.register-container {
  display: flex;
  flex-direction: column;
  align-items: flex-start;
  /* Ensures "New User?" appears above "Register" */
}

.new-user {
  font-size: 0.9rem;
  color: #555;
  margin-bottom: 5px;
}

.nav-link {
  text-decoration: none;
  color: #9484c4;
  font-weight: bold;
  transition: color 0.3s;
}

.nav-link:hover {
  color: #7d6abf;
}

.forgot-password {
  margin-left: auto;
  /* Pushes "Forgot Password" to the right if needed */
  font-size: 0.9rem;
}

.nav-link {
  text-decoration: none;
  color: #9484c4;
  font-weight: bold;
}

.nav-link:hover {
  text-decoration: underline;
}

/* Responsive Design */
@media (max-width: 768px) {
  .main-content {
    flex-direction: column;
    text-align: center;
  }

  .hero {
    max-width: 100%;
  }

  .hero h1 {
    font-size: 2rem;
  }
}

@media (max-width: 480px) {
  .hero h1 {
    font-size: 1.8rem;
  }

  .input-field {
    font-size: 0.9rem;
  }

  .login-button {
    font-size: 0.9rem;
  }
}
</style>
