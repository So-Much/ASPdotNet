<script setup>
import PostForm from '@/components/PostForm.vue';

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
    <PostForm />
</template>

<style scoped>

</style>