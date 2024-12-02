<script>
import axios from "axios"; // Import axios here
import { sendResetLink } from '@/scripts/ForgetPasswordService';
import { toast } from 'vue3-toastify'; // Add notifications
import BaseHeader from "../BaseHeader.vue";

export default {
  name: 'ForgotPassword',
  components: {
    BaseHeader,
  },
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
  mounted() {
    const isToken = sessionStorage.getItem("token");
    if (isToken) {
      this.isLogin = true;
      this.$router.push('/AdminDashboard')
    }
  },
};
</script>


<template>
  <BaseHeader class="header" />
  <main class="main">
    <div class="responsive-wrapper">

      <div class="content-header">
        <div class="content-header-layout">
          <img src="@/Images/ForgotPassword/forgotpasswordimg.jpg" alt="Login Page Illustration"
            class="content-image" />
          <div class="content-header-actions">
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
.header {
  height: 60px;
  padding-top: 0px;
}

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