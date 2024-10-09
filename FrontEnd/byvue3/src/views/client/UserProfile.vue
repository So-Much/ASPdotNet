<script setup>
import HeaderSection from '@/components/HeaderSection.vue';
import UserSidebar from '@/components/UserSidebar.vue';
import { injectMainJS, removeMainJS } from '@/utils/asynchronous';
import { onMounted, onUnmounted } from 'vue';
// import { useRoute } from 'vue-router';
onMounted(() => {
    injectMainJS();
});
onUnmounted(() => {
    removeMainJS();
});


// const route = useRoute();
// const userID = route.params.userId;

</script>

<template>
    <HeaderSection />
    <UserSidebar />
    <!-- <div class="view">
        <h1>Hi Bro</h1>
    </div> -->
     <router-view></router-view>
</template>

<style scoped>
.view {
    margin-left: 256px;
}
</style>