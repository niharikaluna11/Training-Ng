<script>
export default {
    name: "ProductList",
    data() {
        return {
            btnClick: () => {
                alert("Clicked!");
            },
            products: [],
            category: [],
            selectedProduct: null,
            selectedCategory: null // To store selected category
        };
    },
    methods: {
        onCategorySelect() {
            if (this.selectedCategory) {
                fetch(`https://dummyjson.com/products/category/${this.selectedCategory.slug}`)
                    .then(res => res.json())
                    .then(data => {
                        console.log(data.products);
                        this.products = data.products;
                    });
            }
        }
    },
    mounted() {
        console.log('Component loaded');
        fetch('https://dummyjson.com/products/categories')
            .then(res => res.json())
            .then(data => {
                console.log(data);
                this.category = data;
            });
    }
}
</script>

<template>
    <section>
        <h1>Products</h1>
        
        <!-- Styled Button -->
        <button @click="btnClick()" class="btn btn-success custom-button">Click me</button>
        
        <!-- Styled Dropdown for categories -->
        <select v-model="selectedCategory" @change="onCategorySelect" class="form-select custom-select">
            <option disabled value="">Select a category</option>
            <option v-for="c in category" :key="c" :value="c">
                {{ c.slug }}
            </option>
        </select>
        
        <!-- Product List Display -->
        <div v-if="products.length">
            <h2>Products in {{ selectedCategory }} category:</h2>
            <ul>
                <li v-for="product in products" :key="product.id">
                    {{ product.title }} - ${{ product.price }}
                </li>
            </ul>
        </div>
        <div v-else-if="selectedCategory">
            <p>No products found for this category.</p>
        </div>
    </section>
</template>

<style>
/* Style for section and container alignment */
section {
    display: flex;
    flex-direction: column;
    align-items: center;
    font-family: Arial, sans-serif;
}

.ok {
    background-color: #218838;
    padding: 10px 20px;
    height: 200px;
}

/* Custom Button Style */
.custom-button {
    padding: 10px 20px;
    background-color: #a72876;
    color: white;
    border: none;
    border-radius: 5px;
    font-size: 16px;
    font-weight: bold;
    cursor: pointer;
    transition: background-color 0.3s ease, transform 0.2s ease;
}

.custom-button:hover {
    background-color: #218838;
    transform: scale(1.05);
}

/* Custom Dropdown Style */
.custom-select {
    width: 100%;
    max-width: 20rem;
    padding: 10px;
    margin: 15px 0;
    border-radius: 5px;
    border: 1px solid #ced4da;
    background-color: #f8f9fa;
    color: #495057;
    font-size: 16px;
    box-shadow: 0px 4px 6px rgba(0, 0, 0, 0.1);
    appearance: none;
    transition: border-color 0.3s ease, box-shadow 0.3s ease;
}

.custom-select:hover {
    border-color: #80bdff;
    box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.2);
}

.custom-select:focus {
    outline: none;
    border-color: #80bdff;
    box-shadow: 0px 0px 5px rgba(0, 123, 255, 0.5);
}
</style>
