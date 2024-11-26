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

              if (decode.given_name) {
                sessionStorage.setItem("username", decode.given_name);
              }

              console.log("Decoded Token:", decode);

              //http://schemas.microsoft.com/ws/2008/06/identity/claims/role

              if (decode["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] == 'Admin') {
                console.log('Welcome, ADMIN!');
                this.$router.push('/AdminDashboard'); // Redirect Admin users
              }
              else if (decode["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] == 'User') {
                console.log('Welcome, User!');
                this.$router.push('/UserDashboard'); // Redirect regular users
              }
              else if (decode["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] == 'Organization') {
                console.log('Welcome, ORG');
                this.$router.push('/OrganizationDashboard'); // Redirect regular users
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
  <header class="header">
    <div class="header-content responsive-wrapper">
      <div class="header-logo">
        <a href="#">
          <div>
            <img src="https://assets.codepen.io/285131/untitled-ui-icon.svg" />
          </div>
          <img src="https://assets.codepen.io/285131/untitled-ui.svg" />
        </a>
      </div>
      <div class="header-navigation">
        <nav class="header-navigation-links">
          <a href="#">
            <router-link to="/HomePage" class="nav-link active text-light" aria-current="page">
              Home
            </router-link>
          </a>
        </nav>
        <div class="header-navigation-actions">
          <a href="#" class="icon-button">
            <i class="ph-gear-bold"></i>
          </a>
          <a href="#" class="icon-button">
            <i class="ph-bell-bold"></i>
          </a>
          <a href="#" class="avatar">
            <img src="@/Images/profilepicimg.jpg" alt="profile">
          </a>
        </div>
      </div>
      <a href="#" class="button">
        <i class="ph-list-bold"></i>
        <span>Menu</span>
      </a>
    </div>
  </header>

  <main class="main">
    <div class="responsive-wrapper">
      <div class="content-header">
        <!-- Flexbox Container for Image and Text -->
        <div class="content-header-layout">
          <!-- Image Section -->
          <img src="@/Images/loginPage.jpg" alt="Login Page Illustration" />

          <!-- Text Section -->
          <div class="content-header-actions">
            <section class="hero">
              <h1 class="welcome-message" style="font-size: 3em; color: #9484c4;">Welcome Back!</h1>
              <div class="form-group">
                <input v-model="username" type="text" placeholder="Username or Email Address" required
                  class="input-field" />
              </div>
              <div class="form-group">
                <input v-model="password" type="password" placeholder="Password" required class="input-field" />
              </div>
              <div class="form-options">
                <div class="checkbox-group">
                  <input type="checkbox" id="keep-logged-in" />
                  <label for="keep-logged-in">Keep me logged in</label>
                </div>
                <router-link to="ForgotPassword" class="forgot-password-link">Forgot Password?</router-link>
              </div>
              <button :disabled="!isFormValid" @click="login" class="login-button">Login</button>
              <p class="signup-message">
                Don’t have an account yet?
                <router-link to="Register">Sign Up!</router-link>
              </p>
            </section>
          </div>
        </div>
      </div>
    </div>
  </main>
</template>




<style scoped>
.content-header {
  padding-top: 0px;
  display: flex;
  flex-direction: column;
}

.content-header-layout {
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 20px;
  /* Space between image and text */
  flex-wrap: wrap;
  /* Adjust for smaller screens */
  padding: 20px;
}


/* Image Styling */
.content-header-layout {
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 20px;
  /* Space between image and text */
  flex-wrap: wrap;
  /* Adjust for smaller screens */
  padding: 20px;
}

/* Ensure the text section aligns well */
/* .content-header-actions {
  max-width: 400px;
} */
.content-header-actions {
  flex: 1;
  /* Take up equal space */
  max-width: 50%;
  /* Limit the width of the form container */
}

/* Hero Section */
section.hero {
  display: flex;
  flex-direction: column;
  align-items: center;
  text-align: center;
}

.hero h1 {
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
  color: #3e3e3e;
}


.graphic img {
  max-width: 100%;
  height: auto;
  border-radius: 10px;
}

.hero {
  display: flex;
  flex-direction: column;
  align-items: center;
  text-align: center;
  padding: 20px;
}

.welcome-message {
  font-size: 2rem;
  margin-bottom: 20px;
  font-weight: bold;
  color: #333;
}

.form-group {
  margin-bottom: 15px;
  width: 100%;
}

.input-field {
  width: 80%;
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


Section .hero {
  display: flex;
  flex-direction: column;
  align-items: center;
  text-align: center;
  max-width: 400px;
  margin: auto;
  padding: 20px;
  padding-top: 0px;
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
  color: #3e3e3e;
}






.graphic img {
  max-width: 100%;
  height: auto;
  border-radius: 10px;
}


.form-group {
  margin-bottom: 15px;
}


.navigation {
  display: flex;
  align-items: center;
  gap: 20px;
}

.register-container {
  display: flex;
  flex-direction: column;
  align-items: flex-start;
}

.new-user {
  font-size: 0.9rem;
  color: #555;
  margin-bottom: 5px;
}
</style>
