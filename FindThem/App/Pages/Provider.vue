
<template>
    <div id="provider">
        <header-menu ref="headerMenu" />
        <div class="container">
            Prestador de serviço - <strong>{{message}}</strong>
        </div>
    </div>
</template>
<script>
    import Services from "../Services/mainServices";
    import HeaderMenu from "./HeaderMenu.vue";

    export default {
        name: 'provider',
        components: {
            "headerMenu": HeaderMenu
        },
        data() {
            return {
                message: 'Data from Home',
                services: new Services()
            };
        },
        mounted: function () {
        }
    };
</script>