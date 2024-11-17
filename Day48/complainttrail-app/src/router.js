// src/router.js
import { createRouter, createWebHistory } from "vue-router";
import HelloWorld from "@/components/HelloWorld.vue";
import LoginComponent from "@/components/Login/LoginComponent.vue";
import RegisterComponent from "@/components/Login/RegisterComponent.vue";
import GetAllComplaintComponent from "./components/Complaint/GetAllComplaintComponent.vue";

const routes = [
    { path: '/', component: HelloWorld },
    { path: '/login', component: LoginComponent },
    {path:'/register' , component:RegisterComponent},
    {path:'/complaint' , component:GetAllComplaintComponent}
    
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;
