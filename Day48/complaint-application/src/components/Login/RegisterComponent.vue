<script>
import { Register, SendOtp } from "@/scripts/RegisterService";
import { toast } from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';
import BaseHeader from "../BaseHeader.vue";
export default {
  name: "RegisterComponent",
  components: {
    BaseHeader,
  },
  data() {
    return {
      fname: "",
      lname: "",
      username: "",
      password: "",
      email: "",
      otp: "",
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
        this.otp.trim() &&
        this.date.trim() &&
        this.role !== "" &&
        (this.role !== 2 || this.organizationType !== "")
      );
    }
  },
  methods: {
    // Send OTP method
    async sendOtp() {
      if (!this.email) {
        toast.error("Please enter a valid email address.");
        return;
      }
      try {
        // Call SendOtp function and pass the email
        const response = await SendOtp(this.email);

        // If OTP is successfully sent
        if (response.status === 200) {
          toast.success("OTP sent successfully!", {
            rtl: true,
            limit: 2,
            position: toast.POSITION.TOP_CENTER,
          });
        }
      } catch (error) {
        // Handle any error from SendOtp function
        console.error("Error sending OTP:", error);
        toast.error("Failed to send OTP.", {
          rtl: true,
          limit: 2,
          position: toast.POSITION.TOP_RIGHT,
        });
      }
    },

    // Register method
    async Register(event) {
      event.preventDefault();  // Prevent default form submission

      const newdate = new Date(this.date).toISOString();  // Format the date correctly

      try {
        const response = await Register(
          this.fname,
          this.lname,
          this.username,
          this.password,
          this.email,
          this.otp,
          newdate,
          this.role,
          this.organizationType
        );

        // Handle successful registration
        if (response.status === 200) {
          this.fname = "";
          this.lname = "";
          this.username = "";
          this.password = "";
          this.email = "";
          this.role = "";
          this.organizationType = "";
          toast.success(
            `${response.data.data.username} is registered.`,
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
        console.error("Registration error:", err);
        toast.error(
          `Registration failed: ${err.response?.data?.errorMessage || "An unexpected error occurred."}`,
          {
            rtl: true,
            limit: 2,
            position: toast.POSITION.TOP_RIGHT,
          }
        );
      }
    },

    // Method to check role and reset organizationType if needed
    checkRole() {
      if (this.role !== 2) {
        this.organizationType = "";
      }
    }
  },
  mounted() {
    const isToken = sessionStorage.getItem("token");
    if (isToken) {
      this.isLogin = true;
      this.$router.push('/AdminDashboard')
    }
  }
};
</script>

<template>
  <BaseHeader class="header" />


  <main class="main">
    <div class="responsive-wrapper">
      <div class="content-header">
        <div class="content-header-intro">
          <div class="graphic">
            <img src="@/Images/signupvector.avif" alt="Register Page Illustration" />
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
              <button type="button" @click="sendOtp" class="send-otp-btn" style="float: right;">
                Send OTP
              </button>
            </div>



            <div class="form-group">
              <input v-model="otp" type="text" placeholder="Otp" required class="input-field" />
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

              <div v-if="role === 2" class="form-group" style="flex: 1;">
                <select v-model="organizationType" class="input-field">
                  <option disabled value="">Select Type</option>
                  <option :value="1">Company</option>
                  <option :value="2">Government</option>
                  <option :value="3">Agent</option>
                </select>
              </div>
            </div>


            <div class="button-group">
              <button :disabled="!isFormValid" @click="Register" class="login-button">Sign Up</button>
              <router-link to="login"> <button class="login-button">
                  Sign In
                </button></router-link>
            </div>
          </section>

        </div>
      </div>
    </div>
  </main>


</template>


<style scoped>
.header {
  height: 60px;
  padding-top: 0px;
}

.main {
  padding-top: 2rem;
}


.content-header-actions {
  flex: 1;
  width: 100%;
  /* Ensure full width without extra gaps */
  max-width: 500px;
  /* Optional to restrict the content size */
  margin: 0 auto;
  /* Center the container */
}

/* Image styling in the content */
.graphic img {
  border-radius: 10px;
  max-width: 100%;
  max-height: 400px;
  width: auto;
  height: auto;
  object-fit: contain;
  padding-left: 60px;
  /* Ensures the image retains its aspect ratio */
}

.content-header {
  padding-top: 0px;
  margin: 0px;
  margin-right: 0px;
}

/* Hero Section Styling */
.hero {
  flex: 1;
  text-align: left;
  max-width: 500px;
  padding-left: 0px;
  padding-right: 0px;
  padding-top: 0px;
  margin-right: 0px;
}

/* Hero Section */
section.hero {
  display: flex;
  flex-direction: column;
  align-items: center;
  text-align: center;
}

.hero h1 {
  margin-top: 10px;
  font-size: 2rem;
  margin-bottom: 20px;
  color: #9484c4;
}

/* Form Groups (Spacing and Layout) */
.form-group {
  margin-bottom: 10px;
  width: 100%;
  position: relative;
  /* Used for "Send OTP" button placement */
}

/* Input Fields */
.input-field {
  width: 100%;
  height: 40px;
  /* Consistent height across all inputs */
  padding: 10px;
  font-size: 1rem;
  border: 1px solid #ccc;
  border-radius: 5px;
  outline: none;
  box-sizing: border-box;
}

.input-field:focus {
  border-color: #9484c4;
  box-shadow: 0 0 5px rgba(148, 132, 196, 0.5);
}

/* Name Group (First Name and Last Name in one line) */
.name-group {
  display: flex;
  gap: 15px;
  /* Space between First Name and Last Name */
}

.input-container {
  flex: 1;
  /* Ensures equal width for both input fields */
}

/* Email Field with "Send OTP" Button */
.form-group input[type="email"] {
  padding-right: 120px;
  /* Space for the "Send OTP" button */
}

.form-group input[type="email"]:focus {
  border-color: #9484c4;
  box-shadow: 0 0 5px rgba(148, 132, 196, 0.5);
}

/* "Send OTP" Button Styling */
.send-otp-btn {
  position: absolute;
  top: 50%;
  right: 10px;
  transform: translateY(-50%);
  background-color: #9484c4;
  color: white;
  font-size: 0.9rem;
  border: none;
  border-radius: 5px;
  padding: 6px 10px;
  cursor: pointer;
  transition: background-color 0.3s;
}

.send-otp-btn:hover {
  background-color: #7d6abf;
}

.send-otp-btn:disabled {
  background-color: #ccc;
  cursor: not-allowed;
}

/* Role and Organization Select Fields */
.role-group {
  display: flex;
  gap: 15px;
  /* Space between Role and Organization dropdowns */
}

.role-group select {
  flex: 1;
  /* Ensures equal width for both dropdowns */
}

/* General Dropdown Styling */
.form-group select {
  width: 100%;
  height: 40px;
  /* Consistent height with input fields */
  padding: 10px;
  font-size: 1rem;
  border: 1px solid #ccc;
  border-radius: 5px;
  outline: none;
  box-sizing: border-box;
}

.form-group {
  margin-bottom: 15px;
  width: 100%;
}

.form-group select:focus {
  border-color: #9484c4;
  box-shadow: 0 0 5px rgba(148, 132, 196, 0.5);
}

/* Button Group Styling */
.button-group {
  display: flex;
  justify-content: space-between;
  gap: 10px;
}

.login-button {
  width: 200px;
  /* Buttons take up most of the width */
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
