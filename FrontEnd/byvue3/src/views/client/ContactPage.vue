<script setup>
import ContactSection from '@/components/ContactSection.vue';
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
    <ContactSection />
</template>

<style scoped>

</style>