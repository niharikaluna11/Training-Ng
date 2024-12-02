<template>
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
                    <router-link to="/HomePage" class="nav-link active text-light" aria-current="page">
                        Home
                    </router-link>
                </nav>
                <div class="header-navigation-actions">
                    <a href="#" class="text" style="padding-right: 15px;">
                        Hi, {{ userName }}
                    </a>

                    <div id="app">
                        <!-- Avatar -->
                        <div class="avatar-container" @click="toggleDropdown">
                            <div class="avatar">
                                <img :src="profilePicUrl" alt="Profile Picture" />
                            </div>
                        </div>

                        <!-- Dropdown Modal -->
                        <div v-if="isDropdownOpen" class="dropdown-modal">
                            <img :src="profilePicUrl" alt="Profile Picture" class="modal-profile-pic" />

                            <p class="email">{{ username }}</p>
                            <ul class="dropdown-options">
                                <li @click="viewProfile">Profile</li>
                                <li @click="logOut">Log Out</li>
                            </ul>
                        </div>
                    </div>


                </div>
            </div>
        </div>
    </header>
</template>

<script>
import { getProfilePic } from '@/scripts/GetALL/GetProfilePic';
import { toast } from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';
export default {
    name: 'AdminHeader',
    data() {
        return {
            userName: localStorage.getItem("username") || "Guest",
            profilePicUrl: "profilepicimg.jpg", // Default avatar
            isDropdownOpen: false,
        };
    },
    methods: {
        toggleDropdown() {
            this.isDropdownOpen = !this.isDropdownOpen; // Toggle dropdown visibility
        },
        viewProfile() {
            console.log("Viewing profile...");
            this.$router.push({ path: '/AdminProfile' });
        }, logOut() {
            console.log("Logging out...");
            localStorage.clear();
            sessionStorage.clear();

            this.$router.push('/');
            toast.success(
                `Logged Out Succesfully`,
                {
                    rtl: true,
                    limit: 2,
                    position: toast.POSITION.TOP_CENTER,
                }
            );
        },
    },
    async mounted() {
        const username = localStorage.getItem("username");

        if (!username) {
            console.error("No username found in localStorage");
        }

        if (username) {
            try {
                const response = await getProfilePic(username);
                const profilePicture = response.data.profilePicture;
                this.profilePicUrl = profilePicture ? profilePicture : "profilepicimg.jpg";
            } catch (error) {
                console.error("Failed to fetch profile picture:", error);
            }
        }
    },

};
</script>

<style scoped>
/* Add your header styles here */
.header {
    padding-top: 50px;
}

.header-content {
    display: flex;
    justify-content: space-between;
    align-items: center;
}

.header-logo img {
    max-height: 50px;
}

.header-navigation-actions .text {
    text-decoration: none;
    /* Removes underline */
    color: #060000;
    /* Change text color if needed */
}

.header-navigation-actions .text:hover {
    color: #0f264b;
    /* Change color on hover if desired */
}

.modal-profile-pic {

    border-radius: 50%;
    display: block;
}

/* Avatar Styling */
.avatar-container {
    position: relative;
    display: inline-block;
    cursor: pointer;
}

.avatar img {
    width: 50px;
    height: 50px;
    border-radius: 50%;
    display: block;
}

/* Dropdown Modal Styling */
.dropdown-modal {
    position: absolute;
    top: 60px;
    right: 0;
    width: 200px;
    background: #fff;
    border: 1px solid #ddd;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
    border-radius: 8px;
    padding: 10px;
    z-index: 1000;
    text-align: left;
}

.email {
    font-weight: bold;
    margin-bottom: 10px;
}

.dropdown-options {
    list-style: none;
    padding: 0;
    margin: 0;
    align-items: center;
}

.dropdown-options li {
    padding: 10px;
    cursor: pointer;
    border-radius: 4px;
}

.dropdown-options li:hover {
    background: #f0f0f0;
}

/* Close Dropdown on Click Outside */
body {
    position: relative;
}
</style>
