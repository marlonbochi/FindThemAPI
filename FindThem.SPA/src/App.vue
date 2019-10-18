<template>
    <div>
        <router-view></router-view>
    </div>
</template>
<script>
    import Services from "./Services/mainServices";

    export default {
        data() {
            return {
                services: new Services()
            };
        },
        mounted: function () {

            if (window.location.href.indexOf("localhost") > 0) {
                sessionStorage.setItem("urlAPI", "https://localhost:44357");
            } else {
                sessionStorage.setItem("urlAPI", "");
            }
        }
    };
</script>