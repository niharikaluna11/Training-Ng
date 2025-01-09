<template>
    <main class="main">
        <div class="content">
            <h1>401 Unauthorized</h1>
            <p>Visit our homepage, get help, or try searching.</p>
            <br>
            <router-link to="/" class="nav-link active text-light" aria-current="page" style="margin-right: 5px;">
                <button>Home</button>
            </router-link>
        </div>
    </main>
</template>

<script>
export default {
    name: "UnauthorizedPage",
}
</script>

<style scoped>
.main {
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100vh;
    text-align: center;
    background-color: #f5f5f5;
}

.content {
    padding: 20px;
    background-color: #fff;
    border-radius: 8px;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}
</style>
