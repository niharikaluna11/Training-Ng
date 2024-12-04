<template>
    <OrgHeader class="header" />
    <main class="main">
        <OrgSideBar class="admin-sidebar" />

        <div class="responsive-wrapper">
            <div class="content-header">
                <div class="content-header-intro">
                    <h4> Information, </h4>
                </div>
            </div>
            <div class="content">
                <div class="content-main">
                    <div class="card-grid">
                        <!-- Cards -->
                        <article class="card">
                            <div class="card-header">
                                {{ totalCount }}
                            </div>

                            <div class="card-footer">
                                <a>Total Complaints</a>
                            </div>
                        </article>

                        <article class="card">
                            <div class="card-header">
                                {{ organizationcount }}
                            </div>

                            <div class="card-footer">
                                <a>Total Organizations</a>
                            </div>
                        </article>

                        <article class="card">
                            <div class="card-header">
                                {{ usercount }}
                            </div>

                            <div class="card-footer">
                                <a>Total Users</a>
                            </div>
                        </article>

                    </div>
                </div>
            </div>
        </div>
    </main>
</template>

<script>
import { getAllComplaints } from '@/scripts/GetALL/GetAllComplaints';

import { getDash } from '@/scripts/GetALL/DashboardService';
import OrgHeader from './OrgHeader.vue';
import OrgSideBar from './OrgSideBar.vue';

export default {
    name: "OrgKnowledgeBase",
    components: {
        OrgHeader, OrgSideBar
    },
    data() {
        return {
            totalCount: 0,
            usercount: 0,
            organizationcount: 0,
            details: {}, // Holds the detailed count of complaints, users, and organizations
        };
    },

    mounted() {
        this.loadComplaints();
        this.loadDetails(); // Fetch details when component is mounted
    },
    methods: {
        async loadComplaints() {
            try {
                const { totalCount } = await getAllComplaints(); // Call the imported function
                this.totalCount = totalCount; // Set the total count of complaints
                console.log("Total Complaints:", this.totalCount);
            } catch (error) {
                console.error("Error loading complaints:", error);
            }
        },

        async loadDetails() {
            try {
                const response = await getDash(); // Call the imported function
                this.details = response; // Set the total count details
                this.usercount = response.totalUsers || 0; // Safely set user count
                this.organizationcount = response.totalOrganizations || 0; // Safely set organization count
                console.log("Details:", this.details); // Optional: For debugging
            } catch (error) {
                console.error("Error loading details:", error);
            }
        },
    },
}
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
}

.admin-sidebar {
    border-right: 1px solid #d3d3d3;
    width: 60px;
}

.card {
    height: auto;
}

.main {
    display: flex;
    padding-top: 50px;
}

.responsive-wrapper {
    padding: 20px;
}
</style>
