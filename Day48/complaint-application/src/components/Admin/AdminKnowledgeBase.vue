<script>
import AdminHeader from './AdminHeader.vue';
import SideBar from './SideBar.vue';


export default {
    name: "AdminKnowledgeBase",
    components: {
        AdminHeader,
        SideBar
    }
}
</script>

<template>
    <AdminHeader class="header" />
    <main class="main">
        <SideBar class="admin-sidebar" />

        <div class="responsive-wrapper">

        </div>
    </main>
</template>



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
    /* Adds some space between the h2 and button */
}

.admin-sidebar {
    border-right: 1px solid #d3d3d3;
    width: 60px;
}

.main {
    display: flex;
    padding-top: 50px;
}

.responsive-wrapper {
    padding: 20px;
}
</style>