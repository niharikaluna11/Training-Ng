<script>
import { getUserIdByUsername } from "@/scripts/GetALL/GetUserId";
import { getUserProfile } from "@/scripts/GetALL/GetUserProfile";
import {  updateUserProfile } from "@/scripts/UpdateProfile";

export default {
    name: "AdminProfile",
    data() {
        return {
            userName: "", // Username from sessionStorage
            userProfile: null, // Object to store user profile details
            error: "", // Error message for display
            isEditing: false, // Flag to toggle between view and edit mode
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
    async mounted() {
        try {
            const username = sessionStorage.getItem("username");
            if (!username) {
                this.error = "Username not found in sessionStorage.";
                return;
            }

            this.userName = username;
            const userIdResponse = await getUserIdByUsername(username);
            const userId = userIdResponse.data.userId;

            const userProfile = await getUserProfile(userId);

            if (userProfile) {
                this.userProfile = userProfile;
                this.editData = { ...userProfile };
            } else {
                this.error = "Unable to fetch user profile.";
            }
        } catch (err) {
            this.error = "An error occurred while fetching profile details.";
            console.error(err);
        }
    },
    methods: {
        editProfile() {
            this.isEditing = true;
        },
        cancelEdit() {
            this.isEditing = false;
        },
        handleFileChange(event) {
            const file = event.target.files[0];
            this.editData.profilePicture = file;
        },
        async saveProfileChanges() {
            try {
                const formData = new FormData();

                formData.append("FirstName", this.editData.firstName);
                formData.append("LastName", this.editData.lastName);
                formData.append("Email", this.editData.email);
                formData.append("Phone", this.editData.phone);
                formData.append("Address", this.editData.address);
                formData.append("DateOfBirth", this.editData.dateOfBirth);
                formData.append("Preferences", this.editData.preferences);
                if (this.editData.profilePicture) {
                    formData.append("ProfilePicture", this.editData.profilePicture);
                }

                let response;

                response = await updateUserProfile(this.userProfile.userId, formData);


                if (response) {
                    this.userProfile = { ...this.editData };
                    this.isEditing = false;
                }

            } catch (err) {
                this.error = "Failed to save changes.";
            }
        }
    },
};
</script>

<template>
    <div class="admin-profile">
        <header class="header">
            <div class="header-content responsive-wrapper">
                <div class="header-logo">
                    <a href="#">
                        <div>
                            <img src="https://assets.codepen.io/285131/untitled-ui-icon.svg" />
                        </div>
                        <img src="https://assets.codepen.io/285131/untitled-ui.svg" />
                    </a>
                </div>
                <div class="header-navigation">
                    <nav class="header-navigation-links">
                        <a href="#">
                            <router-link to="/HomePage" class="nav-link active text-light" aria-current="page">
                                Home
                            </router-link>
                        </a>
                    </nav>
                    <div class="header-navigation-actions">
                        <a href="#" class="icon-button">
                            <i class="ph-gear-bold"></i>
                        </a>
                        <a href="#" class="icon-button">
                            <i class="ph-bell-bold"></i>
                        </a>
                        <a href="#" class="avatar">
                            <img src="@/Images/profilepicimg.jpg" alt="profile">
                        </a>
                    </div>
                </div>
                <a href="#" class="button">
                    <i class="ph-list-bold"></i>
                    <span>Menu</span>
                </a>
            </div>
        </header>

        <main class="main">
            <div class="main-header">
                <h1>Hello, {{ userName }}</h1>
            </div>

            <button v-if="!isEditing" @click="editProfile" class="button">Update Profile</button>

            <section v-if="userProfile && !isEditing" class="profile-info">
                <p><strong>Name:</strong> {{ userProfile.firstName }} {{ userProfile.lastName }}</p>
                <p><strong>Email:</strong> {{ userProfile.email }}</p>
                <p><strong>Phone:</strong> {{ userProfile.phone }}</p>
                <p><strong>Address:</strong> {{ userProfile.address }}</p>
                <p><strong>Date of Birth:</strong> {{ userProfile.dateOfBirth }}</p>
                <p><strong>Preferences:</strong> {{ userProfile.preferences }}</p>
                <img v-if="userProfile.profilePicture" :src="userProfile.profilePicture" alt="Profile Picture" />
            </section>

            <section v-if="isEditing">
                <form @submit.prevent="saveProfileChanges">
                    <label for="firstName">First Name:</label>
                    <input type="text" v-model="editData.firstName" id="firstName" />

                    <label for="lastName">Last Name:</label>
                    <input type="text" v-model="editData.lastName" id="lastName" />

                    <label for="email">Email:</label>
                    <input type="email" v-model="editData.email" id="email" />

                    <label for="phone">Phone:</label>
                    <input type="text" v-model="editData.phone" id="phone" />

                    <label for="address">Address:</label>
                    <input type="text" v-model="editData.address" id="address" />

                    <label for="dateOfBirth">Date of Birth:</label>
                    <input type="date" v-model="editData.dateOfBirth" id="dateOfBirth" />

                    <label for="preferences">Preferences:</label>
                    <textarea v-model="editData.preferences" id="preferences"></textarea>

                    <label for="profilePicture">Profile Picture:</label>
                    <input type="file" @change="handleFileChange" id="profilePicture" />

                    <button type="submit">Save Changes</button>
                    <button type="button" @click="cancelEdit">Cancel</button>
                </form>
            </section>

            <p v-if="error" class="error">{{ error }}</p>
        </main>
    </div>
</template>

<style scoped>
.main {
    padding-top: 50px;
}

.header-content {
    display: flex;
    justify-content: space-between;
    align-items: center;
}

.profile-info {
    margin-top: 20px;
}

.profile-info img {
    width: 100px;
    height: 100px;
    object-fit: cover;
}

.button {
    padding: 10px;
    background-color: #4CAF50;
    color: white;
    border: none;
    cursor: pointer;
}

.error {
    color: red;
}

form {
    display: flex;
    flex-direction: column;
}

form label,
form input,
form textarea {
    margin-bottom: 10px;
}

form button {
    margin-top: 10px;
    background-color: #008CBA;
    color: white;
}
</style>
