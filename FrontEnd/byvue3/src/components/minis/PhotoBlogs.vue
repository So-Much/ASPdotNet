<script setup>
import { defineProps, onMounted, onUnmounted } from 'vue';
import PhotoBlog from './PhotoBlog.vue';
import { injectMainJS, removeMainJS } from '@/utils/asynchronous';
onMounted(() => {
    injectMainJS();
});
onUnmounted(() => {
    removeMainJS();
});
const props = defineProps({
    // Normal property declarations
    list_blog: Array,
    // Styles property declarations
    styles: Object
});
onMounted(() => {
    for (const selector in props.styles) {
        document.documentElement.style.setProperty(selector, props.styles[selector]);
    }
});
</script>

<template>
    <div class="hero-slider owl-carousel">
        <PhotoBlog v-for="blog in list_blog" :key="blog.id" :data_setbg="blog.data_setbg" :link_to="blog.link_to"
            :styles="blog.styles" />
    </div>
</template>

<style scoped></style>