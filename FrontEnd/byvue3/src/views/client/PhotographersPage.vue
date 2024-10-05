<script setup>
import HeaderSection from '@/components/HeaderSection.vue';
import { injectMainJS, removeMainJS } from '@/utils/asynchronous';
import { onMounted, onUnmounted } from 'vue';

onMounted(() => {
    injectMainJS();
});
onUnmounted(() => {
    removeMainJS();
});
</script>

<template>
    <HeaderSection />
    <h1>Photographers is coming soon!</h1>
</template>

<style scoped>

</style>