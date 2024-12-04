<template>
    <AdminHeader class="header" />

    <main class="main">
        <SideBar class="admin-sidebar" />

        <div class="responsive-wrapper">



            <!-- Profile Information Section -->
            <section v-if="userProfile && !isEditing" class="profile-info">
                <div class="profile-info-content">
                    <div class="profile-image">
                        <img v-if="userProfile.profilePicture" :src="userProfile.profilePicture"
                            alt="Profile Picture" />
                        <img v-else src="../../../public/profilepicimg.jpg" alt="Default Profile Picture" />
                    </div>

                    <div class="profile-details">
                        <p><strong>Name:</strong> {{ userProfile.firstName }} {{ userProfile.lastName }}</p>
                        <p><strong>Email:</strong> {{ userProfile.email }}</p>
                        <p><strong>Phone:</strong> {{ userProfile.phone }}</p>
                        <p><strong>Address:</strong> {{ userProfile.address }}</p>
                        <p><strong>Date of Birth:</strong> {{ userProfile.dateOfBirth }}</p>
                        <p><strong>Preferences:</strong> {{ userProfile.preferences }}</p>
                        <button v-if="!isEditing" @click="editProfile" class="update-button">Update Profile</button>

                    </div>
                </div>
            </section>

            <!-- Update Profile Button -->

            <!-- Edit Profile Section -->
            <section v-if="isEditing">
                <form @submit.prevent="saveProfileChanges" class="edit-form">
                    <div class="profile-info-content " :class="{ editing: isEditing }">



                        <!-- Display the uploaded or current profile picture -->
                        <div class="ok1">
                            <img v-if="editData.profilePicture" :src="editData.profilePicture"
                                alt="Profile Picture Preview" />
                            <img v-else :src="userProfile.profilePicture || 'alt.pic'" alt="Profile Picture Preview" />
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

                            <button class="okbutton" type="submit">Save Changes</button>
                            <button class="okbutton" type="button" @click="cancelEdit">Cancel</button>


                        </div>
                    </div>

                </form>

            </section>

            <!-- Error Message -->
            <p v-if="error" class="error">{{ error }}</p>
        </div>
    </main>
</template>

<script>
import { getUserIdByUsername } from "@/scripts/GetALL/GetUserId";
import { getUserProfile } from "@/scripts/GetALL/GetUserProfile";
import { updateUserProfile } from "@/scripts/UpdateProfile";
import AdminHeader from './AdminHeader.vue';
import SideBar from './SideBar.vue';

export default {
    name: "AdminProfile",
    components: {
        SideBar,
        AdminHeader,
    },
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
            const userId = userIdResponse;

            const userProfile = await getUserProfile(userId);

            if (userProfile) {
                // Format the date for the input field
                if (userProfile.dateOfBirth) {
                    const formattedDate = new Date(userProfile.dateOfBirth).toISOString().split("T")[0];
                    userProfile.dateOfBirth = formattedDate;
                }

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
                    this.userProfile = { ...this.editData };
                    this.isEditing = false;
                }
            } catch (err) {
                if (err.response && err.response.data) {
                    console.error("Server Response Error:", err.response.data);
                } else {
                    console.error("Error updating user profile:", err);
                }
                this.error = "Failed to save changes.";
            }
        }

    },
};
</script>

<style scoped>
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
    max-width: 1200px;
    margin: 0 auto;

}

.profile-info-content {
    display: flex;
    justify-content: flex-start;
    /* Align items to the start */
    align-items: center;
    /* Vertically center the content */
    gap: 50px;
    /* Space between .ok1 and .ok2 */
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
    padding-left: 80px;
    padding-right: 80px;
    padding-bottom: 30px;
}


/* .ok1 will contain the profile image or preview */
.ok1 {
    flex-shrink: 0;
    width: 400px;
    /* Ensure .ok1 maintains a fixed width */
    height: 500px;
    border-radius: 50%;
    margin-top: 30px;
    object-fit: cover;
}

/* .ok2 will contain the form fields */
.ok2 {
    flex-grow: 1;

    flex-direction: column;
    text-align: left;
}

.profile-image img,
.ok1 img {
    width: 100%;
    height: 100%;
    border-radius: 50%;
    object-fit: cover;
}

/* Profile details section for viewing */
.profile-details {
    text-align: left;
    flex-grow: 1;
}

/* Styling for input fields */
.ok2 input,
.ok2 textarea {
    padding: 5px;
    font-size: 1.3rem;
    border-radius: 2px;
    border: 1px solid #ccc;
    width: 100%;
}

.okbutton {
    display: block;
    margin-top: 20px;
    padding: 10px 20px;
    background-color: var(--new-color);
    color: white;
    border: none;
    cursor: pointer;
    font-size: 1rem;
}

.okbutton:hover {
    background-color: var(--c-text-action);
}

.update-button {
    display: block;
    margin-top: 20px;
    padding: 10px 20px;
    background-color: var(--new-color);
    color: white;
    border: none;
    cursor: pointer;
    font-size: 1rem;
}

.update-button:hover {
    background-color: var(--c-text-action);
}

.profile-image {
    flex-shrink: 0;
    margin-right: 20px;
    /* Add margin to create gap between the image and content */
}

.ok1 {
    flex-shrink: 0;
    margin-right: 20px;
}

.profile-image img {
    width: 400px;
    margin-top: 30px;
    height: 400px;
    border-radius: 50%;
    object-fit: cover;
}

.ok1 img {
    width: 400px;
    margin-top: 30px;
    height: 400px;
    border-radius: 50%;
    object-fit: cover;
}

.profile-details {
    text-align: left;
    flex-grow: 1;
    /* Allow details to take up remaining space */
}

.ok2 {
    text-align: left;
    flex-grow: 1;
}

.profile-details p {
    margin: 25px 0;
    font-size: 1.3rem;

}

.ok2 input {
    margin: 15px 0;
    font-size: 1.3rem;
}
</style>