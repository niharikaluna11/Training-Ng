<template>
  <div class="homePage">
    <header class="header">
      <div class="logo">
        <img src="@/Images/logo_transparent.png" alt="Customer Service Agent" />
      </div>
      <nav class="navbar">
        <a href="#" > 
          <router-link to="/HomePage" class="nav-link active text-light" aria-current="page" >
            Home
          </router-link></a>
        <a href="#About">About</a>
        <!-- <a href="#" class="signup"> -->
          <router-link to="/Login" class="nav-link active text-light signup" aria-current="page" >
            Sign Up
          </router-link>
      </nav>
    </header>
    <main class="main-content">
      <section class="hero">
        <h1>24/7 Customer Support</h1>
        <p>Got a problem? We're here to help you anytime!</p>
        <a href="#learn-more" class="learn-more-btn">Contact Us!</a>
      </section>
      <div class="graphic">
        <img src="@/Images/client-service.png" alt="Customer Service Agent" />
      </div>
    </main>
   
    <div id="About">
  <main class="main-content">
    <div class="graphic">
      <img src="" alt="Customer Service Agent" />
    </div>
    <section class="hero">
      <h1>About Our Services</h1>
      <p>We provide top-notch customer service round the clock.</p>
    </section>
  </main>
</div>
<div id="learn-more" class="contact-section">
  <main class="main-content">
    <section class="hero">
      <h1>Contact Us</h1>
      <p>We are here to assist you. Reach out to us by filling out the form below.</p>
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
    </section>
    <div class="graphic">
      <img src="@/Images/designcustomerservice.png" alt="Customer Service Agent" />
    </div>
  </main>
</div>

  </div>
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

* {
  padding: 0;
  margin: 0;
  font-family: sans-serif;
  box-sizing: border-box;
}

body {
  background: linear-gradient(to right, #f3f4f6, #fdfdff);
  color: #333;
  margin: 0;
}

/* Header Styling */
.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 10px 20px;
  /* Reduced padding for compact design */
  background: transparent;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  height: 6rem;
}

.logo {
  font-size: 1.5em;
  font-weight: bold;
  color: #555;
  background: transparent;
}

.logo img {
  width: 120px;
  /* Slightly smaller logo */
  height: auto;
  border-radius: 6px;
  /* Optional: Add rounded corners if needed */
  transition: transform 0.5s ease;
  /* Optional: Add hover effect */
}

/* Optional hover effect */
.logo img:hover {
  transform: scale(1.1);
  /* Slight zoom effect on hover */
}

.navbar {
  display: flex;
  gap: 8px;
}

.navbar a {
  text-decoration: none;
  color: #555;
  padding: 5px 10px;
  /* Optional: Add padding for consistent spacing */
  transition: color 0.2s ease;
}

.navbar .signup {
  padding: 6px 12px;
  /* Compact padding for sign-up button */
  background-color: #9484c4;
  color: white;
  border-radius: 5px;
}

.navbar a:hover {
  background-color: #cac1e4;
}

/* Logo Styling */
.logo img {
  width: 150px;
  height: auto;
}

/* Main Content Styling */
.main-content {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 40px;
  /* Reduced padding to bring content closer to the header */
  flex-wrap: wrap;
  gap: 30px;
}

/* Hero Section Styling */
.hero {
  max-width: 50%;
  text-align: left;
  flex: 1 1 45%;
}

.hero h1 {
  font-size: 2.5em;
  color: #333;
  margin-bottom: 20px;
}

.hero p {
  font-size: 1.2em;
  margin-bottom: 20px;
  color: #666;
}

.learn-more-btn {
  padding: 10px 20px;
  text-decoration: none;
  background-color: #9484c4;
  color: white;
  border-radius: 5px;
  transition: background-color 0.3s;
}

.learn-more-btn:hover {
  background-color: #cac1e4;
}

/* Graphic Section Styling */
.graphic {
  flex: 1 1 45%;
  display: flex;
  justify-content: center;
  align-items: center;
}

/* .graphic img {
  width: 100%;
  height: auto;
  object-fit: contain;
} */

.graphic img {
  max-width: 100%;
  height: auto;
  display: block;
  margin: 0 auto;
}

/* Responsive Design */
@media (max-width: 768px) {
  .header {
    flex-direction: column;
    align-items: flex-start;
    padding: 15px;
    /* Adjusted padding for mobile devices */
  }

  .navbar {
    flex-wrap: wrap;
    gap: 10px;
  }

  .main-content {
    flex-direction: column;
    text-align: center;
    padding: 20px;
  }

  .hero {
    max-width: 100%;
    text-align: center;
    margin-bottom: 20px;
  }

  .hero h1 {
    font-size: 2em;
  }

  .hero p {
    font-size: 1em;
  }

  .graphic img {
    max-width: 80%;
  }
}

@media (max-width: 480px) {
  .header {
    padding: 12px;
    /* Smaller padding for mobile */
  }

  .navbar {
    gap: 5px;
  }

  .hero h1 {
    font-size: 1.8em;
  }

  .hero p {
    font-size: 0.9em;
  }

  .learn-more-btn {
    font-size: 0.9em;
    padding: 8px 16px;
  }

  .graphic img {
    max-width: 250px;
  }
}
</style>
