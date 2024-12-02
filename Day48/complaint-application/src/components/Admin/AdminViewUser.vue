<template>
    <AdminHeader class="header" />
    <main class="main">
        <SideBar class="admin-sidebar" />

        <div class="responsive-wrapper">
            <div v-if="users.length > 0">
                <div v-for="user in users" :key="user.id" class="user-profile">
                    <div class="profile-image">
                        <img :src="user.profilePicture || 'profilepicimage.jpg'" alt="User Image" />
                    </div>
                    <div class="user-info">
                        <h3>{{ user.firstName }} {{ user.lastName }}</h3>
                        <p v-if="user.viewDetails">Email: {{ user.email }}</p>
                        <p v-if="user.viewDetails">Phone: {{ user.phone }}</p>
                        <p v-if="user.viewDetails">Address: {{ user.address }}</p>
                    </div>
                    <div class="actions">
                        <button @click="editUser(user)">Edit</button>
                        <button @click="viewUser(user.id)">View</button>
                        <button @click="deleteUser(user)">Deactivate</button>
                        <button @click="ReactivateUser(user)">Reactivate</button>
                    </div>
                </div>
            </div>
            <div v-else>
                <p v-if="errorMessage">{{ errorMessage }}</p>
                <p v-else>No users found.</p>
            </div>
        </div>
    </main>

    <!-- Modal for Editing User -->
    <div v-if="isEditing" class="modal-overlay">
        <div class="modal-content">
            <form @submit.prevent="saveProfileChanges" class="edit-form">
                <div class="profile-info-content">
                    <div class="ok1">
                        <img v-if="editData.profilePicture" :src="editData.profilePicture"
                            alt="Profile Picture Preview" />
                        <img v-else :src="userProfile.profilePicture || 'default-profile.jpg'"
                            alt="Profile Picture Preview" />
                        <input type="file" @change="handleFileChange" id="profilePicture" />
                    </div>
                    <div class="ok2">
                        <input type="text" v-model="editData.firstName" id="firstName" />
                        <input type="text" v-model="editData.lastName" id="lastName" />
                        <input type="email" v-model="editData.email" id="email" />
                        <input type="text" v-model="editData.phone" id="phone" />
                        <input type="text" v-model="editData.address" id="address" />
                        <input type="date" v-model="editData.dateOfBirth" id="dateOfBirth" />
                        <textarea v-model="editData.preferences" id="preferences"></textarea>
                        <button class="okbutton" type="submit" @click="saveProfileChanges()">Save Changes</button>
                        <button class="okbutton" type="button" @click="cancelEdit">Cancel</button>
                    </div>
                </div>
            </form>
        </div>
    </div>
</template>


<script>
import { toast } from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';
import { getAllUsersProfiles } from '@/scripts/GetALL/GetAllprofile'; // Import the function to get users
import AdminHeader from './AdminHeader.vue';
import SideBar from './SideBar.vue';
import { updateUserProfile } from '@/scripts/UpdateProfile'; // Import the update function
import { deactivateUser, reactivateUser } from '@/scripts/ActivateService';

