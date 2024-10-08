<script setup>
import BannerBlog from '@/components/BannerBlog.vue';
import BlogPosts from '@/components/BlogPosts.vue';
import BlogTitle from '@/components/BlogTitle.vue';
import FooterSection from '@/components/FooterSection.vue';
import HeaderSection from '@/components/HeaderSection.vue';
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
    <HeaderSection />
    <BannerBlog 
    blog_name="Stand Blog" 
    blog_title="A free blog template by TemplateMo website" 
    />
    <BlogTitle 
    author_name="Glilbert Bellamy Salander"
    author_role="User"
    blog_description="Sharing a best trip to the world"
    />
    
    <BlogPosts />
    <FooterSection />
</template>

<style scoped></style>