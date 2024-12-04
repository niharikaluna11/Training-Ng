<template>
    <OrgHeader class="header" />
    <main class="main">
        <OrgSideBar class="admin-sidebar" />

        <div class="responsive-wrapper">
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
                <p>No complaints found.</p>
            </div>
        </div>

        <!-- Modal -->
        <div v-if="showModal" class="modal-overlay">
            <div v-if="complaintDetails" class="modal">
                <template v-if="isUpdating">
                    <h3>Update Complaint ID: {{ complaintDetails.complaintId }}</h3>
                    <form @submit.prevent="submitUpdate">
                        <label for="status">Status:</label>
                        <select v-model="updatedStatus" id="status">
                            <option v-for="status in statusOptions" :key="status" :value="status">
                                {{ getStatusText(status) }}
                            </option>
                        </select>

                        <label for="comment">Comment:</label>
                        <textarea v-model="updateComment" id="comment" placeholder="Enter your comment"></textarea>

                        <label for="status-date">Status Date:</label>
                        <input type="datetime-local" v-model="statusDate" id="status-date" required />

                        <div class="modal-buttons">
                            <button type="submit">Save Changes</button>
                            <button type="button" @click="cancelUpdate">Cancel</button>
                        </div>
                    </form>
                </template>

                <template v-else>
                    <h3>Complaint ID: {{ complaintDetails.complaintId }}</h3>
                    <p><strong>Category:</strong> {{ complaintDetails.categoryName }}</p>
                    <p><strong>Status:</strong> {{ complaintDetails.status }}</p>
                    <p><strong>Priority:</strong> {{ complaintDetails.priority }}</p>
                    <p><strong>Updated At:</strong> {{ formatDate(complaintDetails.latestStatusDate) }}</p>
                    <div v-if="complaintDetails.organizationDetails">
                        <p><strong>Organization:</strong> {{ complaintDetails.organizationDetails.name }}</p>
                    </div>
                    <div v-if="complaintDetails.userDetails">
                        <p><strong>User:</strong> {{ complaintDetails.userDetails.username }}</p>
                    </div>

                    <button @click="startUpdate">Update Complaint</button>
                    <button @click="closeModal">Close</button>
                </template>
            </div>
        </div>
    </main>
</template>

<script>
import { getComplaintDetails } from "@/scripts/GetALL/GetAllComplaints";
import { fetchCategoriesName } from "@/scripts/GetALL/ComplaintCategoryService";
import { getUserIdByUsername } from "@/scripts/GetALL/GetUserId";
import { getAllComplaintsbyorg, getOrgIdByUserId } from "@/scripts/GetALL/GetComplaintBy";
import OrgHeader from "./OrgHeader.vue";
import OrgSideBar from "./OrgSideBar.vue";
import { updateComplaintStatus } from "@/scripts/UpdateComplaintStatus";

