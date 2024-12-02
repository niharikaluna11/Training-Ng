<template>
  <BaseHeader class="header" />
  <main class="main">
    <div class="responsive-wrapper">
      <div class="content-header">
        <div class="content-header-intro">
          <section class="hero">
            <h1>24/7 Customer Support</h1>
            <p>Got a problem? We're here to help you anytime!</p>
            <a href="#learn-more" class="learn-more-btn">Contact Us!</a>
          </section>
        </div>
        <div class="content-header-actions">
          <img src="@/Images/client-service.png" alt="Customer Service Agent" />
        </div>
      </div>
    </div>
  </main>

</template>

<script>
import BaseHeader from './BaseHeader.vue';

export default {
  name: "HelloWorld",
  components: {
    BaseHeader,
  },
};
</script>

<style scoped>
.header {
  height: 60px;
  padding-top: 0px;
}

.contact-section {
  margin-top: 40px;
  padding: 20px;
  border-radius: 10px;
}

.contact-form {
  margin-top: 20px;
  display: flex;
  flex-direction: column;
  gap: 20px;
}

.form-group {
  display: flex;
  flex-direction: column;
}

label {
  font-size: 1.1em;
  margin-bottom: 5px;
  color: #555;
}

input,
textarea {
  padding: 10px;
  font-size: 1em;
  border: 1px solid #ccc;
  border-radius: 5px;
}

textarea {
  resize: none;
}

.submit-btn {
  padding: 10px 20px;
  font-size: 1em;
  background-color: #9484c4;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.submit-btn:hover {
  background-color: #cac1e4;
}
</style>
