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
    <div class="user_profile_view">
        <router-view></router-view>
    </div>
</template>

<style scoped>
.user_profile_view {
    margin-left: 276px;
    width: 100%;
    height: auto;
}
</style>