export default {
    name: "OrgComplaintView",
    components: { OrgHeader, OrgSideBar },
    data() {
        return {
            userName: "",
            userId: null,
            userIdd: null,
            complaints: [],
            errorMessage: "",
            selectedComplaintId: null,
            complaintDetails: null,
            currentPage: 1,
            pageSize: 6,
            totalPages: 1,
            showModal: false,
            isUpdating: false,
            updatedStatus: 0,
            updateComment: "",
            statusDate: "",
            categories: [],
            statusOptions: [0, 1, 2],
            searchQuery: "",
        };
    },
    computed: {
        filteredComplaints() {
            return this.complaints.filter(complaint =>
                complaint.description.includes(this.searchQuery)
            );
        },
        pageNumbers() {
            return Array.from({ length: this.totalPages }, (_, i) => i + 1);
        },
    },
    async mounted() {
        const username = localStorage.getItem("username");
        if (username) {
            this.userName = username;
            try {
                this.userIdd = await getUserIdByUsername(username);
                const orge = await getOrgIdByUserId(this.userIdd);
                console.log(orge);
                this.userId = orge.data.organizationId;
                await this.fetchComplaints();
            } catch (error) {
                console.error("Failed to fetch user ID:", error);
            }
        } else {
            console.warn("Username not found in localStorage");
        }
        await this.fetchCategories();
    },
    methods: {
        async fetchComplaints() {
            if (!this.userId) return;
            try {
                const complaintsData = await getAllComplaintsbyorg(this.userId, this.currentPage, this.pageSize);
                this.complaints = complaintsData.complaints || [];
                this.totalPages = Math.ceil((complaintsData.totalCount || 0) / this.pageSize);
            } catch (error) {
                this.errorMessage = "Failed to fetch complaints. Please try again later.";
                console.error("Error fetching complaints:", error);
            }
        },
        async fetchCategories() {
            try {
                const response = await fetchCategoriesName();
                if (response?.$values) {
                    this.categories = response.$values.map(category => ({
                        id: category.categoryId,
                        name: category.categoryName,
                    }));
                }
            } catch (error) {
                console.error("Error fetching categories:", error);
            }
        },
        changePage(page) {
            if (page > 0 && page <= this.totalPages) {
                this.currentPage = page;
                this.fetchComplaints();
            }
        },
        formatDate(dateString) {
            const date = new Date(dateString);
            return date.toLocaleDateString("en-US", {
                year: "numeric",
                month: "long",
                day: "numeric",
                hour: "2-digit",
                minute: "2-digit",
                second: "2-digit",
            });
        },
        async fetchComplaintDetails(complaintId) {
            if (this.selectedComplaintId === complaintId) {
                this.closeModal();
                return;
            }
            try {
                const { complaintDetails } = await getComplaintDetails(complaintId);
                this.complaintDetails = complaintDetails;
                this.selectedComplaintId = complaintId;
                this.showModal = true;
            } catch (error) {
                console.error("Error fetching complaint details:", error);
            }
        },
        closeModal() {
            this.showModal = false;
            this.isUpdating = false;
            this.selectedComplaintId = null;
        },
        startUpdate() {
            this.isUpdating = true; // Show the update form in the modal
        },
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

        async submitUpdate() {

            try {
                await updateComplaintStatus(
                    this.complaintDetails.complaintId,
                    this.updatedStatus,
                    this.updateComment,
                    this.statusDate
                );
                alert("Complaint updated successfully.");
                this.isUpdating = false;
                this.fetchComplaints();
                this.fetchCategories();
                this.closeModal();
            } catch (err) {
                console.error("Error updating complaint:", err.response.data);
                alert(err.response.data.message);
            }
        },
        cancelUpdate() {
            // Reset the form values and close the modal
            this.isUpdating = false;
            this.updateComment = "";
            this.statusDate = "";
            this.updatedStatus = 0;
            this.closeModal();
        },
    },
};
</script>

<style scoped>
/* Style for the status update form in the modal */
.modal-buttons {
    display: flex;
    gap: 10px;
    margin-top: 20px;
}

textarea {
    width: 90%;
    height: 80px;
    padding: 8px;
    border: 1px solid #ccc;
    border-radius: 4px;
    resize: none;
}

input,
select {
    width: 80%;
    padding: 8px;
    border: 1px solid #ccc;
    border-radius: 4px;
    margin-bottom: 10px;
}

.modal-buttons button {
    padding: 8px 12px;
    border: none;
    border-radius: 4px;
    background-color: var(--new-color);
    color: white;
    cursor: pointer;
    transition: background-color 0.3s ease;
}

.modal-buttons button:hover {
    background-color: var(--c-text-action);
}

.modal-buttons button:focus {
    outline: none;
}

/* Add your styles here for search inputs and other components */
/* Style for the search filters container */
.search-filters {}

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


.dropdown-header {
    padding: 10px;
    font-size: 1rem;
    background-color: #ffffff;
    border: 1px solid #ccc;
    border-radius: 5px;
    cursor: pointer;
}


.search-dropdown {

    width: 100%;
    max-width: 400px;
    margin: 0 auto;

    border-radius: 5px;

    background-color: white;
}

.dropdown-icon {
    font-size: 0.9rem;
    color: #666;
}

.dropdown-body {

    background-color: white;
    max-height: 250px;

    border-top: 1px solid #ccc;
}

.dropdown-search {
    display: flex;
    padding: 10px;
    border-bottom: 1px solid #ccc;
}

.search-input {
    flex: 1;
    padding: 5px;
    font-size: 1rem;
    border: 1px solid #ccc;
    border-radius: 3px;
}

.search-button {
    margin-left: 5px;
    background: none;
    border: none;
    cursor: pointer;
    font-size: 1.2rem;
}

.dropdown-list {
    list-style: none;
    padding: 0;
    margin: 0;
    max-height: 200px;
    overflow-y: auto;
}

.dropdown-item {
    padding: 10px;
    cursor: pointer;
    font-size: 1rem;
    border-bottom: 1px solid #f0f0f0;
}

.dropdown-item:last-child {
    border-bottom: none;
}

.dropdown-item:hover {
    background-color: #f0f0f0;
}

.no-results {
    text-align: center;
    color: #999;
    padding: 10px;
}
</style>
