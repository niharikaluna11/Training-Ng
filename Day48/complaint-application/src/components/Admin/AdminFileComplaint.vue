<template>
    <AdminHeader class="header" />
    <main class="main">
        <SideBar class="admin-sidebar" />

        <div class="responsive-wrapper">
            <!-- Main Header -->


            <!-- Complaint Form -->
            <form @submit.prevent="submitComplaint" class="complaint-form">
                <!-- User ID and Organization ID on the same row -->
                <div class="form-row">
                    <div class="form-group">
                        <label for="userId">User ID:</label>
                        <input type="number" v-model="formData.userId" id="userId" required style="width: 90%;" />
                    </div>
                    <div class="form-group">
                        <label for="organizationId">Organization ID:</label>
                        <input type="number" v-model="formData.organizationId" id="organizationId" required />
                    </div>
                </div>

                <!-- Category ID -->
                <div class="form-group">
                    <label for="categoryId">Category ID:</label>
                    <input type="number" v-model="formData.categoryId" id="categoryId" required />
                </div>

                <!-- Description -->
                <div class="form-group">
                    <label for="description">Description:</label>
                    <textarea v-model="formData.description" id="description" required></textarea>
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
import AdminHeader from './AdminHeader.vue';
import SideBar from './SideBar.vue';

import { fileComplaint } from '@/scripts/FileComplaint';
export default {
    name: "AdminFileComplaint",
    components: {
        SideBar,
        AdminHeader,
    },
    data() {
        return {
            userName: "",
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
    },
    methods: {
        handleFileUpload(event) {
            this.formData.attachmentUrl = Array.from(event.target.files);
        },
        async submitComplaint() {
            try {
                await fileComplaint(this.formData); // Call the service to submit the complaint
                alert("Complaint filed successfully!");
                this.$router.push("/AdminViewComplaints"); // Redirect to the view complaints page
            } catch (error) {
                console.error("Error filing complaint:", error);
                alert("Failed to file complaint. Please try again.");
            }
        },
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

.complaint-form {
    max-width: 500px;
    margin: 0 auto;
    display: flex;
    flex-direction: column;
}

.form-group {
    margin-bottom: 15px;
}

.text-center {
    text-align: center;
}

.form-row {
    display: flex;
    justify-content: space-between;
    gap: 5px;
}

.form-group {
    flex: 1;
}

.complaint-form {
    max-width: 600px;
    margin: 0 auto;
    display: flex;
    flex-direction: column;
    gap: 15px;
    /* Adds consistent spacing between form fields */
}

button {
    align-self: center;
    /* Centers the submit button */
    padding: 10px 20px;
    font-size: 1rem;
    background-color: #007bff;
    color: white;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    transition: background-color 0.3s ease;
}

button:hover {
    background-color: #0056b3;
}

.form-group label {
    display: block;
    margin-bottom: 5px;
    font-weight: bold;
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
    background-color: #007bff;
    color: white;
    border: none;
    border-radius: 5px;
    cursor: pointer;
}

button:hover {
    background-color: #0056b3;
}
</style>
