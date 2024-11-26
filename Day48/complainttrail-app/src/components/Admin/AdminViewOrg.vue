<template>
    <div class="responsive-wrapper">
        <!-- Main Header -->
        <div class="main-header">
            <h1>G'day, {{ userName }}</h1>
        </div>

        <div class="content-header">
            <div class="content-header-intro">
                <h4>Helpful Links,</h4>
            </div>
        </div>

        <div class="content">
            <div class="content-main">
                <div class="card-grid">
                    <article class="card">
                        <div class="card-header">
                            <span><img src="https://assets.codepen.io/285131/zeplin.svg" /></span>
                        </div>
                        <div class="card-footer">
                            <router-link to="/admin-profile" class="nav-link active text-light" aria-current="page">
                                <a>Account Setting</a>
                            </router-link>
                        </div>
                    </article>
                    <article class="card">
                        <div class="card-header">
                            <span><img src="https://assets.codepen.io/285131/zeplin.svg" /></span>
                        </div>
                        <div class="card-footer">
                            <router-link to="/error-page" class="nav-link active text-light" aria-current="page">
                                <a>Help</a>
                            </router-link>
                        </div>
                    </article>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: 'ViewOrgViewUser',
    data() {
        return {
            userName: 'Admin', // You can replace this with dynamic data as needed
        };
    },
};
</script>

<style scoped>
/* Add specific styles for content grid, cards, and other UI elements */
.card-grid {
    display: grid;
    grid-template-columns: repeat(2, 1fr);
    gap: 20px;
}

.card {
    background: white;
    border-radius: 8px;
    padding: 10px;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
    transition: transform 0.2s, box-shadow 0.2s;
}

.card:hover {
    transform: translateY(-3px);
    box-shadow: 0 8px 12px rgba(0, 0, 0, 0.2);
}
</style>
