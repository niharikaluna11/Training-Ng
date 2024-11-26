<template>
    <div>
        <button class="create-category-btn" @click="openModal">Create Category</button>

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

        <h2>All Complaint Categories</h2>
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
</template>

<script>
import { createCategory, fetchCategories } from "@/scripts/ComplaintCategoryService"; // Adjust path if necessary

export default {
    name: "ComplaintCategory",

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