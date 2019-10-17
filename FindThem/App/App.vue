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
                message: 'Message from App.vue',
                services: new Services()
            };
        },
        mounted: function () {
            
        }
    };
</script>