export default {
    name: 'AdminViewUser',
    components: {
        SideBar,
        AdminHeader,
    },
    data() {
        return {
            userName: "", // Reactive property to store the user's name
            users: [], // Store the list of users fetched from the API
            errorMessage: "", // Store any error message
            isEditing: false, // Modal open/close state
            userProfile: null, // Store the profile of the user being edited
            editData: {
                firstName: "",
                lastName: "",
                email: "",
                phone: "",
                address: "",
                dateOfBirth: "",
                preferences: "",
                profilePicture: null, // For file input
            },
        };
    },
    mounted() {
        // Retrieve `given_name` from sessionStorage
        const name = sessionStorage.getItem("username");
        this.userName = name || "Admin"; // Set the name to the reactive property

        // Fetch all users' profiles when the component is mounted
        this.fetchUsers();
    },
    methods: {
        async fetchUsers() {
            try {
                const usersData = await getAllUsersProfiles(); // Fetch users data from the API
                if (usersData && usersData.$values && usersData.$values.length > 0) {
                    this.users = usersData.$values;
                } else {
                    this.errorMessage = "No users found."; // Set error message if no data returned
                }
            } catch (error) {
                this.errorMessage = "Failed to fetch users. Please try again later.";
                console.error("Error fetching users' profiles:", error);
            }
        },
        editUser(user) {
            // Set the profile data to be edited
            this.userProfile = user;
            this.editData = { ...user }; // Populate edit form with current user data
            this.isEditing = true; // Open the modal
        },
        cancelEdit() {
            this.isEditing = false; // Close the modal without saving changes
        },
        async saveProfileChanges() {
            try {
                const formData = new FormData();

                formData.append("FirstName", this.editData.firstName);
                formData.append("LastName", this.editData.lastName);
                formData.append("Email", this.editData.email);
                formData.append("Phone", this.editData.phone);
                formData.append("Address", this.editData.address);

                // Format the date to match backend requirements
                const formattedDate = new Date(this.editData.dateOfBirth).toISOString();
                formData.append("DateOfBirth", formattedDate);

                formData.append("Preferences", this.editData.preferences);

                // Handle profile picture
                if (this.editData.profilePicture instanceof File) {
                    formData.append("ProfilePicture", this.editData.profilePicture);
                } else if (this.userProfile.profilePicture) {
                    formData.append("ProfilePicture", this.userProfile.profilePicture);
                } else {
                    formData.append("ProfilePicture", null); // If both are null, backend should handle it
                }

                const response = await updateUserProfile(this.userProfile.userId, formData);

                if (response) {
                    // Successfully updated, close the modal
                    this.userProfile = { ...this.editData }; // Update the user profile with changes
                    this.isEditing = false; // Close the modal

                    // Navigate to AdminViewUser page
                    this.$router.push('/AdminViewUser'); // Navigate to the desired page
                }
            } catch (err) {
                this.errorMessage = "Failed to save changes.";
                console.error("Error updating user profile:", err);
            }
        },
        handleFileChange(event) {
            const file = event.target.files[0];
            this.editData.profilePicture = file;
        },
        viewUser(userId) {
            const user = this.users.find(user => user.id === userId);
            if (user) {
                user.viewDetails = !user.viewDetails; // Toggle the viewDetails flag
            }
        },
        deleteUser(user) {
            this.user = user;
            console.log(user);

            deactivateUser(user.email)
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
        ReactivateUser(user) {
            this.user = user;
            console.log(user);

            reactivateUser(user.email)
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


    }

};
</script>


<style scoped>
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



/* User Profile Styling */
.user-profile {
    display: flex;
    align-items: center;
    justify-content: space-between;
    margin-bottom: 15px;
    border: 1px solid #ccc;
    padding: 15px;
    border-radius: 5px;
    background-color: #fff;
}

.profile-image img {
    width: 60px;
    height: 60px;
    border-radius: 50%;
    object-fit: cover;
    margin-right: 20px;
}

.user-info {
    flex-grow: 1;
}

.user-info h3 {
    margin: 0;
    font-size: 1.2em;
    font-weight: 600;
}

.user-info p {
    margin: 5px 0;
}

.actions {
    display: flex;
    gap: 10px;
    align-items: center;
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

/* Error Message */
p {
    font-size: 1.1em;
    color: #333;
}


.modal-overlay {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: rgba(0, 0, 0, 0.5);
    /* Semi-transparent background */
    display: flex;
    justify-content: center;
    align-items: center;
    z-index: 1000;
    /* Ensure modal is on top */
}

.modal-content {
    background-color: white;
    padding: 30px;
    border-radius: 8px;
    max-width: 800px;
    width: 100%;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.okbutton {
    padding: 10px 20px;
    background-color: var(--new-color);
    color: white;
    border: none;
    margin-left: 2px;
    border-radius: 4px;
    cursor: pointer;
}

.okbutton:hover {
    background-color: var(--c-text-action);
}

/* Adjust modal content layout */
.profile-info-content {
    display: flex;
    gap: 20px;
}

.ok1 {
    flex-shrink: 0;
    width: 200px;
}

.ok1 img {
    height: 300px;
    width: 200px;
}

.ok2 {
    flex-grow: 1;
    height: 400px;

    flex-direction: column;
}

/* Styling for input fields */
.ok2 input,
.ok2 textarea {
    padding: 5px;
    margin: 2px;
    font-size: 1.3rem;
    border-radius: 2px;
    border: 1px solid #ccc;
    width: 100%;
}
</style>
