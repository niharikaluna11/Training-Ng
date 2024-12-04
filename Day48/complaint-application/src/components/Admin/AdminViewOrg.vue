<template>
    <AdminHeader class="header" />
    <main class="main">
        <SideBar class="admin-sidebar" />

        <div class="responsive-wrapper">
            <div v-if="organizations.length > 0">
                <div v-for="organization in organizations" :key="organization.id" class="organization-profile">
                    <div class="profile-header">
                        <img :src="organization.profilePicture" alt="Profile Picture" class="profile-pic" />
                        <h3>{{ organization.name }}</h3>
                        <div class="profile-details" v-if="organization.showDetails">
                            <p><strong>Address:</strong> {{ organization.address }}</p>
                            <p><strong>Phone:</strong> {{ organization.phone }}</p>
                            <p><strong>Email:</strong> <a :href="'mailto:' + organization.email">{{ organization.email
                                    }}</a></p>
                            <p><strong>Type:</strong> {{ getTypeText(organization.types) }}</p>
                        </div>
                        <div class="actions">
                            <button @click="editOrganization(organization)">Edit</button>
                            <button @click="viewOrganization(organization)">View</button>
                            <button @click="deleteUser(organization)">Deactivate</button>
                            <button @click="ReactivateUser(organization)">Reactivate</button>
                        </div>
                    </div>
                </div>
            </div>
            <div v-else>
                <!-- <p v-if="errorMessage">{{ message }}</p> -->
                <p>No organizations found.</p>
            </div>
        </div>
    </main>

    <!-- Modal for Editing Organization -->
    <div v-if="isEditing" class="modal-overlay">
        <div class="modal-content">
            <form @submit.prevent="saveProfileChanges" class="edit-form">
                <div class="profile-info-content">
                    <div class="ok1">
                        <img v-if="editData.profilePicture" :src="editData.profilePicture"
                            alt="Profile Picture Preview" />
                        <img v-else :src="organizationProfile.profilePicture || 'default-profile.jpg'"
                            alt="Profile Picture Preview" />
                        <input type="file" @change="handleFileChange" id="profilePicture" />
                    </div>
                    <div class="ok2">
                        <input type="text" v-model="editData.name" id="name" placeholder="Organization Name" />
                        <input type="text" v-model="editData.address" id="address" placeholder="Address" />
                        <input type="text" v-model="editData.phone" id="phone" placeholder="Phone" />
                        <input type="email" v-model="editData.email" id="email" placeholder="Email" />
                        <textarea v-model="editData.description" id="description" placeholder="Description"></textarea>
                        <select v-model="editData.type" id="type">
                            <option value="1">Corporate</option>
                            <option value="2">Non-Profit</option>
                            <option value="3">Government</option>
                        </select>
                        <button class="okbutton" type="submit">Save Changes</button>
                        <button class="okbutton" type="button" @click="cancelEdit">Cancel</button>
                    </div>
                </div>
            </form>
        </div>
    </div>
</template>

<script>
import { getAllOrganizationsProfiles } from '@/scripts/GetALL/GetAllprofile';
import AdminHeader from './AdminHeader.vue';
import SideBar from './SideBar.vue';
import { updateOrgProfile } from '@/scripts/UpdateProfile';
import { toast } from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';
import { deactivateUser, reactivateUser } from '@/scripts/ActivateService';

