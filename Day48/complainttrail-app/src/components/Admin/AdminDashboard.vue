<script>
import AdminSideBar from '../Admin/AdminSideBar.vue';
import AdminHeader from './AdminHeader.vue';

export default {
    name: 'AdminDashboard',
    components: {
        AdminSideBar,
        AdminHeader
    },
    data() {
        return {
            userName: "", // Reactive property to store user's name
        };
    },
    mounted() {
        // Retrieve `given_name` from sessionStorage
        const name = sessionStorage.getItem("username");
        if (name) {
            this.userName = name; // Set the name to the reactive property
        } else {
            this.userName = "Admin"; // Fallback if name is not found
        }
    },
};
</script>


<template>
    <AdminHeader class="header" />

    <main class="main">
        <!-- Admin Side Bar -->
        <AdminSideBar class="admin-sidebar" />

        <!-- Responsive Wrapper -->
        <div class="responsive-wrapper">
            <!-- Main Header -->
            <div class="main-header">
                <h1>G'day, {{ userName }}</h1>
            </div>

            <!-- Helpful Links Section -->
            <div class="content-header">
                <div class="content-header-intro">
                    <h4>Helpful Links,</h4>
                </div>
            </div>
            <div class="content">
                <div class="content-main">
                    <div class="card-grid">
                        <!-- Cards -->
                        <article class="card">
                            <div class="card-header">
                                <div>
                                    <span><img src="https://assets.codepen.io/285131/zeplin.svg" /></span>

                                </div>
                            </div>

                            <div class="card-footer">
                                <router-link to="/AdminProfile" class="nav-link active text-light" aria-current="page">
                                    <a>Account Setting</a>
                                </router-link>

                            </div>

                        </article>
                        <article class="card">
                            <div class="card-header">
                                <div>
                                    <span><img src="https://assets.codepen.io/285131/zeplin.svg" /></span>

                                </div>
                            </div>

                            <div class="card-footer">
                                <router-link to="/ErrorPage" class="nav-link active text-light" aria-current="page">
                                    <a>Help</a>
                                </router-link>
                            </div>

                        </article>


                    </div>
                </div>
            </div>

            <!-- Services Section -->
            <div class="content-header">
                <div class="content-header-intro">

                    <h4>Services,</h4>
                </div>
            </div>
            <div class="content">
                <div class="content-main">
                    <div class="card-grid">
                        <!-- More Cards -->
                        <article class="card">
                            <div class="card-header">
                                <div>
                                    <span><img src="https://assets.codepen.io/285131/zeplin.svg" /></span>

                                </div>
                            </div>

                            <div class="card-footer">
                                <a href="#">View All Users</a>
                            </div>

                        </article>
                        <article class="card">
                            <div class="card-header">
                                <div>
                                    <span><img src="https://assets.codepen.io/285131/zeplin.svg" /></span>

                                </div>
                            </div>

                            <div class="card-footer">
                                <a href="" style="font-size: small;">View All Organizations</a>
                            </div>

                        </article>
                        <article class="card">
                            <div class="card-header">
                                <div>
                                    <span><img src="https://assets.codepen.io/285131/zeplin.svg" /></span>

                                </div>
                            </div>

                            <div class="card-footer">
                                <a href="">View All Complaints</a>
                            </div>

                        </article>
                        <article class="card">
                            <div class="card-header">
                                <div>
                                    <span><img src="https://assets.codepen.io/285131/zeplin.svg" /></span>

                                </div>
                            </div>

                            <div class="card-footer">
                                <a href="">File A Complaint</a>
                            </div>

                        </article>
                        <article class="card">
                            <div class="card-header">
                                <div>
                                    <span><img src="https://assets.codepen.io/285131/zeplin.svg" /></span>

                                </div>
                            </div>

                            <div class="card-footer">
                                <a href="">Knowledge Base</a>
                            </div>

                        </article>

                        <!-- Add other cards as needed -->
                    </div>
                </div>
            </div>
            <div class="content-header">
                <div class="content-header-intro">
                    <h4>Knowledge Base Graph,</h4>
                </div>
            </div>
            <div class="content">
                <div class="content-main">
                    <div class="card-grid">





                    </div>
                </div>
            </div>

        </div>
    </main>
</template>

<style scoped>
.header {
    height: 20px;
}

.main {
    display: flex;
    padding-top: 80px;
    /* flex-direction: column; */
    /* min-height: 100vh;
  background-color: #f4f5f7; */
}

.content {
    margin-top: 0px;
    border-top: 0px;
}

/* Admin Sidebar */
.admin-sidebar {
    width: 16%;
    color: white;
    padding: 20px;
    border-right: 1px solid #d3d3d3;
    /* Light gray border for a subtle effect */
    background-color: #ffffff;
    /* White background to match the style */
    border-radius: 4px;
    /* Optional: adds slightly rounded corners */
    /* box-shadow: 2px 0 5px rgba(0, 0, 0, 0.1); */
}

