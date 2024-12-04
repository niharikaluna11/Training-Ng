<template>
    <BaseHeader class="header" />
    <main class="main">
        <div class="responsive-wrapper">
            <div class="content-header">
                <div class="container">
                    <div class="help-box">
                        <h1>How can we help?</h1>
                        <p>Find answers or ask your questions here.</p>
                        <form id="help-form" @submit.prevent="handleSubmit">
                            <input type="email" id="email" name="email" placeholder="Enter your email" v-model="email"
                                required />
                            <textarea id="query" name="query" placeholder="Tell us your query" rows="5" v-model="query"
                                required></textarea>
                            <button type="submit">Send</button>
                        </form>
                        <p v-if="message" :class="{ success: isSuccess, error: !isSuccess }">
                            {{ message }}
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </main>
</template>

<script>
import { submitQuery } from '@/scripts/UserHelpDesk';
import BaseHeader from './BaseHeader.vue';
// Import the API function

export default {
    name: "HelpPage",
    components: {
        BaseHeader,
    },
    data() {
        return {
            email: "",
            query: "",
            message: null,
            isSuccess: false,
        };
    },
    methods: {
        async handleSubmit() {
            try {
                // Call the submitQuery function with the entered email and query
                await submitQuery(this.email, this.query);
                this.message = "Your query has been submitted successfully.";
                this.isSuccess = true;
                this.email = ""; // Reset form fields
                this.query = "";
            } catch (error) {
                // Extract and display the server's error message
                this.message = error.response?.data?.message || "An error occurred. Please try again.";
                this.isSuccess = false;
            }
        },
    },
};
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

/* Message Styles */
p.success {
    color: green;
}

p.error {
    color: red;
}
</style>
