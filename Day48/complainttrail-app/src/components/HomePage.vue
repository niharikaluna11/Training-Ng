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
          <router-link to="/HomePage" class="nav-link active text-light" aria-current="page">
            <a href="#">Home </a>
          </router-link>
          <a href="#About"> About </a>
          <a href="#Feature"> Features </a>
          <a href="#learn-more"> Contact us </a>

        </nav>
        <div class="header-navigation-actions">
         
          <router-link to="/Login" class="nav-link active text-light signup" aria-current="page">
            <a href="#" class="button">
            <i class="ph-lightning-bold"></i>

              <span>Sign Up</span>
           
          </a>
        </router-link>
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

  <main class="main">
    <div class="responsive-wrapper">
      <div class="content-header">
        <div class="content-header-intro">

          <img src="" alt="Customer Service Agent" />

        </div>
        <div class="content-header-actions">

          <h1>About Our Services</h1>
          <p>We provide top-notch customer service round the clock.</p>

        </div>
      </div>
    </div>
  </main>


  <main class="main">
    <div class="responsive-wrapper">
      <div class="content-header">
        <div class="content-header-intro">

          <h1>Contact Us</h1>
          <!-- <p>We are here to assist you. Reach out to us by filling out the form below.</p> -->
          <form class="contact-form">
            <div class="form-group">
              <label for="email">Email:</label>
              <input type="email" id="email" placeholder="Enter your email" required />
            </div>
            <div class="form-group">
              <label for="message">Message:</label>
              <textarea id="message" rows="5" placeholder="Write your message here..." required></textarea>
            </div>
            <button type="submit" class="submit-btn">Send Message</button>
          </form>

        </div>
        <div class="content-header-actions">
          <img src="@/Images/client-service.png" alt="Customer Service Agent" style="width: min-content;" />

        </div>
      </div>
    </div>
  </main>




</template>

<script>

export default {
  name: "HelloWorld",
};
</script>




<style scoped>
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
