<script setup>
import BannerBlog from '@/components/BannerBlog.vue';
import BlogPosts from '@/components/BlogPosts.vue';
import BlogTitle from '@/components/BlogTitle.vue';
import FooterSection from '@/components/FooterSection.vue';
import { injectMainJS, removeMainJS } from '@/utils/asynchronous';
import { onMounted, onUnmounted } from 'vue';
// import { computed } from 'vue';
// import { useRoute } from 'vue-router';

onMounted(() => {
    injectMainJS();
});
onUnmounted(() => {
    removeMainJS();
});

// const route = useRoute();
// const currentBlog = computed(() => parseInt(route.params.blogId))
</script>

<template>
    <BannerBlog 
    blog_title="Stand Blog" 
    blog_description="A free blog template by TemplateMo website" 
    />
    <BlogTitle 
    author_name="Glilbert Bellamy Salander"
    author_role="User"
    author_intro="I'm newbie in photography, but I'm trying to learn more and more"
    />
    <BlogPosts />
    <FooterSection />
</template>

<style scoped></style>