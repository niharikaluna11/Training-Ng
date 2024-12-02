<template>
    <AdminHeader class="header" />
    <main class="main">
        <SideBar class="admin-sidebar" />

        <div class="responsive-wrapper">

            <!-- Search Filters -->
            <div class="search-filters">
                <label for="category">Category:</label>
                <select v-model="searchCategory" id="category">
                    <option value="">All</option>
                    <option v-for="category in categories" :key="category" :value="category">{{ category }}</option>
                </select>

                <label for="status">Status:</label>
                <select v-model="searchStatus" id="status">
                    <option value="">All</option>
                    <option v-for="status in statusOptions" :key="status" :value="status">{{ getStatusText(status) }}
                    </option>
                </select>

                <label for="priority">Priority:</label>
                <select v-model="searchPriority" id="priority">
                    <option value="">All</option>
                    <option v-for="priority in priorityOptions" :key="priority" :value="priority">{{
                        getPriorityText(priority) }}</option>
                </select>
            </div>

            <!-- Complaints List -->
            <div v-if="filteredComplaints.length > 0">
                <div v-for="complaint in filteredComplaints" :key="complaint.complaintId" class="complaint-profile">
                    <div class="complaint-info">
                        <h3>ID: {{ complaint.complaintId }}</h3>
                        <p><strong>Complaint:</strong> {{ complaint.description }}</p>
                    </div>
                    <div class="complaint-button">
                        <button @click="fetchComplaintDetails(complaint.complaintId)">
                            {{ selectedComplaintId === complaint.complaintId ? "Hide Details" : "View Details" }}
                        </button>
                        <!-- <button @click="editComplaint(complaint.complaintId)">Update</button> -->
                    </div>
                </div>
            </div>

            <!-- Pagination -->
            <div v-if="totalPages > 1" class="pagination">
                <button @click="changePage(currentPage - 1)" :disabled="currentPage === 1">Previous</button>
                <span v-for="page in pageNumbers" :key="page" :class="{ 'active': page === currentPage }">
                    <button @click="changePage(page)">{{ page }}</button>
                </span>
                <button @click="changePage(currentPage + 1)" :disabled="currentPage === totalPages">Next</button>
            </div>

            <!-- No Complaints or Error Message -->
            <div v-else>
                <p v-if="errorMessage">{{ errorMessage }}</p>
                <p v-else>No complaints found.</p>
            </div>
        </div>

        <div v-if="showModal" class="modal-overlay">
            <div class="modal">
                <h3>Complaint ID: {{ complaintDetails.complaintId }}</h3>
                <p><strong>Category:</strong> {{ complaintDetails.categoryName }}</p>
                <p><strong>Status:</strong> {{ complaintDetails.status }}</p>
                <p><strong>Priority:</strong> {{ complaintDetails.priority }}</p>
                <p><strong>Updated At:</strong> {{ formatDate(complaintDetails.latestStatusDate) }}</p>

                <div v-if="complaintDetails.files && complaintDetails.files.length > 0">
                    <p><strong>Attached Files:</strong></p>
                    <ul>
                        <li v-for="file in complaintDetails.files" :key="file">{{ file }}</li>
                    </ul>
                </div>

                <div v-if="complaintDetails.organizationDetails">
                    <p><strong>Organization:</strong> {{ complaintDetails.organizationDetails.name }}</p>
                </div>

                <div v-if="complaintDetails.userDetails">
                    <p><strong>User:</strong> {{ complaintDetails.userDetails.email }}</p>
                </div>

                <button @click="closeModal">Close</button>
            </div>
        </div>

    </main>
</template>


<script>
import { getAllComplaints, getComplaintDetails } from "@/scripts/GetALL/GetAllComplaints";
import AdminHeader from "./AdminHeader.vue";
import SideBar from "./SideBar.vue";
import { fetchCategoriesName } from "@/scripts/GetALL/ComplaintCategoryService";
// import { updateComplaintStatus } from "@/scripts/UpdateComplaintStatus";

