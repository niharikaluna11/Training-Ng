// src/router.js
import { createRouter, createWebHistory } from "vue-router";
import HomePage from "@/components/HomePage.vue";
import LoginComponent from "@/components/Login/LoginComponent.vue";
import RegisterComponent from "@/components/Login/RegisterComponent.vue";
import UserDashboard from "./components/User/UserDashboard.vue";
import AdminDashboard from "./components/Admin/AdminDashboard.vue";
import OrganizationDashboard from "./components/Organization/OrganizationDashboard.vue";
import AdminSideBar from "./components/Admin/AdminSideBar.vue";
import UserSideBar from "./components/User/UserSideBar.vue";
import OrgSideBar from "./components/Organization/OrgSideBar.vue";
import ErrorPage from "./components/ErrorPage.vue";
import ForgotPassword from "./components/Login/ForgotPassword/ForgotPassword.vue";
import AdminProfile from "./components/Admin/AdminProfile.vue";
import AdminViewUser from "./components/Admin/AdminViewUser.vue";
import AdminViewOrg from "./components/Admin/AdminViewOrg.vue";

const routes = [
    { path: '/', component: HomePage },
    { path: '/HomePage', component: HomePage },
    { path: '/Login', component: LoginComponent },
    { path: '/Register', component: RegisterComponent },
    { path: '/UserDashboard', component: UserDashboard },
    { path: '/AdminDashboard', component: AdminDashboard },
    { path: '/OrganizationDashboard', component: OrganizationDashboard },
    { path: '/AdminSideBar', component: AdminSideBar },
    { path: '/UserSideBar', component: UserSideBar },
    { path: '/OrgSideBar', component: OrgSideBar },
    { path: '/ErrorPage', component: ErrorPage },
    { path: '/ForgotPassword', component: ForgotPassword },
    { path: '/AdminProfile', component: AdminProfile },
    { path: '/AdminViewUser', component: AdminViewUser },
    { path: '/AdminViewOrg', component: AdminViewOrg },
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;
