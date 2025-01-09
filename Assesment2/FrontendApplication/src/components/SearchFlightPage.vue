<template>

    <header>
        <h1>Travel Search</h1>
    </header>
    <main class="main">
        <div class="content">
            <p> search input and search button here</p>
            <button class="btn btn-primary" @click="searchFlight">Search Flight</button>
              <button class="btn btn-primary" @click="searchFlight">Search Flight</button>
            
        </div>
    </main>
</template>

<script>

export default {
    name: "SearchFlightPage",

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

    width: 210vh;
    margin-left: 50px;
    margin-right: 40px;
    text-align: center;
    background-color: #f5f5f5;
}

.content {
    padding: 20px;
    display: flex;
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
