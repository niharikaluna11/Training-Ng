<template>
  <div class="register-page">
    <header class="header">
      <div class="logo">
        <img src="@/Images/logo_transparent.png" alt="Customer Service Logo" />
      </div>
      <nav class="navbar">
        <a href="#">
          <router-link to="/HomePage" class="nav-link active text-light" aria-current="page">
            Home
          </router-link>
        </a>
      </nav>
    </header>

    <main class="main-content">
      <div class="graphic">
        <img src="@/Images/RegisterPage1.png" alt="Register Page Illustration" />
      </div>

      <section class="hero">
        <h1>Get Started!</h1>
        <div class="form-group name-group">
          <div class="input-container">
            <input v-model="fname" type="text" placeholder="First Name" required class="input-field" />
          </div>
          <div class="input-container">
            <input v-model="lname" type="text" placeholder="Last Name" required class="input-field" />
          </div>
        </div>

        <div class="form-group">
          <input v-model="username" type="text" placeholder="Username" required class="input-field" />
        </div>

        <div class="form-group">
          <input v-model="password" type="password" placeholder="Password" required class="input-field" />
        </div>

        <div class="form-group">
          <input v-model="email" type="email" placeholder="Email" required class="input-field" />
        </div>

        <div class="form-group">
          <input v-model="date" type="datetime-local" class="input-field" />
        </div>

        <div class="form-group role-group">
          <select v-model="role" @change="checkRole" class="input-field">
            <option disabled value="">Select Role</option>
            <option :value="0">Admin</option>
            <option :value="1">User</option>
            <option :value="2">Organization</option>
          </select>
        </div>

        <div v-if="role === 2" class="form-group">
          <select v-model="organizationType" class="input-field">
            <option disabled value="">Select Type</option>
            <option :value="1">Company</option>
            <option :value="2">Government</option>
            <option :value="3">Agent</option>
          </select>
        </div>

        <div class="button-group">
          <button :disabled="!isFormValid" @click="Register" class="login-button">Sign Up</button>
          <button class="login-button" >
            <router-link to="login" >Sign In</router-link>
          </button>
        </div>
      </section>



    </main>
  </div>
</template>



<script>
import { Register } from "@/scripts/RegisterService";

export default {
  name: "RegisterComponent",
  data() {
    return {
      fname: "",
      lname: "",
      username: "",
      password: "",
      email: "",
      date: "",
      role: "",
      organizationType: ""
    };
  },
  computed: {
    isFormValid() {
      return (
        this.fname.trim() &&
        this.lname.trim() &&
        this.username.trim() &&
        this.password.trim() &&
        this.email.trim() &&
        this.date.trim() &&
        this.role !== "" &&
        (this.role !== 2 || this.organizationType !== "")
      );
    }
  },
  methods: {
    Register(event) {
      event.preventDefault();
      Register(
        this.fname,
        this.lname,
        this.username,
        this.password,
        this.email,
        this.date,
        this.role,
        this.organizationType
      )
        .then((response) => {
          alert(response.data.username + " is registered");
        })
        .catch((err) => {
          console.log(err);
          alert(err.response);
        });
    },
    checkRole() {
      if (this.role !== 2) {
        this.organizationType = "";
      }
    }
  }
};
</script>



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
  max-width: 1200px;
}

/* Graphic Section */
.graphic img {
  border-radius: 10px;
  max-width: 100%;
  max-height: 400px;
  /* Adjust this value as needed */
  width: auto;
  height: auto;
  object-fit: contain;
  /* Ensures the image retains its aspect ratio */
}


/* Hero Section */
.hero {
  flex: 1;
  text-align: left;
  max-width: 400px;
  padding: 20px;
}

.hero h1 {
  font-size: 2.5rem;
  margin-bottom: 20px;
}
/* General Form Group */
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

/* First Name and Last Name in One Line */
.name-group {
  display: flex;
  gap: 15px; /* Space between First Name and Last Name */
}

.input-container {
  flex: 1;
}

/* Role and Organization Select in One Line */
.role-group {
  display: flex;
  gap: 15px; /* Space between Role and Organization Type */
}

/* Button Group (half width, in one line) */
.button-group {
  display: flex;
  justify-content: space-between;
  gap: 10px;
}

.login-button {
  width: 60%; /* Button takes up almost half of the width */
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



/* Navigation */
.navigation {
  margin-top: 20px;
  display: flex;
  gap: 15px;
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

@media (max-width: 768px) {
  .graphic img {
    max-height: 200px;
  }
}

@media (max-width: 480px) {
  .graphic img {
    max-height: 150px;
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
