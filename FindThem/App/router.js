import Vue from 'vue';
import VueRouter from 'vue-router';
import Home from './Pages/Home.vue';
import Login from './Pages/Login.vue';
const routes = [
    { path: '/', component: Home },
    { path: '/home', component: Home },
    { path: '/login', component: Login },
]
Vue.use(VueRouter);
const router = new VueRouter({ mode: 'history', routes: routes });
export default router;