/* Responsive Wrapper */
.responsive-wrapper {

    padding-left: 40px;
    gap: 5px;
}

/* Header Styles */
.main-header {
    font-size: 2rem;
    font-weight: bold;
}

.content-header {
    padding-top: 0px;
}

/* Content Header */
.content-header-intro h2 {
    font-size: 1.5rem;
    margin-bottom: 10px;
}

/* Card Grid */
.card-grid {
    display: grid;
    grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
    gap: 0px;
}

.card {
    background: white;
    border-radius: 8px;
    border: 1px solid var(--ds-border);
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
    padding: 10px;
    transition: transform 0.2s ease-in-out, box-shadow 0.2s ease-in-out;
    /* Add transition for smooth effects */
}

.card:hover {
    transform: translateY(-3px);
    box-shadow: 0 8px 12px rgba(0, 0, 0, 0.2);
}

.card:hover .card-header img {
    transform: translateY(6px);
    transform: scale(1.3);
}

.card-footer a:hover {
    color: var(--c-text-action);
    text-transform: capitalize;
}

.card-footer {
    text-align: center;
}

.card-footer a {
    color: #3498db;
    text-decoration: none;
    font-weight: bold;
}

/* hjscujv */

.card-header {
    display: flex;
    align-items: flex-start;
    justify-content: space-between;
    padding: 0.5rem 1.25rem 1rem 1.25rem;

    div {
        display: flex;
        align-items: center;

        span {
            width: 40px;
            height: 40px;
            border-radius: 8px;
            display: inline-flex;
            align-items: center;
            justify-content: center;

            img {
                max-height: 100%;
            }
        }

        h3 {
            margin-left: 0.75rem;
            font-weight: 500;
        }
    }
}

.card-header {
    padding: 2rem;
    background-color: var(--ds-surface-sunken);
}

.card-header img {
    width: 40px;
    /* Smaller image size */
    height: 40px;
}


.vertical-tabs {
    display: flex;
    flex-direction: column;

    a {
        display: flex;
        align-items: center;
        padding: 0.75em 1em;
        background-color: transparent;
        border-radius: 8px;
        text-decoration: none;
        font-weight: 500;
        color: var(--c-text-action);
        transition: 0.15s ease;

        &:hover,
        &:focus,
        &.active {
            background-color: var(--c-background-tertiary);
            color: var(--c-accent-primary);
        }

        &+* {
            margin-top: 0.25rem;
        }
    }
}


.card-grid {
    display: grid;
    grid-template-columns: repeat(1, 1fr);

    gap: 18px;

    @media (min-width: 600px) {
        grid-template-columns: repeat(2, 1fr);
    }

    @media (min-width: 1200px) {
        grid-template-columns: repeat(5, 1fr);
    }
}

.card {
    background-color: var(--c-background-primary);
    border-radius: 6px;
    /* Make it smaller */
    overflow: hidden;
    display: flex;
    flex-direction: column;
    width: 180px;
    /* Adjust width to make it smaller */
    height: 150px;
    /* Adjust height to make it smaller */
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    /* Optional for softer shadow */
}

.card-header,
.card-body,
.card-footer {
    display: flex;
    justify-content: center;
    align-items: center;
    text-align: center;
}



.card-body {
    padding: 1rem 1.25rem;
    font-size: 0.875rem;
}

.card-footer {
    margin-top: auto;
    padding: 1rem 1.25rem;
    display: flex;
    align-items: center;
    border-top: 1px solid var(--c-border-primary);

    a {
        color: var(--c-text-action);
        text-decoration: none;
        font-weight: 500;
        font-size: 0.875rem;
    }
}


.horizontal-tabs {
    margin-top: 1.5rem;
    display: flex;
    align-items: center;
    overflow-x: auto;

    @media (max-width: 1000px) {
        scrollbar-width: none;
        position: relative;

        &::-webkit-scrollbar {
            display: none;
        }
    }

    a {
        display: inline-flex;
        flex-shrink: 0;
        align-items: center;
        height: 48px;
        padding: 0 0.25rem;
        font-weight: 500;
        color: inherit;
        border-bottom: 3px solid transparent;
        text-decoration: none;
        transition: 0.15s ease;

        &:hover,
        &:focus,
        &.active {
            color: var(--c-accent-primary);
            border-bottom-color: var(--c-accent-primary);
        }

        &+* {
            margin-left: 1rem;
        }
    }
}

.content-panel {
    display: none;
    max-width: 280px;
    width: 25%;
    padding: 2rem 1rem 2rem 0;
    margin-right: 3rem;

    @media (min-width: 800px) {
        display: block;
    }
}

img {
    display: block;
    max-width: 100%;
}
</style>
