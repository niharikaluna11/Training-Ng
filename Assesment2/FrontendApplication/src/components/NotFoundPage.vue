<template>
    <main class="main">
        <div class="content">
            <h1>404 Not Found</h1>
            <p>Visit our homepage, get help, or try searching.</p>
            <br>
            <router-link to="/" class="nav-link active text-light" aria-current="page" style="margin-right: 5px;">
                <button class="btn btn-primary">Home</button>
            </router-link>
        </div>
    </main>
</template>


<script>
export default {
    name: "NotFoundPage",
}
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
</style>