export default {
    name: "AdminViewComplaints",
    components: {
        SideBar,
        AdminHeader,
    },
    data() {
        return {
            userName: "",
            complaints: [],
            errorMessage: "",
            selectedComplaintId: null,
            complaintDetails: null,
            currentPage: 1,
            pageSize: 6,
            totalPages: 1,
            showModal: false,
            isUpdating: false, // Flag to show the update form
            updatedStatus: 0, // Default status value
            updateComment: "", // Comment input for update
            statusDate: "", // Date input for update
            categories: [],
            statusOptions: [0, 1, 2],
            priorityOptions: [0, 1, 2, 3],
        };
    },

    mounted() {
        const name = sessionStorage.getItem("username");
        this.userName = name || "Admin";
        this.fetchComplaints();
        this.fetchCategories();
        this.$nextTick(() => {
            // Ensuring the component is properly hydrated before fetching complaint details
            this.fetchComplaintDetails(this.complaints.id);
        });
    },
    computed: {
        pageNumbers() {
            return Array.from({ length: this.totalPages }, (_, i) => i + 1);
        },
        filteredComplaints() {
            return this.complaints.filter((complaint) => {
                const complaintDetails = complaint.complaintDetails || {};

                const matchesCategory = this.searchCategory
                    ? complaintDetails.categoryName == this.searchCategory
                    : true;
                const matchesStatus = this.searchStatus
                    ? complaintDetails.status == this.searchStatus
                    : true;
                const matchesPriority = this.searchPriority
                    ? complaintDetails.priority == this.searchPriority
                    : true;

                return matchesCategory && matchesStatus && matchesPriority;
            });
        },

    },
    methods: {
        async fetchComplaints() {
            try {
                const complaintsData = await getAllComplaints(this.currentPage, this.pageSize);
                console.log(complaintsData); // Log the full response to check the structure
                this.complaints = complaintsData.complaints || [];

                // Fetch details for each complaint
                for (const complaint of this.complaints) {
                    const { complaintDetails } = await getComplaintDetails(complaint.complaintId);
                    complaint.complaintDetails = complaintDetails;  // Store details inside the complaint
                }

                this.totalPages = Math.ceil((complaintsData.totalCount || 0) / this.pageSize);
            } catch (error) {
                this.errorMessage = "Failed to fetch complaints. Please try again later.";
                console.error("Error fetching complaints:", error);
            }
        },

        async fetchCategories() {
            try {
                const categoriesData = await fetchCategoriesName();
                console.log(categoriesData);
                this.categories = categoriesData || []; // Store fetched categories in the `categories` array
            } catch (error) {
                console.error("Error fetching categories:", error);
            }

        },



        async fetchComplaintDetails(complaintId) {
            try {
                if (!complaintId) {
                    console.error("Complaint ID is missing.");
                    return;
                }

                const { complaintDetails } = await getComplaintDetails(complaintId);
                this.complaintDetails = complaintDetails;
                this.selectedComplaintId = this.selectedComplaintId === complaintId ? null : complaintId;
                this.showModal = !!this.selectedComplaintId;
            } catch (error) {
                console.error("Error fetching complaint details:", error);
            }
        }
        ,

        getStatusText(status) {
            switch (status) {
                case 0:
                    return "Received";
                case 1:
                    return "In Progress";
                case 2:
                    return "Resolved";
                default:
                    return "Unknown";
            }
        },
        getPriorityText(priority) {
            switch (priority) {
                case 0:
                    return "Low";
                case 1:
                    return "Medium";
                case 2:
                    return "High";
                case 3:
                    return "Urgent";
                default:
                    return "Unknown";
            }
        },
        formatDate(dateString) {
            return new Date(dateString).toLocaleString(undefined, {
                year: "numeric",
                month: "short",
                day: "numeric",
                hour: "2-digit",
                minute: "2-digit",
            });
        },
        changePage(page) {
            if (page > 0 && page <= this.totalPages) {
                this.currentPage = page;
                this.fetchComplaints();
            }
        },

        closeModal() {
            this.showModal = false;
        },


    },
};
</script>



<style scoped>
/* Style for the status update form in the modal */

/* Add your styles here for search inputs and other components */
/* Style for the search filters container */
.search-filters {
    display: flex;
    gap: 20px;
    /* Gap between elements */
    margin-bottom: 20px;
    flex-wrap: wrap;
    margin-left: 5px;
    left: 5px;
    /* Align items to the left */
    padding: 10px;

    /* Light background color */
    border-radius: 8px;
    /* Rounded corners */
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
    /* Subtle shadow for depth */
}

/* Style for labels */
.search-filters label {
    font-weight: 600;
    margin-left: 5px;
    color: #333;
    font-size: 1rem;
}

/* Style for select dropdowns */
.search-filters select {
    padding: 8px 12px;
    /* More padding for better size */
    font-size: 1em;
    border: 1px solid #ccc;
    /* Light border */
    width: 22%;
    border-radius: 5px;
    /* Rounded corners */
    /* White background for select */
    color: #333;
    /* Dark text color for select */
    transition: border-color 0.3s ease, box-shadow 0.3s ease;
    /* Smooth transitions for focus state */
}

/* Change border color on focus */
.search-filters select:focus {
    border-color: #007BFF;
    /* Blue border on focus */
    box-shadow: 0 0 5px rgba(0, 123, 255, 0.5);
    /* Subtle blue glow */
    outline: none;
    /* Remove default focus outline */
}

