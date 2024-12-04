<script>
import UserHeader from './UserHeader.vue';
import UserMainPage from './UserMainPage.vue';
import UserSideBar from './UserSideBar.vue';


export default {
    name: 'UserDashboard',
    components: {
        UserSideBar,
        UserHeader,
        UserMainPage
    }
};
</script>


<template>

    <UserHeader class="header" />

    <main class="main">
        <!-- Admin Side Bar -->
        <UserSideBar class="admin-sidebar" />

        <!-- Responsive Wrapper -->
        <div class="responsive-wrapper">


            <UserMainPage />

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
