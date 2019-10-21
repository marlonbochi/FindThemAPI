
<template>
    <div id="Home">
        <header-menu ref="headerMenu" />

        <div class="container">
            <h4 class="text-center margin-top">Bem vindo ao gerenciador do <strong>Find Them</strong></h4>
        </div>
    </div>
</template>
<script>
    import Services from "../middleware/mainServices";
    import HeaderMenu from "../components/HeaderMenu.vue";

    export default {
        name: "Home",
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