/* Responsive adjustments for smaller screens */
@media (max-width: 768px) {
    .search-filters {
        flex-direction: column;
        /* Stack filters vertically */
        gap: 15px;
        /* Reduce gap for small screens */
    }

    .search-filters select {
        width: 100%;
        /* Make select boxes full width on small screens */
    }
}

/* Keep existing styles... */
/* Add your styles here */
.header {
    height: 60px;
    padding-top: 0px;
}

.admin-sidebar {
    border-right: 1px solid #d3d3d3;
    width: 60px;
}

.main {
    display: flex;
    padding-top: 60px;
}

.responsive-wrapper {
    padding: 20px;
}

.main-header {
    margin-bottom: 20px;
}

/* Pagination Styling */
.pagination {
    display: flex;
    justify-content: center;
    gap: 10px;
    margin-top: 20px;
}

.pagination button {
    padding: 8px 12px;
    border: none;
    border-radius: 4px;
    background-color: var(--new-color);
    color: white;
    cursor: pointer;
    transition: background-color 0.3s ease;
}

.pagination button:hover {
    background-color: var(--c-text-action);
}

.pagination button:disabled {
    background-color: #d3d3d3;
    cursor: not-allowed;
}

.pagination span.active button {
    background-color: var(--new-color);
    color: white;
}

.pagination span.active button:hover {
    background-color: var(--c-text-action);
}

/* General Button Styling */
button {
    padding: 8px 12px;
    border: none;
    border-radius: 4px;
    background-color: var(--new-color);
    color: white;
    cursor: pointer;
    transition: background-color 0.3s ease;
}


button:hover {
    background-color: var(--c-text-action);
}

button:focus {
    outline: none;
}

button:disabled {
    background-color: #d3d3d3;
    cursor: not-allowed;
}

/* General Button Styling */
button {
    padding: 8px 12px;
    border: none;
    border-radius: 4px;
    background-color: var(--new-color);
    color: white;
    cursor: pointer;
    transition: background-color 0.3s ease;
}

button:hover {
    background-color: var(--c-text-action);
}

button:focus {
    outline: none;
}

button:disabled {
    background-color: #d3d3d3;
    cursor: not-allowed;
}

/* Error Message Styling */
p {
    font-size: 1.1em;
    color: #333;
}

/* Extra Detail Styling (Complaints Description, etc.) */
.complaint-profile p strong {
    font-weight: 600;
}

.complaint-profile p {
    margin-bottom: 10px;
}

/* Responsive Adjustments for Small Screens */
@media (max-width: 768px) {
    .responsive-wrapper {
        padding: 10px;
        /* Reduce padding for small screens */
    }

    /* Adjust button size and layout */
    button {
        width: 100%;
        /* Ensure buttons are full width on small screens */
    }

    /* Add spacing between different complaint entries */
    .complaint-profile+.complaint-profile {
        margin-top: 20px;
    }
}


/* Flexbox adjustments for complaint profile */
.complaint-profile {
    display: flex;
    justify-content: space-between;
    /* Align complaint ID and buttons on the same row */
    margin-bottom: 15px;
    border: 1px solid #ccc;
    padding: 15px;
    border-radius: 5px;
    background-color: #fff;
    flex-wrap: wrap;
    gap: 15px;
    /* Added gap for spacing between content */
}

/* Adjust complaint info section (complaintId and description) */
.complaint-info {
    flex: 1;
    /* Allow complaint info to take up remaining space */
}

/* Buttons Styling - Complaint Profile Actions */
.complaint-button {
    display: flex;
    gap: 4px;
    /* Added 4px gap between buttons */
    align-items: center;
}

/* Individual paragraph styles inside the complaint profile */
.complaint-profile p {
    font-size: 1em;
    margin-bottom: 10px;
}

/* Adjust the width for small screens */
@media (max-width: 768px) {
    .complaint-profile p {
        width: 100%;
    }

    /* Adjust heading size for better readability on small screens */
    .complaint-profile h3 {
        font-size: 1.1em;
    }

    /* Make complaint profile stack on smaller screens */
    .complaint-profile {
        flex-direction: column;
        align-items: flex-start;
    }

    /* Make complaint info take full width on smaller screens */
    .complaint-info {
        width: 100%;
    }

    /* Stack buttons vertically on smaller screens */
    .complaint-button {
        flex-direction: column;
        gap: 5px;
    }

    /* Make buttons full width on smaller screens */
    button {
        width: 100%;
    }
}


.modal-overlay {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.5);
    display: flex;
    justify-content: center;
    align-items: center;
}

.modal {
    background-color: white;
    padding: 20px;
    border-radius: 10px;
    max-width: 600px;
    width: 80%;
}
</style>