export default {
    name: 'AdminViewOrg',
    components: {
        SideBar,
        AdminHeader,
    },
    data() {
        return {
            userName: "", // Reactive property to store the user's name
            organizations: [], // Store the list of organizations fetched from the API
            errorMessage: "", // Store any error message
            isEditing: false, // Modal open/close state
            organizationProfile: null, // Store the profile of the organization being edited
            editData: {
                name: "",
                address: "",
                phone: "",
                email: "",
                description: "",
                type: "",
                profilePicture: null, // For file input
            },
        };
    },
    mounted() {
        const name = sessionStorage.getItem("username");
        this.userName = name || "Admin"; // Set the name to the reactive property
        this.fetchOrganizations();
    },
    methods: {
        async fetchOrganizations() {
            try {
                const organizationsData = await getAllOrganizationsProfiles(); // Assuming this function is imported
                if (organizationsData && organizationsData.$values && organizationsData.$values.length > 0) {
                    this.organizations = organizationsData.$values;
                } else {
                    this.errorMessage = "No organizations found.";
                }
            } catch (error) {
                this.errorMessage = "Failed to fetch organizations.";
                console.error("Error fetching organizations:", error);
            }
        },
        editOrganization(organization) {
            this.organizationProfile = organization;
            this.editData = { ...organization }; // Populate edit form with current organization data
            this.isEditing = true; // Open the modal for editing
        },
        cancelEdit() {
            this.isEditing = false; // Close the modal without saving changes
        },
        async saveProfileChanges() {
            try {
                const formData = new FormData();
                formData.append("Name", this.editData.name);
                formData.append("Address", this.editData.address);
                formData.append("Phone", this.editData.phone);
                formData.append("Email", this.editData.email);
                formData.append("Description", this.editData.description);
                formData.append("Type", this.editData.type);

                if (this.editData.profilePicture instanceof File) {
                    formData.append("ProfilePicture", this.editData.profilePicture);
                } else if (this.organizationProfile && this.organizationProfile.profilePicture != null) {

                    formData.append("ProfilePicture", this.organizationProfile.profilePicture);
                } else {
                    formData.append("ProfilePicture", this.organizationProfile.profilePicture);
                }


                const response = await updateOrgProfile(this.organizationProfile.userId, formData);
                if (response) {
                    this.organizationProfile = { ...this.editData }; // Update the organization profile with changes
                    this.isEditing = false; // Close the modal
                }
            } catch (err) {
                this.errorMessage = "Failed to save changes.";
                console.error("Error updating organization profile:", err);
            }
        },
        handleFileChange(event) {
            const file = event.target.files[0];
            this.editData.profilePicture = file;
        },
        viewOrganization(organization) {
            // Toggle the visibility of details for the selected organization
            organization.showDetails = !organization.showDetails;
        }
        ,
        getTypeText(type) {
            switch (type) {
                case 0:
                    return "Corporate";
                case 1:
                    return "Government";
                case 2:
                    return "Agent";
                default:
                    return "Unknown";
            }
        },

        deleteUser(organization) {
            this.organization = organization;
            console.log(organization);

            deactivateUser(organization.email)
                .then((response) => {
                    console.log("User deactivated successfully:", response);

                    // Display success toast with correct username
                    toast.success(
                        `${response.data.username} has been deactivated successfully`,
                        {
                            rtl: true,
                            limit: 2,
                            position: toast.POSITION.TOP_CENTER,
                        }
                    );
                })
                .catch((error) => {
                    if (error.response) {
                        console.error("Server responded with an error:", error.response.data);
                        toast.error(`Failed to deactivate user: ${error.response.data.message}`, {
                            rtl: true,
                            limit: 2,
                            position: toast.POSITION.TOP_CENTER,
                        });
                    } else {
                        console.error("Error deactivating user:", error.message);
                        toast.error("An unexpected error occurred. Please try again.", {
                            rtl: true,
                            limit: 2,
                            position: toast.POSITION.TOP_CENTER,
                        });
                    }
                });
        }
        ,
        ReactivateUser(organization) {
            this.organization = organization;
            console.log(organization);

            reactivateUser(organization.email)
                .then((response) => {
                    console.log("User reactivated successfully:", response);

                    // Display success toast with correct username
                    toast.success(
                        `${response.data.username} has been reactivated successfully`,
                        {
                            rtl: true,
                            limit: 2,
                            position: toast.POSITION.TOP_CENTER,
                        }
                    );
                })
                .catch((error) => {
                    if (error.response) {
                        console.error("Server responded with an error:", error.response.data);
                        toast.error(`Failed to reactivate user: ${error.response.data.message}`, {
                            rtl: true,
                            limit: 2,
                            position: toast.POSITION.TOP_CENTER,
                        });
                    } else {
                        console.error("Error reactivating user:", error.message);
                        toast.error("An unexpected error occurred. Please try again.", {
                            rtl: true,
                            limit: 2,
                            position: toast.POSITION.TOP_CENTER,
                        });
                    }
                });
        }

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
