<template>
    <div>
        <!--Hello from App Vue: <strong>{{</strong> message <strong>}}</strong>
        <router-link to="/Home">Go to Home</router-link>
        <router-link to="/Login">Go to Login</router-link>-->
        <router-view></router-view>
    </div>
</template>
<script>
    export default {
        data() {
            return {
                message: 'Message from App.vue',
            };
        }
    };
</script>