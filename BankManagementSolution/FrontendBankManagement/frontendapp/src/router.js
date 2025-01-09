import HomePage from '@/components/HomePage.vue';
import { createRouter, createWebHistory } from 'vue-router';
import UnauthorizedPage from './components/UnauthorizedPage.vue';
import AddCustomer from './components/AddCustomer.vue';
import UpdateCustomer from './components/UpdateCustomer.vue';
import NotFound from './components/NotFound.vue';


const routes = [
    {
        path: '/',
        name: 'HomePage',
        component: HomePage,
        meta: { requiresAuth: true, }
    },
    {
        path: '/AddCustomerPage',
        name: 'AddCustomerPage',
        component: AddCustomer,
        meta: { requiresAuth: true, requiresManager: true, }
    },
    {
        path: '/UpdateCustomerPage',
        name: 'UpdateCustomer',
        component: UpdateCustomer,
        meta: { requiresAuth: true, requiresManager: true, }
    },

    {
        path: '/UnauthorizedPage',
        name: 'UnauthorizedPage',
        component: UnauthorizedPage
    },

    {
        path: '/NotFound',
        name: 'NotFound',
        component: NotFound
    },
    { path: '/:pathMatch(.*)*', redirect: '/NotFound' }


];

const router = createRouter({
    history: createWebHistory(),
    routes
});


router.beforeEach((to, from, next) => {
    if (to.matched.some(record => record.meta.requiresAuth)) {
        const token = sessionStorage.getItem('token');


        if (!token || !isValidToken(token)) {
            next({ path: '/UnauthorizedPage' });
        } else {

            const payload = JSON.parse(atob(token.split(".")[1]));
            const role = payload["Roles"];
            sessionStorage.setItem("role", role);


            console.log(role);


            if (to.matched.some(record => record.meta.requiresManager)) {
                if (role !== 'ZoneManager' && role !== 'BranchManager') {
                    return next("/UnauthorizedPage");
                }
            }


            next();
        }
    } else {

        next();
    }
});

function isValidToken(token) {

    return token !== "invalid";
}

export default router;
