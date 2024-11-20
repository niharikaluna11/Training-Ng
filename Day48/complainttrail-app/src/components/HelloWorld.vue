<template>
  <div class="homePage">
    <header class="header">
      <div class="logo" ><img src="@/Images/logo_transparent.png" alt="Customer Service Agent" /></div>
      <nav class="navbar">
        <a href="#about">About</a>
        <a href="#news">News</a>
        <a href="#pricing">Pricing</a>
        <a href="#blog">Blog</a>
        <a href="#signup" class="signup">Sign Up</a>
      </nav>
    </header>
    <main class="main-content">
      <section class="hero">
        <h1>24/7 Open Customer Service</h1>
        <p>
          Lorem ipsum dolor sit amet, lorem tacimates mel te. Pri in iriure
          eligendi aliquando, no est facer semper deserunt.
        </p>
        <a href="#learn-more" class="learn-more-btn">Learn More</a>
      </section>
      <div class="graphic">
        <img src="@/Images/client-service.png" alt="Customer Service Agent" />
      </div>
    </main>
  </div>
</template>

<script>
export default {
  name: "HelloWorld",
};
</script>

<style scoped>


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
  padding: 5px 20px; /* Adjusted padding to reduce the distance */
  background: transparent;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); /* Adding subtle shadow to separate the header from the body */
}

.logo {
  font-size: 1.5em;
  font-weight: bold;
  color: #555;
}

.navbar {
  display: flex;
  gap: 20px; /* Adjusted gap to bring links closer together */
}

.navbar a {
  text-decoration: none;
  color: #555;
  transition: color 0.3s;
}

.navbar .signup {
  padding: 8px 18px; /* Slightly smaller padding for a more compact design */
  background-color: #4285f4;
  color: white;
  border-radius: 5px;
}

.navbar a:hover {
  color: #4285f4;
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
  padding: 40px; /* Reduced padding to bring content closer to the header */
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
  background-color: #4285f4;
  color: white;
  border-radius: 5px;
  transition: background-color 0.3s;
}

.learn-more-btn:hover {
  background-color: #357ae8;
}

/* Graphic Section Styling */
.graphic {
  flex: 1 1 45%;
  display: flex;
  justify-content: center;
  align-items: center;
}

.graphic img {
  width: 100%;
  height: auto;
  object-fit: contain;
}

/* Responsive Design */
@media (max-width: 768px) {
  .header {
    flex-direction: column;
    align-items: flex-start;
    padding: 15px; /* Adjusted padding for mobile devices */
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
    padding: 12px; /* Smaller padding for mobile */
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
