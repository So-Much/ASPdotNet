<script setup>
// import BannerBlog from '@/components/BannerBlog.vue';
import BlogPosts from '@/components/BlogPosts.vue';
// import BlogTitle from '@/components/BlogTitle.vue';
import FooterSection from '@/components/FooterSection.vue';
import HeaderSection from '@/components/HeaderSection.vue';
import { axios } from '@/configs';
import { injectMainJS, removeMainJS } from '@/utils/asynchronous';
import { onBeforeMount, onMounted, onUnmounted, ref } from 'vue';
import { useLoading } from 'vue-loading-overlay';
import {
    useRoute,
    // useRouter
} from 'vue-router';
// import { useToast } from 'vue-toastification';
// import { computed } from 'vue';
// import { useRoute } from 'vue-router';

onMounted(() => {
    injectMainJS();
});
onUnmounted(() => {
    removeMainJS();
});

// const router = useRouter();
const route = useRoute();
const $loading = useLoading();
// const toast = useToast();
const blogId = route.params.blogId;
const blog = ref();


onBeforeMount(() => {
    const loader = $loading.show();
    axios.get(`/api/blog/${blogId}`)
        .then((res) => {
            blog.value = res.data;
            console.log(res)
            loader.hide();
        })
        .catch((err) => {
            loader.hide();
            console.log(err);
        })
})

// const route = useRoute();
// const currentBlog = computed(() => parseInt(route.params.blogId))
</script>

<template>
    <HeaderSection />
    <!-- <BannerBlog :blog_name="blog.title" :blog_title="blog.description" /> -->
    <!-- <BlogTitle author_name="Glilbert Bellamy Salander" author_role="User"
        blog_description="Sharing a best trip to the world" /> -->

    <BlogPosts />
    <FooterSection />
</template>

<style scoped></style>