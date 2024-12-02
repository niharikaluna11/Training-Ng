<template>
    <BaseHeader class="header" />
    <main class="main">
        <div class="responsive-wrapper">
            <div class="content-header">
                <div class="content-header-intro">
                    <div>
                        <h1>404 Page Not Found</h1>
                        <p>Sorry, the page you're looking for cannot be found.</p>
                        <p>Visit our homepage, get help, or try searching.</p>

                        <br>
                        <router-link to="/HomePage" class="nav-link active text-light" aria-current="page"
                            style="margin-right: 5px;">
                            <button>
                                Home
                            </button>
                        </router-link>
                        <router-link to="/HelpPage" class="nav-link active text-light" aria-current="page">
                            <button>
                                Help
                            </button>
                        </router-link>
                    </div>
                </div>
                <div class="content-header-actions">

                    <img src="@/Images/errorimages/404errorpage.jpg" alt="ERROR" />

                </div>
            </div>
        </div>
    </main>
</template>

<script>
import BaseHeader from './BaseHeader.vue';

export default {
    name: "ErrorPage",
    components: {
        BaseHeader,
    },
}
</script>

<style scoped>
.main {
    padding: 0px;
}

body {
    background-color: #fff;
}

.content-header {
    padding-left: 30px;
    gap: 10px;
    background-color: #fff;
}

button {
    background-color: var(--c-text-action);
    border: none;
    padding: 10px 20px;
    border-radius: 5px;
    cursor: pointer;
    transition: background-color 0.3s ease;
    margin-top: 20px;
    color: whitesmoke;
    /* Add margin to create space between heading and button */
}

button:hover {
    background-color: var(--c-text-secondary);
    color: whitesmoke;
}

button .nav-link {
    color: #fff;
    text-decoration: none;
}

button .nav-link:hover {
    text-decoration: underline;
}
</style>