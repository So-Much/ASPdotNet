<script setup>
import { defineProps, onMounted } from 'vue';
const props = defineProps({
    // Normal property declarations
    blogId: String,
    blog_metadata: String,
    blog_date: String,
    blog_title: String,
    blog_categories: Array,
    blog_description: String,
    // Styles property declarations
    styles: Object
});
const formatDate = (dateString) => {
    const date = new Date(dateString);
    const pad = (num) => num.toString().padStart(2, '0');
    return `${pad(date.getHours())}:${pad(date.getMinutes())}:${pad(date.getSeconds())} ${pad(date.getDate())}/${pad(date.getMonth() + 1)}/${date.getFullYear()}`;
};
onMounted(() => {
    for (const selector in props.styles) {
        document.documentElement.style.setProperty(selector, props.styles[selector]);
    }
    console.log('blog_metadata: ', props.blog_metadata);
});
</script>

<template>
    <div class="blog-post">
        <img :src="props.blog_metadata" alt="Blog image">
        <div class="blog-date">{{ formatDate(props.blog_date) }}</div>
        <h3>{{ props.blog_title }}</h3>
        <div class="blog-cata">
            <span v-for="blog_category in props.blog_categories" :key="blog_category">
                {{ blog_category.hashtag }}
            </span>
        </div>
        <p>{{  props.blog_description  }}</p>
        <router-link :to="`/blogs/${props.blogId}`" class="site-btn">Blog-Detail</router-link>
    </div>
</template>

<style scoped></style>