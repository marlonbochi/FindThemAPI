import Vue from 'vue';
import VueRouter from 'vue-router';
import Home from './Pages/Home.vue';
import Login from './Pages/Login.vue';
import Client from './Pages/Client.vue';
import Provider from './Pages/Provider.vue';
const routes = [
    { path: '/', component: Home },
    { path: '/home', component: Home },
    { path: '/login', component: Login },
    { path: '/client', component: Client },
    { path: '/provider', component: Provider },
]
Vue.use(VueRouter);
const router = new VueRouter({ mode: 'history', routes: routes });
export default router;