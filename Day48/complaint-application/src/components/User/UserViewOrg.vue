<template>
    <UserHeader class="header" />
    <main class="main">
        <UserSideBar class="admin-sidebar" />

        <div class="responsive-wrapper">
            <!-- Check if organizations exist -->
            <div v-if="organizations.length > 0">
                <div v-for="organization in organizations" :key="organization.id" class="organization-profile">
                    <div class="profile-header">
                        <img :src="organization.profilePicture" alt="Profile Picture" class="profile-pic" />
                        <h3>{{ organization.name }}</h3>
                        <!-- Toggle visibility of organization details -->
                        <div class="profile-details" v-if="organization.showDetails">
                            <p><strong>Address:</strong> {{ organization.address }}</p>
                            <p><strong>Phone:</strong> {{ organization.phone }}</p>
                            <p><strong>Email:</strong>
                                <a :href="'mailto:' + organization.email">
                                    {{ organization.email }}
                                </a>
                            </p>
                            <p><strong>Type:</strong> {{ getTypeText(organization.types) }}</p>
                        </div>
                        <div class="actions">
                            <button @click="viewOrganization(organization)">View</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Display if no organizations are found -->
            <div v-else>
                <p>No organizations found.</p>
            </div>
        </div>
    </main>
</template>


<script>
import { getAllOrganizationsProfiles } from '@/scripts/GetALL/GetAllprofile';
import UserHeader from './UserHeader.vue';
import UserSideBar from './UserSideBar.vue';

export default {
    name: 'UserViewOrg',
    components: {
        UserSideBar,
        UserHeader,
    },
    data() {
        return {
            userName: "", // Current user's name
            organizations: [], // List of organizations
            errorMessage: "", // Error message for failed API calls
        };
    },
    mounted() {
        const name = sessionStorage.getItem("username");
        this.userName = name || "Admin"; // Fallback to 'Admin' if no username found
        this.fetchOrganizations();
    },
    methods: {
        // Fetch organizations from API
        async fetchOrganizations() {
            try {
                const response = await getAllOrganizationsProfiles();
                if (response?.$values?.length) {
                    this.organizations = response.$values.map(org => ({
                        ...org,
                        showDetails: false, // Add `showDetails` flag for each organization
                    }));
                } else {
                    this.errorMessage = "No organizations found.";
                }
            } catch (error) {
                this.errorMessage = "Failed to fetch organizations.";
                console.error("Error fetching organizations:", error);
            }
        },

        // Toggle organization details
        viewOrganization(organization) {
            organization.showDetails = !organization.showDetails;
        },

        // Convert organization type ID to a human-readable format
        getTypeText(type) {
            const types = ["Corporate", "Government", "Agent"];
            return types[type] || "Unknown";
        },
    },
};
</script>



<style scoped>
/* Organization Profile Styling */
.organization-profile {
    display: flex;
    flex-direction: column;
    margin-bottom: 20px;
    border: 1px solid #ccc;
    padding: 15px;
    border-radius: 5px;
    background-color: #fff;
}

.profile-header {
    display: flex;
    align-items: center;
    gap: 50px;
}

.profile-pic {
    width: 60px;
    height: 60px;
    border-radius: 50%;
    object-fit: cover;
}

.organization-profile h3 {
    font-size: 1.5em;
    font-weight: 600;
    margin: 0;
}

.profile-details {
    margin-top: 10px;
    font-size: 1.1em;
}

.profile-details p {
    margin: 5px 0;
}

.actions {
    position: absolute;
    right: 85px;
    display: flex;
    gap: 10px;
}

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

/* Modal */
.modal-overlay {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: rgba(0, 0, 0, 0.5);
    display: flex;
    justify-content: center;
    align-items: center;
    z-index: 1000;
}

.modal-content {
    background-color: white;
    padding: 30px;
    border-radius: 8px;
    max-width: 800px;
    width: 100%;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.profile-info-content {
    display: flex;
    gap: 20px;
}

.ok1 {
    flex-shrink: 0;
    width: 200px;
}

.ok1 img {
    height: 200px;
    width: 200px;
    border-radius: 8px;
    object-fit: cover;
}

.ok2 {
    flex-grow: 1;
}

input,
select,
textarea {
    width: 100%;
    padding: 8px;
    margin-bottom: 10px;
    border: 1px solid #ddd;
    border-radius: 4px;
}

button.okbutton {
    background-color: var(--new-color);
    color: white;
    font-size: 16px;
}

button.okbutton:disabled {
    background-color: #d3d3d3;
}

textarea {
    height: 100px;
    resize: vertical;
}

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
</style>
