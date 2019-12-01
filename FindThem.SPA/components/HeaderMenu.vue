
<template>
    <div id="HeaderMenu" class="container">
        <header class="blog-header py-3">
            <div class="row flex-nowrap justify-content-between align-items-center">
                <div class="col-4 pt-1">
                </div>
                <div class="col-4 text-center">
                    <nuxt-link to="/" class="blog-header-logo text-dark">Find Them</nuxt-link>
                </div>
                <div class="col-4 d-flex justify-content-end align-items-center">
                    <a class="btn btn-sm btn-outline-secondary" href="#" @click="logout">Logout</a>
                </div>
            </div>
        </header>
        <div class="nav-scroller py-1 mb-2">
            <nav class="nav d-flex justify-content-between">
                <nuxt-link to="/" class="p-2 text-muted" v-bind:class="{'selected': route == '' }">Home</nuxt-link>
                <nuxt-link to="/client" class="p-2 text-muted" v-bind:class="{'selected': route == 'client' }">Clientes</nuxt-link>
                <nuxt-link to="/provider" class="p-2 text-muted" v-bind:class="{'selected': route == 'provider' }">Prestador de serviços</nuxt-link>
                <nuxt-link to="/service" class="p-2 text-muted" v-bind:class="{'selected': route == 'service' }">Serviços</nuxt-link>
            </nav>
        </div>
    </div>
</template>
<script>
    import Services from "../middleware/mainServices";

    export default {
        name: "HeaderMenu",
        data() {
            return {
                services: new Services(),
                route: ""
            }
        },
        mounted: function () {

            if (window.location.href.indexOf("localhost") > 0) {
                sessionStorage.setItem("urlAPI", "https://localhost:5001");
            } else {
                sessionStorage.setItem("urlAPI", "https://findthem20190819101129.azurewebsites.net");
            }
            
            this.services.validateToken();
            this.defineRouteName(this.$router.currentRoute.path);
        },
        watch: {
            '$route'(route) {
                this.defineRouteName(route.currentRoute.path);
            }
        },
        methods: {
            logout: function () {
                this.services.eraseCookie("tokenID");
                this.services.eraseCookie("tokenExpiration");
                this.services.eraseCookie("kindUser");

                sessionStorage.clear();

                this.$router.push("/login");
            },

            defineRouteName: function ($routePath) {
                this.route = $routePath.replace("/", "");
            }
        }
    };

</script>