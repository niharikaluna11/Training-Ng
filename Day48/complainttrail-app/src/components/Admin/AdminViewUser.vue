<template>
    <div class="responsive-wrapper">
        <!-- Main Header -->
        <div class="main-header">
            <h1>G'day, {{ userName }}</h1>
        </div>

        <h1>All Users</h1>

        <!-- Display all users' profiles -->
        <div v-if="users.length > 0">
            <div v-for="user in users" :key="user.id" class="user-profile">
                <h3>{{ user.firstName }} {{ user.lastName }}</h3>
                <div>
                    <p>Email: {{ user.email }}</p>
                    <p>Phone: {{ user.phone }}</p>
                    <p>Address: {{ user.address }}</p>
                </div>
                <div>
                    <button>edit</button>
                    <button>view</button>
                    <button>delete</button>
                </div>


            </div>
        </div>
        <div v-else>
            <p v-if="errorMessage">{{ errorMessage }}</p>
            <p v-else>No users found.</p>
        </div>
    </div>
</template>

<script>
import { getAllUsersProfiles } from '@/scripts/GetALL/GetAllprofile'; // Import the function to get users

export default {
    name: 'AdminViewUser',
    data() {
        return {
            userName: "", // Reactive property to store the user's name
            users: [], // Store the list of users fetched from the API
            errorMessage: "", // Store any error message
        };
    },
    mounted() {
        // Retrieve `given_name` from sessionStorage
        const name = sessionStorage.getItem("username");
        if (name) {
            this.userName = name; // Set the name to the reactive property
        } else {
            this.userName = "Admin"; // Fallback if name is not found
        }

        // Fetch all users' profiles when the component is mounted
        this.fetchUsers();
    },
    methods: {
        async fetchUsers() {
            try {
                // Fetch the users from the API
                const usersData = await getAllUsersProfiles(); // Assuming this function is imported
                if (usersData && usersData.$values && usersData.$values.length > 0) {
                    this.users = usersData.$values; // Extract users from the $values array
                } else {
                    this.errorMessage = "No users found."; // Set error message if no data returned
                }
            } catch (error) {
                // Handle API request error
                this.errorMessage = "Failed to fetch users. Please try again later.";
                console.error("Error fetching users' profiles:", error);
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

.user-profile {
    margin-bottom: 15px;
    border: 1px solid #ccc;
    padding: 10px;
    border-radius: 5px;
}

.user-profile h3 {
    margin: 0;
    font-size: 1.2em;
}

.user-profile p {
    margin: 5px 0;
}

.user-profile img {
    width: 50px;
    height: 50px;
    border-radius: 50%;
    object-fit: cover;
    margin-top: 10px;
}
</style>
