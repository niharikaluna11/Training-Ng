<template>
    <AdminHeader class="header" />
    <main class="main">
        <SideBar class="admin-sidebar" />

        <div class="responsive-wrapper">
            <!-- Main Header -->


            <!-- Complaint Form -->
            <form @submit.prevent="submitComplaint" class="complaint-form">


                <div class="form-row">
                    <div class="form-group">
                        <div class="search-dropdown">
                            <div class="dropdown-header" @click="toggleDropdown1">
                                <span>{{ selectedUser?.username || "Select a User" }}</span>
                                <span class="dropdown-icon">▼</span>
                            </div>
                            <div class="dropdown-body" v-if="isDropdownOpen1">
                                <div class="dropdown-search">
                                    <input type="text" v-model="searchQuery" placeholder="Type to search..."
                                        class="search-input" />
                                    <button class="search-button" @click="clearSearch">🔍</button>
                                </div>
                                <ul class="dropdown-list">
                                    <li v-for="user in filteredUsers" :key="user.id" @click="selectUser(user)"
                                        class="dropdown-item">
                                        {{ user.username }}
                                    </li>
                                    <li v-if="filteredUsers.length === 0" class="no-results">No users found</li>
                                </ul>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="dropdown-header" @click="toggleDropdown2">
                            <span>{{ selectedOrganization?.name || "Select an Organization" }}</span>
                            <span class="dropdown-icon">▼</span>
                        </div>
                        <div class="dropdown-body" v-if="isDropdownOpen2">
                            <div class="dropdown-search">
                                <input type="text" v-model="searchQuery" placeholder="Search by organization name..."
                                    class="search-input" />
                                <button class="search-button" @click="clearSearch">🔍</button>
                            </div>
                            <ul class="dropdown-list">
                                <li v-for="org in filteredOrganizations" :key="org.orgId"
                                    @click="selectOrganization(org)" class="dropdown-item">
                                    {{ org.name }}
                                </li>
                                <li v-if="filteredOrganizations.length === 0" class="no-results">No organizations found.
                                </li>
                            </ul>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="dropdown-header" @click="toggleCategoryDropdown">
                            <span>{{ selectedCategory?.name || "Select a Category" }}</span>
                            <span class="dropdown-icon">▼</span>
                        </div>
                        <div class="dropdown-body" v-if="isDropdownOpen3">
                            <div class="dropdown-search">
                                <input type="text" v-model="searchQuery" placeholder="Search by category name..."
                                    class="search-input" />
                                <button class="search-button" @click="clearSearch">🔍</button>
                            </div>
                            <ul class="dropdown-list">
                                <li v-for="category in filteredCategories()" :key="category.id"
                                    @click="selectCategory(category)" class="dropdown-item">
                                    {{ category.name }}
                                </li>
                                <li v-if="filteredCategories().length === 0" class="no-results">No categories found.
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <label for="description">Description:</label>
                    <textarea v-model="formData.description" id="description" required
                        class="description-textarea"></textarea>
                </div>



                <!-- Priority -->
                <div class="form-group">
                    <label for="priority">Priority:</label>
                    <select v-model="formData.priority" id="priority" required>
                        <option value="0">Low</option>
                        <option value="1">Medium</option>
                        <option value="2">High</option>
                    </select>
                </div>

                <!-- Comment by User -->
                <div class="form-group">
                    <label for="commentByUser">Comment by User:</label>
                    <textarea v-model="formData.commentByUser" id="commentByUser"></textarea>
                </div>

                <!-- Attachments -->
                <div class="form-group">
                    <label for="attachmentUrl">Attachments:</label>
                    <input type="file" multiple @change="handleFileUpload" id="attachmentUrl" />
                </div>

                <!-- Terms and Conditions -->
                <div class="form-group">
                    <label>
                        <input type="checkbox" v-model="formData.acceptTermsAndConditions" required />
                        Accept Terms and Conditions
                    </label>
                </div>

                <!-- Submit Button -->
                <button type="submit">Submit</button>
            </form>
        </div>
    </main>

</template>

<script>
import { fetchCategories } from '@/scripts/GetALL/ComplaintCategoryService';
import AdminHeader from './AdminHeader.vue';
import SideBar from './SideBar.vue';


import { fileComplaint } from '@/scripts/FileComplaint';
import { getAllOrganizationsProfiles, getAllUsersProfiles } from '@/scripts/GetALL/GetAllprofile';

