<template>
    <div class="responsive-wrapper">
        <!-- Main Header -->
        <div class="main-header">
            <h1>G'day, {{ userName }}</h1>
        </div>

        <h1>All Organizations</h1>

        <!-- Display all organizations' names -->
        <div v-if="organizations.length > 0">
            <div v-for="organization in organizations" :key="organization.id" class="organization-profile">
                <h3>{{ organization.name }}</h3>
                <div>
                    <button>edit</button>
                    <button>view</button>
                    <button>delete</button>
                </div>
            </div>
        </div>
        <div v-else>
            <p v-if="errorMessage">{{ errorMessage }}</p>
            <p v-else>No organizations found.</p>
        </div>
    </div>
</template>

<script>
import { getAllOrganizationsProfiles } from '@/scripts/GetALL/GetAllprofile'; // Import the function to get organizations

export default {
    name: 'AdminViewOrg',
    data() {
        return {
            userName: "", // Reactive property to store the user's name
            organizations: [], // Store the list of organizations fetched from the API
            errorMessage: "", // Store any error message
        };
    },
    mounted() {
        // Retrieve `given_name` from sessionStorage
        const name = sessionStorage.getItem("username");
        if (name) {
            this.userName = name; // Set the name to the reactive property
        } else {
            this.userName = "Admin";
        }
        this.fetchOrganizations();
    },
    methods: {
        async fetchOrganizations() {
            try {
                const organizationsData = await getAllOrganizationsProfiles(); // Assuming this function is imported
                if (organizationsData && organizationsData.$values && organizationsData.$values.length > 0) {
                    this.organizations = organizationsData.$values;
                } else {
                    this.errorMessage = "No organizations found."; // Set error message if no data returned
                }
            } catch (error) {
                // Handle API request error
                this.errorMessage = "Failed to fetch organizations. Please try again later.";
                console.error("Error fetching organizations' profiles:", error);
            }
        }
    },
};
</script>

<style scoped>
/* Add your styles here */

.responsive-wrapper {
    padding: 20px;
}

.main-header {
    margin-bottom: 20px;
}

.organization-profile {
    margin-bottom: 15px;
    border: 1px solid #ccc;
    padding: 10px;
    border-radius: 5px;
}

.organization-profile h3 {
    margin: 0;
    font-size: 1.2em;
}
</style>
