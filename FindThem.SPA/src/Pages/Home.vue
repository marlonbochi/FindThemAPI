
<template>
    <div id="Home">
        <header-menu ref="headerMenu" />
    </div>
</template>
<script>
    import Services from "../Services/mainServices";
    import HeaderMenu from "./HeaderMenu.vue";

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