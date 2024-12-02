<template>
    <BaseHeader class="header" />
    <main class="main">
        <div class="responsive-wrapper">
            <div class="content-header">

                <div class="container">
                    <div class="help-box">
                        <h1>How can we help?</h1>
                        <p>Find answers or ask your questions here.</p>
                        <form id="help-form">
                            <input type="email" id="email" name="email" placeholder="Enter your email" required>
                            <textarea id="query" name="query" placeholder="Tell us your query" rows="5"
                                required></textarea>
                            <button type="submit">Send</button>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </main>
</template>

<script>
import BaseHeader from './BaseHeader.vue';

export default {
    name: "HelpPage",
    components: {
        BaseHeader,
    },
}
</script>

<style scoped>
.main {
    padding: 0px;
}

body {
    background-color: #fff;
}


/* Container Styles */
.container {
    /* max-width: 500px; */
    width: 100%;
    margin-top: 100px;
}

/* Help Box Styles */
.help-box {
    background-color: white;
    padding: 40px;
    border-radius: 10px;
    box-shadow: 0px 5px 20px rgba(0, 0, 0, 0.1);
    text-align: center;
}

.help-box h1 {
    font-size: 30px;
    color: #1E2A48;
    margin-bottom: 15px;
}

.help-box p {
    font-size: 16px;
    color: #4E5D6B;
    margin-bottom: 30px;
}

/* Form Styles */
form {
    display: flex;
    flex-direction: column;
}

input,
textarea {
    padding: 12px;
    margin: 8px 0;
    border: 1px solid #ddd;
    border-radius: 5px;
    font-size: 16px;
}

textarea {
    resize: vertical;
}

/* Button Styles */
button {
    padding: 12px;
    background-color: #1E2A48;
    color: white;
    border: none;
    border-radius: 5px;
    font-size: 18px;
    cursor: pointer;
}

button:hover {
    background-color: #3E4C7A;
}
</style>