<template>
    <AdminHeader class="header" />
    <main class="main">
        <SideBar class="admin-sidebar" />

        <div class="responsive-wrapper">
            <div>

                <div class="category-header">
                    <h2>Complaint Categories</h2>
                    <button class="create-category-btn" @click="openModal">Create Category</button>
                </div>


                <div v-if="isModalOpen" class="modal-overlay" @click="closeModal">
                    <div class="modal-content" @click.stop>
                        <h2>Create Complaint Category</h2>
                        <form @submit.prevent="createCategoryHandler">
                            <div>
                                <label for="name">Name:</label>
                                <input type="text" id="name" v-model="category.name" required />
                            </div>
                            <div>
                                <label for="description">Description:</label>
                                <input type="text" id="description" v-model="category.description" required />
                            </div>
                            <div class="modal-buttons">
                                <button type="submit">Create Category</button>
                                <button type="button" @click="closeModal">Cancel</button>
                            </div>
                        </form>
                    </div>
                </div>
                <table v-if="categories.length > 0">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Description</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(item, index) in categories" :key="index">
                            <td>{{ item.name }}</td>
                            <td>{{ item.description }}</td>
                        </tr>
                    </tbody>
                </table>
                <p v-else>No categories available</p>



            </div>
        </div>
    </main>
</template>

<script>
import { createCategory, fetchCategories } from "@/scripts/GetALL/ComplaintCategoryService"; // Adjust path if necessary
import AdminHeader from './AdminHeader.vue';
import SideBar from './SideBar.vue';

export default {
    name: "ComplaintCategory",
    components: {
        SideBar,
        AdminHeader,
    },
    data() {
        return {
            category: {
                name: "",
                description: "",
            },
            categories: [],
            pageNum: 1,
            pageSize: 9,
            isModalOpen: false,
        };
    },
    created() {
        this.fetchCategories();
    },
    methods: {
        openModal() {
            this.isModalOpen = true;
        },
        closeModal() {
            this.isModalOpen = false;
        },
        async createCategoryHandler() {
            const categoryData = {
                name: this.category.name,
                description: this.category.description,
            };

            try {
                await createCategory(categoryData);  // Calls the service function to create a category
                this.category.name = "";
                this.category.description = "";
                this.closeModal();
                this.fetchCategories();  // Reload categories after creation
            } catch (error) {
                console.error(error);
                alert("Failed to create category.");
            }
        },
        async fetchCategories() {
            const token = sessionStorage.getItem("Token");  // Make sure token key is correct
            try {
                this.categories = await fetchCategories(this.pageNum, this.pageSize, token);
            } catch (error) {
                console.error(error);
                alert("Failed to fetch categories.");
            }
        },
    },
};
</script>

<style scoped>
.header {
    height: 20px;
    padding: 20px;
}

.category-header {
    display: flex;
    align-items: center;
    left: 10px;
    gap: 800px;
    /* Adds some space between the h2 and button */
}

.admin-sidebar {
    border-right: 1px solid #d3d3d3;
    width: 60px;
}

.main {
    display: flex;
    padding-top: 50px;
}

.responsive-wrapper {
    padding: 20px;
}

.responsive-wrapper h2 {
    color: #333;
    /* Darker text for headings */
    margin-bottom: 16px;
    /* Space below headings */
    font-family: Arial, sans-serif;
    font-size: 1.5rem;
    /* Slightly larger font size for prominence */
}

.create-category-btn {
    background-color: #007bff;
    /* Primary color */
    color: white;
    border: none;
    padding: 10px 20px;
    border-radius: 4px;
    cursor: pointer;
    font-size: 1rem;
    transition: background-color 0.3s ease;
}

.create-category-btn:hover {
    background-color: #0056b3;
    /* Darker shade on hover */
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
    align-items: center;
    justify-content: center;
    z-index: 1000;
}

.modal-content {
    background: white;
    padding: 20px;
    border-radius: 8px;
    width: 100%;
    max-width: 500px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
    /* More prominent shadow for modal */
}

.modal-content h2 {
    margin-bottom: 16px;
    font-size: 1.25rem;
    color: #444;
}

.modal-buttons button {
    margin-top: 16px;
    padding: 8px 16px;
    font-size: 1rem;
    border-radius: 4px;
    border: none;
    cursor: pointer;
    transition: background-color 0.3s ease;
}

.modal-buttons button[type="submit"] {
    background-color: #28a745;
    /* Success color */
    color: white;
}

.modal-buttons button[type="submit"]:hover {
    background-color: #218838;
    /* Darker green */
}

.modal-buttons button[type="button"] {
    background-color: #dc3545;
    /* Danger color */
    color: white;
    margin-left: 8px;
}

.modal-buttons button[type="button"]:hover {
    background-color: #c82333;
    /* Darker red */
}

table {
    width: 100%;
    border-collapse: collapse;
    margin-top: 16px;
    background: white;
    border-radius: 8px;
    overflow: hidden;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

table thead {
    background-color: #007bff;
    color: white;
    text-align: left;
}

table th,
table td {
    padding: 12px;
    border-bottom: 1px solid #ddd;
}

table tr:last-child td {
    border-bottom: none;
    /* Remove border for the last row */
}

table tr:hover {
    background-color: #f1f1f1;
    /* Highlight on hover */
}

p {
    font-size: 1rem;
    color: #666;
    margin-top: 16px;
    text-align: center;
}
</style>