// src/router.js
import { createRouter, createWebHistory } from "vue-router";
import HomePage from "@/components/HomePage.vue";
import LoginComponent from "@/components/Login/LoginComponent.vue";
import RegisterComponent from "@/components/Login/RegisterComponent.vue";
import UserDashboard from "./components/Dashboard/UserDashboard.vue";

const routes = [
    { path: '/', component: HomePage },
    { path: '/HomePage', component: HomePage },
    { path: '/Login', component: LoginComponent },
    { path: '/Register', component: RegisterComponent },
    { path: '/UserDashboard', component: UserDashboard }
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;
