<script setup>
import { defineProps, onMounted } from 'vue';
const props = defineProps({
    // Normal property declarations
    blogId: String,
    blog_metadata: String,
    blog_date: String,
    blog_title: String,
    blog_categories: Array,
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
    <div class="blog-post">
        <img :src="blog_metadata" alt="">
        <div class="blog-date">{{ blog_date }}</div>
        <h3>{{ blog_title }}</h3>
        <div class="blog-cata">
            <span v-for="blog_category in blog_categories" :key="blog_category">
                {{ blog_category }}
            </span>
        </div>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incidi-dunt ut
            labore et dolore magna aliqua. Quis ipsum suspendisse ultrices gravida. Risus commodo
            viverra maecenas.</p>
        <router-link :to="`/blogs/${blogId}`" class="site-btn">Blog-Detail</router-link>
    </div>
</template>

<style scoped></style>