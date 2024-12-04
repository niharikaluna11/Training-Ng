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
                <div v-if="categories.length > 0">
                    <table>
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
                    <div class="pagination-controls">
                        <button :disabled="pageNum === 1" @click="changePage('prev')">
                            Previous
                        </button>
                        <span>Page {{ pageNum }}</span>
                        <button :disabled="categories.length < pageSize" @click="changePage('next')">
                            Next
                        </button>
                    </div>
                </div>

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
            pageSize: 6,
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
            try {
                this.categories = await fetchCategories(this.pageNum, this.pageSize);
            } catch (error) {
                console.error(error);
                alert("Failed to fetch categories.");
            }
        },
        changePage(direction) {
            if (direction === 'prev' && this.pageNum > 1) {
                this.pageNum--;
            } else if (direction === 'next') {
                this.pageNum++;
            }
            this.fetchCategories(); // Fetch new categories based on updated page number
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
    /* Adds space between the h2 and button */
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
    color: var(--c-text-secondary);
    /* Updated to secondary color */
    margin-bottom: 16px;
    /* Space below headings */
    font-family: Arial, sans-serif;
    font-size: 1.5rem;
    /* Larger font size for prominence */
}

.create-category-btn {
    background-color: var(--c-text-secondary);
    /* Primary action color */
    color: white;
    border: none;
    padding: 10px 20px;
    border-radius: 4px;
    cursor: pointer;
    font-size: 1rem;
    transition: background-color 0.3s ease;
}

.create-category-btn:hover {
    background-color: var(--c-text-action);
    /* New color on hover */
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
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.3);
    /* Prominent shadow */
}

.modal-content h2 {
    margin-bottom: 16px;
    font-size: 1.25rem;
    color: var(--c-text-secondary);
    /* Secondary color for headings */
}

.modal-content form {
    display: flex;
    flex-direction: column;
    gap: 10px;
}

.modal-content input,
.modal-content textarea {
    width: 100%;
    /* Align input and textarea widths */
    padding: 8px;
    border: 1px solid var(--c-text-secondary);
    /* Subtle border color */
    border-radius: 4px;
    font-size: 1rem;
}

.modal-content textarea {
    height: 100px;
    resize: none;
}

.modal-buttons {
    display: flex;
    justify-content: flex-end;
    gap: 10px;
}

.modal-buttons button {
    padding: 8px 16px;
    font-size: 1rem;
    border-radius: 4px;
    border: none;
    cursor: pointer;
    transition: background-color 0.3s ease;
}

.modal-buttons button[type="submit"] {
    background-color: var(--c-text-secondary);
    /* Action color */
    color: white;
}

.modal-buttons button[type="submit"]:hover {
    background-color: var(--c-text-action);
    /* New color on hover */
}

.modal-buttons button[type="button"] {
    background-color: #e9606d;
    /* Danger color */
    color: white;
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
    /* Subtle table shadow */
}

table thead {
    background-color: var(--c-text-action);
    /* Action color for header */
    color: white;
    text-align: left;
}

table th,
table td {
    padding: 12px;
    border-bottom: 1px solid var(--c-text-secondary);
    /* Secondary color for borders */
}

table tr:last-child td {
    border-bottom: none;
    /* Remove border for the last row */
}



p {
    font-size: 1rem;
    color: var(--c-text-secondary);
    /* Subtle color for text */
    margin-top: 16px;
    text-align: center;
}



.pagination-controls {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-top: 16px;
}

.pagination-controls button {
    padding: 8px 16px;
    font-size: 1rem;
    border-radius: 4px;
    border: 1px solid var(--c-text-secondary);
    background-color: var(--c-text-action);
    color: white;
    cursor: pointer;
    transition: background-color 0.3s ease;
}

.pagination-controls button:disabled {
    background-color: var(--c-text-secondary);
    cursor: not-allowed;
}

.pagination-controls span {
    font-size: 1rem;
    color: var(--c-text-secondary);
}
</style>