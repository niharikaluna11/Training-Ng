<script>
import OrgHeader from './OrgHeader.vue';
import OrgMainPage from './OrgMainPage.vue';
import OrgSideBar from './OrgSideBar.vue';

export default {
    name: 'OrganizationDashboard',
    components: {
        OrgSideBar, OrgHeader, OrgMainPage
    }
};
</script>


<template>

    <OrgHeader />

    <main class="main">
        <!-- Admin Side Bar -->
        <OrgSideBar class="admin-sidebar" />

        <!-- Responsive Wrapper -->
        <div class="responsive-wrapper">
            <OrgMainPage />
        </div>
    </main>
</template>

<style scoped>
.header {
    height: 60px;
    padding-top: 0px;
}

.admin-sidebar {
    border-right: 1px solid #d3d3d3;
    width: 60px;
}

.main {
    display: flex;
    padding-top: 60px;

}

.content {
    margin-top: 0px;
    border-top: 0px;
}

.responsive-wrapper {

    padding-left: 40px;
    gap: 5px;
}
</style>
