// src/router.js
import { createRouter, createWebHistory } from "vue-router";
import HomePage from "@/components/HomePage.vue";
import LoginComponent from "@/components/Login/LoginComponent.vue";
import RegisterComponent from "@/components/Login/RegisterComponent.vue";
import GetAllComplaintComponent from "./components/Complaint/GetAllComplaintComponent.vue";

const routes = [
    { path: '/', component: HomePage },
    { path: '/HomePage', component: HomePage },
    { path: '/Login', component: LoginComponent },
    {path:'/Register' , component:RegisterComponent},
    {path:'/Complaint' , component:GetAllComplaintComponent}
    
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;
