<script>
import AdminHeader from './AdminHeader.vue';
import AdminMainPage from './AdminMainPage.vue';
import SideBar from './SideBar.vue';
export default {
    name: 'AdminDashboard',
    components: {
        SideBar,
        AdminHeader,
        AdminMainPage
    },
    data() {
        return {
            userName: "",
        };
    },
    mounted() {
        const name = sessionStorage.getItem("username");
        if (name) {
            this.userName = name;
        } else {
            this.userName = "Admin";
        }
    },
};
</script>

<template>
    <AdminHeader class="header" />

    <main class="main">

        <SideBar class="admin-sidebar" />

        <div class="responsive-wrapper">
            <AdminMainPage />
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