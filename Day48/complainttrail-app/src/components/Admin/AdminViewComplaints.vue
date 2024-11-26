<template>
    <div class="responsive-wrapper">
        <!-- Main Header -->
        <div class="main-header">
            <h1>G'day, {{ userName }}</h1>
        </div>

        <h1>All Complaints</h1>

        <!-- Display all complaints -->
        <div v-if="complaints.length > 0">
            <div v-for="complaint in complaints" :key="complaint.id" class="complaint-profile">
                <h3>Complaint ID: {{ complaint.id }}</h3>
                <p><strong>Description:</strong> {{ complaint.description }}</p>
                <p><strong>Status:</strong> {{ getStatusText(complaint.status) }}</p>
                <p><strong>Priority:</strong> {{ getPriorityText(complaint.priority) }}</p>
                <p><strong>Last Updated:</strong> {{ formatDate(complaint.lastUpdated) }}</p>
                <div>
                    <button>Edit</button>
                    <button>View</button>
                </div>
            </div>
        </div>
        <div v-else>
            <p v-if="errorMessage">{{ errorMessage }}</p>
            <p v-else>No complaints found.</p>
        </div>
    </div>
</template>

<script>
import { getAllComplaints } from '@/scripts/GetALL/GetAllComplaints'; // Import the function to get complaints

export default {
    name: 'AdminViewComplaints',
    data() {
        return {
            userName: "", // Reactive property to store the user's name
            complaints: [], // Store the list of complaints fetched from the API
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
        this.fetchComplaints();
    },
    methods: {
        async fetchComplaints() {
            try {
                const complaintsData = await getAllComplaints(); // Assuming this function is imported
                if (complaintsData && complaintsData.$values && complaintsData.$values.length > 0) {
                    this.complaints = complaintsData.$values;
                } else {
                    this.errorMessage = "No complaints found."; // Set error message if no data returned
                }
            } catch (error) {
                // Handle API request error
                this.errorMessage = "Failed to fetch complaints. Please try again later.";
                console.error("Error fetching complaints:", error);
            }
        },
        getStatusText(status) {
            // Convert status to user-friendly text
            switch (status) {
                case 0:
                    return "Open";
                case 1:
                    return "In Progress";
                case 2:
                    return "Closed";
                default:
                    return "Unknown";
            }
        },
        getPriorityText(priority) {
            // Convert priority to user-friendly text
            switch (priority) {
                case 0:
                    return "Low";
                case 1:
                    return "Medium";
                case 2:
                    return "High";
                default:
                    return "Unknown";
            }
        },
        formatDate(dateString) {
            // Format date to a more readable format
            const options = { year: "numeric", month: "short", day: "numeric", hour: "2-digit", minute: "2-digit" };
            return new Date(dateString).toLocaleString(undefined, options);
        },
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

.complaint-profile {
    margin-bottom: 15px;
    border: 1px solid #ccc;
    padding: 10px;
    border-radius: 5px;
}

.complaint-profile h3 {
    margin: 0;
    font-size: 1.2em;
}
</style>
