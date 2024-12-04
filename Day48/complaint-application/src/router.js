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
import ForgotPassword from "./components/Login/ForgotPassword.vue";
import AdminProfile from "./components/Admin/AdminProfile.vue";
import AdminViewUser from "./components/Admin/AdminViewUser.vue";
import AdminViewOrg from "./components/Admin/AdminViewOrg.vue";
import AdminViewComplaints from "./components/Admin/AdminViewComplaints.vue";
import AdminFileComplaint from "./components/Admin/AdminFileComplaint.vue";
import ComplaintCategory from "./components/Admin/ComplaintCategory.vue";
import AdminKnowledgeBase from "./components/Admin/AdminKnowledgeBase.vue";
import HelpPage from "./components/HelpPage.vue";
import UnauthorizedPage from "./components/UnauthorizedPage.vue";
import UserProfile from "./components/User/UserProfile.vue";
import UserCategoryView from "./components/User/UserCategoryView.vue";
import UserViewOrg from "./components/User/UserViewOrg.vue";
import UserFileComplaint from "./components/User/UserFileComplaint.vue";
import UserKnowledgeBase from "./components/User/UserKnowledgeBase.vue";
import OrgCategoryView from "./components/Organization/OrgCategoryView.vue";
import OrgProfile from "./components/Organization/OrgProfile.vue";
import OrgComplaintView from "./components/Organization/OrgComplaintView.vue";
import OrgKnowledgeBase from "./components/Organization/OrgKnowledgeBase.vue";

const routes = [
    { path: '/', component: HomePage },
    { path: '/HomePage', component: HomePage },
    { path: '/Login', component: LoginComponent },
    { path: '/Register', component: RegisterComponent },
    { path: '/ErrorPage', component: ErrorPage },
    { path: '/HelpPage', component: HelpPage },
    { path: '/ForgotPassword', component: ForgotPassword },
    { path: '/UnauthorizedPage', component: UnauthorizedPage },

    {
        path: '/OrgKnowledgeBase',
        component: OrgKnowledgeBase,
        meta: { requiresAuth: true, requiresOrganization: true }
    },
    {
        path: '/OrgComplaintView',
        component: OrgComplaintView,
        meta: { requiresAuth: true, requiresOrganization: true }
    },
    {
        path: '/OrgProfile',
        component: OrgProfile,
        meta: { requiresAuth: true, requiresOrganization: true }
    },
    {
        path: '/OrgCategoryView',
        component: OrgCategoryView,
        meta: { requiresAuth: true, requiresOrganization: true }
    },

    {
        path: '/UserKnowledgeBase',
        component: UserKnowledgeBase,
        meta: { requiresAuth: true, requiresUser: true }
    },
    {
        path: '/UserFileComplaint',
        component: UserFileComplaint,
        meta: { requiresAuth: true, requiresUser: true }
    },
    {
        path: '/UserViewOrg',
        component: UserViewOrg,
        meta: { requiresAuth: true, requiresUser: true }
    },
    {
        path: '/UserCategoryView',
        component: UserCategoryView,
        meta: { requiresAuth: true, requiresUser: true }
    },
    {
        path: '/UserDashboard',
        component: UserDashboard,
        meta: { requiresAuth: true, requiresUser: true }
    },
    {
        path: '/UserProfile',
        component: UserProfile,
        meta: { requiresAuth: true, requiresUser: true }
    },
    {
        path: '/AdminDashboard',
        component: AdminDashboard,
        meta: { requiresAuth: true, requiresAdmin: true }
    },
    {
        path: '/AdminProfile',
        component: AdminProfile,
        meta: { requiresAuth: true, requiresAdmin: true }
    },
    {
        path: '/OrganizationDashboard',
        component: OrganizationDashboard,
        meta: { requiresAuth: true, requiresOrganization: true }
    },
    {
        path: '/AdminSideBar',
        component: AdminSideBar,
        meta: { requiresAuth: true, requiresAdmin: true }
    },
    {
        path: '/UserSideBar',
        component: UserSideBar,
        meta: { requiresAuth: true }
    },
    {
        path: '/OrgSideBar',
        component: OrgSideBar,
        meta: { requiresAuth: true, requiresOrganization: true }
    },

    {
        path: '/AdminViewUser',
        component: AdminViewUser,
        meta: { requiresAuth: true, requiresAdmin: true }
    },
    {
        path: '/AdminViewOrg',
        component: AdminViewOrg,
        meta: { requiresAuth: true, requiresAdmin: true }
    },
    {
        path: '/AdminViewComplaints',
        component: AdminViewComplaints,
        meta: { requiresAuth: true, requiresAdmin: true }
    },
    {
        path: '/AdminFileComplaint',
        component: AdminFileComplaint,
        meta: { requiresAuth: true, requiresAdmin: true }
    },
    {
        path: '/AdminKnowledgeBase',
        component: AdminKnowledgeBase,
        meta: { requiresAuth: true, requiresAdmin: true }
    },
    {
        path: '/ComplaintCategory',
        component: ComplaintCategory,
        meta: { requiresAuth: true, requiresAdmin: true }
    },
    { path: '/:pathMatch(.*)*', redirect: '/ErrorPage' }



];



const router = createRouter({
    history: createWebHistory(),
    routes
});
router.afterEach(() => {
    window.scrollTo({
        top: 0,
        left: 0,
        behavior: "smooth",
    });
});
router.beforeEach((to, from, next) => {
    if (to.matched.some((record) => record.meta.requiresAuth)) {
        if (!isAuthenticated()) {
            next({ path: "/UnauthorizedPage", query: { redirect: to.path } });
        } else {
            next();
        }
    } else {
        next();
    }
});

function isAuthenticated() {
    // Check user authentication (e.g., token stored in localStorage)
    return !!sessionStorage.getItem('token');
}
router.beforeEach((to, from, next) => {
    if (to.matched.some((record) => record.meta.requiresAdmin)) {
        if (!isAdminAuthenticated()) {
            next({ path: "/UnauthorizedPage", query: { redirect: to.fullPath } });
        } else {
            next();
        }
    } else {
        next();
    }
});
function isAdminAuthenticated() {
    const userRole = localStorage.getItem('role');
    console.log(userRole);
    if (userRole == "Admin") {
        return true;
    }
}
router.beforeEach((to, from, next) => {
    if (to.matched.some((record) => record.meta.requiresOrganization)) {
        if (!isOrganizationAuthenticated()) {
            next({ path: "/UnauthorizedPage", query: { redirect: to.fullPath } });
        } else {
            next();
        }
    } else {
        next();
    }
});
function isOrganizationAuthenticated() {
    const userRole = localStorage.getItem('role');
    console.log(userRole);
    if (userRole == "Organization") {
        return true;
    }
}
router.beforeEach((to, from, next) => {
    if (to.matched.some((record) => record.meta.requiresUser)) {
        if (!isUserAuthenticated()) {
            next({ path: "/UnauthorizedPage", query: { redirect: to.fullPath } });
        } else {
            next();
        }
    } else {
        next();
    }
});
function isUserAuthenticated() {
    const userRole = localStorage.getItem('role');
    console.log(userRole);
    if (userRole == "User") {
        return true;
    }
}

export default router;
