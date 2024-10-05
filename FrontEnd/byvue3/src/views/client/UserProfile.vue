<script setup>
import HeaderSection from '@/components/HeaderSection.vue';
import { injectMainJS, removeMainJS } from '@/utils/asynchronous';
import { onMounted, onUnmounted } from 'vue';
import { useRoute } from 'vue-router';
onMounted(() => {
    injectMainJS();
});
onUnmounted(() => {
    removeMainJS();
});


const route = useRoute();
const userID = route.params.userId;

</script>

<template>
    <HeaderSection />
    <h1>User Profile {{ userID }}</h1>
</template>

<style scoped></style>