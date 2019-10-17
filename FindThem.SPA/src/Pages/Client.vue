
<template>
    <div id="client">
        <header-menu ref="headerMenu" />
        <div class="container">
            Clientes - <strong>{{message}}</strong>
        </div>
    </div>
</template>
<script>
    import Services from "../Services/mainServices";
    import HeaderMenu from "./HeaderMenu.vue";

    export default {
        name: 'client',
        components: {
            "headerMenu": HeaderMenu
        },
        data() {
            return {
                message: 'Data from Home',
                services: new Services()
            };
        }
    };
</script>