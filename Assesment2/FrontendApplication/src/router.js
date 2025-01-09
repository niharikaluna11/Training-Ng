import { createRouter, createWebHistory } from 'vue-router';
import MainPage from './components/MainPage.vue';
import SearchFlightPage from './components/SearchFlightPage.vue';
import NotFoundPage from './components/NotFoundPage.vue';

const routes = [
    {
        path: '/',
        name: 'MainPage',
        component: MainPage,
        meta: { requiresAuth: true, }
    },
    {
        path: '/search-flight',
        name: 'SearchFlight',
        component: SearchFlightPage,
        meta: { requiresAuth: true, }
    },
    {
        path: '/not-found',
        name: 'NotFoundPage',
        component: NotFoundPage,

    },

    { path: '/:pathMatch(.*)*', redirect: '/not-found' }


];

const router = createRouter({
    history: createWebHistory(),
    routes
});


export default router;
