<script>
import axios from "axios"; // Import axios here
import { sendResetLink } from '@/scripts/ForgetPasswordService';
import { toast } from 'vue3-toastify'; // Add notifications

export default {
  name: 'ForgotPassword',
  data() {
    return {
      username: '',
      otp: '',
      newPassword: '',
      confirmNewPassword: '',
      isOtpSent: false,
    };
  },
  computed: {
    isFormValid() {
      return this.username.trim() !== '';
    },
    isResetFormValid() {
      return (
        this.otp.trim() !== '' &&
        this.newPassword.trim() !== '' &&
        this.confirmNewPassword.trim() !== '' &&
        this.newPassword === this.confirmNewPassword
      );
    },
  },
  methods: {
    async handleSendResetLink() {
      try {
        const response = await sendResetLink(this.username);
        if (response.status === 200) {
          toast.success("Reset link sent successfully!", {
            position: toast.POSITION.TOP_CENTER,
            autoClose: 60000, 
          });
          this.isOtpSent = true;
        } else {
          toast.error(response?.data?.message || "Failed to send reset link.", {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 60000, 
          });
        }
      } catch (err) {
        console.error("Error sending reset link:", err);
        toast.error(err?.response?.data?.message || "An unexpected error occurred.", {
          position: toast.POSITION.TOP_RIGHT,
        });
      }
    },
    async handleResetPassword() {
      try {
        const resetData = {
          usernameOrEmail: this.username,
          otp: this.otp,
          newPassword: this.newPassword,
          confirmNewPassword: this.confirmNewPassword,
        };

        const response = await axios.post(
          `http://localhost:5062/api/ForgotPassword/reset-password`,
          resetData
        );

        if (response.status === 200) {
          toast.success("Password reset successfully!", {
            position: toast.POSITION.TOP_CENTER,
            autoClose: 60000,
          });
          this.$router.push("/login"); // Redirect to login
        } else {
          toast.error(response?.data?.message || "Failed to reset password.", {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 60000, 
          });
        }
      } catch (err) {
        console.error("Error resetting password:", err);
        toast.error(err?.response?.data?.message || "An unexpected error occurred.", {
          position: toast.POSITION.TOP_RIGHT,
        });
      }
    },
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
          <img src="@/Images/ForgotPassword/forgotpasswordimg.jpg" alt="Login Page Illustration"
            class="content-image" />

          <!-- Conditional Rendering for the Form -->
          <div class="content-header-actions">
            <!-- Show Initial Form if OTP is not sent -->
            <section v-if="!isOtpSent" class="hero">
              <h1 class="welcome-message" style="font-size: 3em; color: #9484c4;">
                Forgot your Password!
              </h1>

              <div class="form-group">
                <input v-model="username" type="text" placeholder="Username or Email Address" required
                  class="input-field" />
              </div>

              <button class="login-button" :disabled="!isFormValid" @click="handleSendResetLink">
                Send Reset Link
              </button>
            </section>

            <!-- Show Reset Password Form if OTP is sent -->
            <section v-else class="hero">
              <h1 class="welcome-message" style="font-size: 3em; color: #9484c4;">
                Reset Your Password
              </h1>

              <div class="form-group">
                <input v-model="otp" type="text" placeholder="Enter OTP" required class="input-field" />
              </div>

              <div class="form-group">
                <input v-model="newPassword" type="password" placeholder="New Password" required class="input-field" />
              </div>

              <div class="form-group">
                <input v-model="confirmNewPassword" type="password" placeholder="Confirm New Password" required
                  class="input-field" />
              </div>

              <button class="login-button" :disabled="!isResetFormValid" @click="handleResetPassword">
                Reset Password
              </button>
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

/* Flexbox container for the image and text */
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
.content-image {
  width: 20%;
  /* Adjust as needed */
  height: auto;
  border-radius: 10px;
}

/* Ensure the text section aligns well */
.content-header-actions {
  max-width: 400px;
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
</style>