import Vue from 'vue';
import App from './App.vue';
import Router from './router';

new Vue({
    render: h => h(App),
    router: Router,
    components: { App }
}).$mount('#app')