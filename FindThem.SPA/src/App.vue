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

        }
    };
</script>