<template>
  <header class="header">
    <div class="header-content responsive-wrapper">
      <div class="header-logo">
        <a href="#">
          <div>
            <img src="https://assets.codepen.io/285131/untitled-ui-icon.svg" />
          </div>
          <img src="https://assets.codepen.io/285131/untitled-ui.svg" />
          <!-- <img src="@/Images/logo_transparent.png" alt="Customer Service Agent"
            style="display: block;max-width: 30%;" /> -->
        </a>
      </div>
      <div class="header-navigation">
        <nav class="header-navigation-links">
          <a href="#"> <router-link to="/HomePage" class="nav-link active text-light" aria-current="page">
              Home
            </router-link> </a>

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
        <div class="content-header-intro">
          <div class="graphic">
            <img src="@/Images/RegisterPage1.png" alt="Register Page Illustration" />
          </div>
        </div>
        <div class="content-header-actions">
          <section class="hero">
            <h1 style="font-size: 3em; color: #9484c4;">Get Started!</h1>
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
              <input v-model="date" type="date" class="input-field" id="date" />
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
              <button class="login-button">
                <router-link to="login">Sign In</router-link>
              </button>
            </div>
          </section>

        </div>
      </div>
    </div>
  </main>


</template>


<script>
import { Register } from "@/scripts/RegisterService";
import { toast } from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';

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
    async Register(event) {
      event.preventDefault();  // Prevent default form submission

      const newdate = new Date(this.date).toISOString();  // Format date correctly

      try {
        const response = await Register(
          this.fname,
          this.lname,
          this.username,
          this.password,
          this.email,
          newdate,
          this.role,
          this.organizationType
        );

        // Handle successful registration
        if (response.status === 200) {
          this.fname = "",
            this.lname = "",
            this.username = "",
            this.password = "",
            this.email = "",
            this.role = "",
            this.organizationType = "",
            toast.success(
              `${response.data.data.username} is registered `,
              {
                rtl: true,
                limit: 2,
                position: toast.POSITION.TOP_CENTER,
              }
            );
        } else {

          toast.error(
            `${response.data.response.data.errorMessage || "An unexpected error occurred during registration."}`,
            {
              rtl: true,
              limit: 2,
              position: toast.POSITION.TOP_RIGHT,
            }
          );
        }
      } catch (err) {
        // Handle any unexpected errors
        console.error('Registration error:', err);
        toast.error(
          `Registration failed: ${err.response.data.errorMessage || "An unexpected error occurred."}`,
          {
            rtl: true,
            limit: 2,
            position: toast.POSITION.TOP_RIGHT,
          }
        );
      }
    },
    checkRole() {
      // Check if the role is not equal to 2, reset organizationType
      if (this.role !== 2) {
        this.organizationType = "";
      }
    }
  }
};
</script>

<style scoped>
.content-header {
  padding-top: 0px;
}

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
  padding-top: 0px;
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
  gap: 15px;
  /* Space between First Name and Last Name */
}

.input-container {
  flex: 1;
}

/* Role and Organization Select in One Line */
.role-group {
  display: flex;
  gap: 15px;
  /* Space between Role and Organization Type */
}

/* Button Group (half width, in one line) */
.button-group {
  display: flex;
  justify-content: space-between;
  gap: 10px;
}

.login-button {
  width: 60%;
  /* Button takes up almost half of the width */
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
</style>
