<template>

    <header>

    </header>
    <main class="main">
        <div class="content">
            <h1> Click on me </h1>
            <button class="btn btn-primary" @click="searchFlight">Search Flight</button>
            <button class="btn btn-disabled" disabled>Search Hotel</button>
        </div>
    </main>
</template>

<script>
import { toast } from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';

export default {
    name: "MainPage",
    methods: {
        searchFlight() {

            toast.success("Redirecting to Flight Main Page", {
                rtl: true,
                limit: 2,
                position: toast.POSITION.TOP_CENTER,
                autoClose: 1000,
            });


            setTimeout(() => {
                this.$router.push("/search-flight");
            }, 1000);
        },
    },
};
</script>

<style scoped>
header h1 {
    text-align: center;
    font-size: 2rem;
    color: var(--c-text-primary, #333);

}

.main {

    justify-content: center;
    align-items: center;

    width: 100vh;
    margin-left: 450px;
    text-align: center;
    background-color: #f5f5f5;
}

.content {
    margin-top: 160px;
    padding: 20px;
    background-color: #fff;
    border-radius: 8px;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

.btn {
    padding: 10px 20px;
    font-size: 1rem;
    margin: 10px 0;
    margin-left: 10px;
    border: none;
    border-radius: 5px;
    cursor: pointer;
}

.btn-primary {
    background-color: var(--c-text-action);
    color: white;
}

.btn-primary:hover {
    background-color: var(--c-text-secondary);
}

.btn-disabled {
    background-color: var(--grey);
    color: black;
    cursor: not-allowed;
}
</style>