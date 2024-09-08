<script setup>
import { defineProps, onMounted } from 'vue';
import PhotoBlog from '../refs/PhotoBlog.vue';
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