export default {
    name: "AdminFileComplaint",
    components: {
        SideBar,
        AdminHeader,

    },
    data() {

        return {
            userName: "",
            searchQuery: "",
            users: [],
            categories: [],  // Array to store fetched categories
            selectedCategory: null,
            organizations: [],
            selectedOrganization: null,
            isDropdownOpen1: false,
            isDropdownOpen2: false,
            isDropdownOpen3: false,
            pageNum: 1,  // Pagination page number
            pageSize: 10,

            selectedUser: null,
            formData: {
                userId: null,
                organizationId: null,
                categoryId: null,
                description: "",
                priority: 0,
                status: 0,
                commentByUser: "",
                attachmentUrl: [],
                acceptTermsAndConditions: false,
            },
        };
    },
    mounted() {
        // Retrieve `username` from sessionStorage
        const name = sessionStorage.getItem("username");
        this.userName = name || "Admin";
        this.fetchUsers();
        this.fetchOrganizations();
        this.fetchCategories();
    },

    computed: {
        filteredUsers() {
            // Filter users by username based on the search query
            return this.users.filter(user =>
                user.username?.toLowerCase().includes(this.searchQuery.toLowerCase())
            );
        },
        filteredOrganizations() {
            // Filter organizations based on search query
            return this.organizations.filter(org =>
                org.name.toLowerCase().includes(this.searchQuery.toLowerCase())
            );
        },
    },

    methods: {
        async fetchCategories() {
            // Make sure token key is correct
            try {
                const response = await fetchCategories(this.pageNum, this.pageSize);
                this.categories = response;
                // Assuming the response directly contains category data

                if (this.categories) {
                    this.categories = this.categories.$values.map(category => ({
                        id: category.categoryId,
                        categoryId: category.categoryId,
                        name: category.name,
                        description: category.description
                    }));
                } else {
                    console.error("Invalid response format or no categories found.");
                }

            } catch (error) {
                console.error(error);
            }
        },




        async fetchUsers() {
            try {
                const response = await getAllUsersProfiles(); // Fetch user data from API
                this.users = response.$values.map(user => ({
                    id: user.id,
                    userId: user.userId, // Unique user ID for formData
                    username: user.firstName + " " + user.lastName, // Construct username
                }));
            } catch (error) {
                console.error("Error fetching user profiles:", error);
            }
        },
        async fetchOrganizations() {
            try {
                const organizationsData = await getAllOrganizationsProfiles();
                if (organizationsData?.$values?.length > 0) {
                    this.organizations = organizationsData.$values.map(org => ({
                        orgId: org.orgId,
                        name: org.name,
                    }));
                } else {
                    this.errorMessage = "No organizations found.";
                    this.organizations = [];
                }
            } catch (error) {
                this.errorMessage = "Failed to fetch organizations.";
                console.error("Error fetching organizations:", error);
            }
        },

        selectOrganization(org) {
            this.selectedOrganization = org;
            this.formData.organizationId = org.orgId; // Set the selected organization's ID in the form
            this.isDropdownOpen2 = false; // Close the dropdown
        },
        selectUser(user) {
            // Assign selected user to formData and update UI
            this.selectedUser = user;
            this.formData.userId = user.userId;
            this.isDropdownOpen1 = false; // Close dropdown after selection
        },
        selectCategory(category) {
            this.selectedCategory = category;  // Set the selected category
            this.formData.categoryId = category.categoryId;  // Ensure formData is updated
            this.isDropdownOpen3 = false;  // Close the dropdown after selection
        }
        ,
        toggleDropdown1() {
            this.isDropdownOpen1 = !this.isDropdownOpen1;// Toggle dropdown visibility
        },
        toggleDropdown2() {
            this.isDropdownOpen2 = !this.isDropdownOpen2; // Toggle dropdown visibility
        },
        toggleCategoryDropdown() {
            this.isDropdownOpen3 = !this.isDropdownOpen3;
        },

        filteredCategories() {
            return this.categories.filter(category =>
                category.name.toLowerCase().includes(this.searchQuery.toLowerCase())
            );
        },
        clearSearch() {
            this.searchQuery = ""; // Clear search input
        },




        handleFileUpload(event) {
            this.formData.attachmentUrl = Array.from(event.target.files);
        },
        async submitComplaint() {
            if (!this.formData.categoryId) {
                alert("Please select a category.");
                return; // Prevent submission
            }

            try {
                await fileComplaint(this.formData); // Call the service to submit the complaint
                alert("Complaint filed successfully!");
                this.$router.push("/AdminViewComplaints"); // Redirect to the view complaints page
            } catch (error) {
                console.error("Error filing complaint:", error);
                alert("Failed to file complaint. Please try again.");
            }
        }

    },
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


.form-row {
    display: flex;
    gap: 10px;
}

.form-group {
    flex: 1;
    margin-bottom: 10px;
}

.text-center {
    text-align: center;
}

.description-textarea {
    min-height: 150px;
    font-size: 1rem;
    padding: 10px;
    border: 1px solid #ccc;
    border-radius: 5px;
}

.form-group {
    flex: 1;
}


.complaint-form {
    width: 90%;
    margin: 20px auto;
    display: flex;
    flex-direction: column;
    gap: 15px;
}

.dropdown-header {
    padding: 10px;
    font-size: 1rem;
    background-color: #f8f8f8;
    border: 1px solid #ccc;
    border-radius: 5px;
    cursor: pointer;
}

.form-group label {
    font-weight: bold;
    margin-bottom: 5px;
    display: block;
}

.form-group input,
.form-group select,
.form-group textarea {
    width: 100%;
    padding: 8px;
    font-size: 1rem;
    border: 1px solid #ccc;
    border-radius: 5px;
}

button {
    padding: 10px 15px;
    font-size: 1rem;
    background-color: var(--c-text-secondary);
    color: white;
    border: none;
    border-radius: 5px;
    cursor: pointer;
}

button:hover {
    background-color: var(--c-text-action);
}


/* bvsagcdv */

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
