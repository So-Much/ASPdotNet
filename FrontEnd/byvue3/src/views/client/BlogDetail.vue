<script setup>
import { usePhotographerTemplate } from '@/utils/ImportTemplate';
import { computed } from 'vue';
import { useRoute } from 'vue-router';

usePhotographerTemplate();

const route = useRoute();
const currentBlog = computed(() => parseInt(route.params.blogId))
</script>

<template>
    <div>Hello welcome to blog detail</div>
    <h1>This is Blog {{ currentBlog }}</h1>
</template>

<style scoped>

</style>