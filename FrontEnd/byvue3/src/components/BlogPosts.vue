<script setup>
import { onMounted, onUnmounted, computed, ref, onBeforeMount, watch } from 'vue';
import { injectMainJS, removeMainJS } from '@/utils/asynchronous';
import PostItem from './minis/PostItem.vue';
import PaginationComponent from './PaginationComponent.vue';
import { useRoute, useRouter } from 'vue-router';
import { axios } from '@/configs';
import { useToast } from 'vue-toastification';
import { useLoading } from 'vue-loading-overlay';

onMounted(() => {
    injectMainJS();
});
onUnmounted(() => {
    removeMainJS();
});

const formatDate = (dateString) => {
    const date = new Date(dateString);
    const pad = (num) => num.toString().padStart(2, '0');
    return `${pad(date.getDate())}/${pad(date.getMonth() + 1)}/${date.getFullYear()}`;
};


const currentPage = computed(() => parseInt(route.query.page || 1))
const pageSize = ref(9);
const totalPages = ref(0);
// const numpages = computed(() => Array.from({ length: totalPages.value }, (_, i) => i + 1));

const route = useRoute();
const blogId = route.params.blogId;
const router = useRouter();
const toast = useToast();
const $loading = useLoading();

const posts = ref();
const popularPosts = ref();
const hashtags = ref();

console.log("🚀 ~ toast:", toast)
console.log("🚀 ~ router:", router)

onBeforeMount(()=> {
    const loader = $loading.show();
    axios.get(`/api/blog/${blogId}/getpublishposts?page=${currentPage.value}&limit=${pageSize.value}`)
    .then(res => {
        console.log(res);
        posts.value = res.data;
        const pagination = JSON.parse(res.headers['x-pagination']);
        totalPages.value = pagination.TotalPages;
        loader.hide();
    })
    .catch(err => {
        loader.hide();
        console.log(err);
    })

    const loaderPopularPost = $loading.show();
    axios.get(`/api/blog/${blogId}/getpopularposts`)
    .then(res => {
        console.log(res);
        popularPosts.value = res.data;
        loaderPopularPost.hide();
    })
    .catch(err => {
        popularPosts.value = [];
        loaderPopularPost.hide();
        console.log(err);
    })
    const topHashTagLoader = $loading.show();
    axios.get(`/api/blog/${blogId}/gettophasgtag`)
    .then(res => {
        console.log(res);
        hashtags.value = res.data;
        topHashTagLoader.hide();
    })
    .catch(err => {
        hashtags.value = [];
        topHashTagLoader.hide();
        console.log(err);
    })
})

const hashtagFIlter = ref();

watch(
    hashtagFIlter,
    (newVal, oldVal) => {
        console.log("🚀 ~ oldVal:", oldVal)
        const loader = $loading.show();
        axios.get(`/api/blog/${blogId}/getpostbyhashtag?hashtag=${newVal}`)
        .then(res => {
            console.log(res);
            posts.value = res.data;
            loader.hide();
        })
        .catch(err => {
            loader.hide();
            console.log(err);
        })
    }
)

</script>

<template>
    <section class="blog-posts grid-system">
        <div class="container">
            <div class="row">
                <div class="col-lg-8">
                    <div class="all-blog-posts">
                        <div class="row">
                            <div v-for="post in posts" :key="post.id" class="col-lg-6">
                                <PostItem :post="post" :blogId="blogId" />
                            </div>
                            <div class="col-lg-12 pagination">
                                <PaginationComponent
                                :totalPages="totalPages"
                                :currentPage="currentPage" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="sidebar">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="sidebar-item search">
                                    <form id="search_form" name="gs" method="GET" action="#">
                                        <input type="text" name="q" class="searchText" placeholder="type to search..."
                                            autocomplete="on">
                                    </form>
                                </div>
                            </div>
                            <div class="col-lg-12">
                                <div class="sidebar-item recent-posts">
                                    <div class="sidebar-heading">
                                        <h2>Popular Post</h2>
                                    </div>
                                    <div class="content">
                                        <ul>
                                            <li v-for="post in popularPosts" :key="post.id">
                                                <router-link :to="`/blogs/${blogId}/post/${post.id}`">
                                                    <h5>{{ post.title }}</h5>
                                                    <span>{{ formatDate(post.createAt) }}</span>
                                                </router-link>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <!-- <div class="col-lg-12">
                                <div class="sidebar-item categories">
                                    <div class="sidebar-heading">
                                        <h2>Categories</h2>
                                    </div>
                                    <div class="content">
                                        <ul>
                                            <li><a href="#">- Nature Lifestyle</a></li>
                                            <li><a href="#">- Awesome Layouts</a></li>
                                            <li><a href="#">- Creative Ideas</a></li>
                                            <li><a href="#">- Responsive Templates</a></li>
                                            <li><a href="#">- HTML5 / CSS3 Templates</a></li>
                                            <li><a href="#">- Creative &amp; Unique</a></li>
                                        </ul>
                                    </div>
                                </div>
                            </div> -->
                            <div class="col-lg-12">
                                <div class="sidebar-item tags">
                                    <div class="sidebar-heading">
                                        <h2>Tag Clouds</h2>
                                    </div>
                                    <div class="content">
                                        <ul>
                                            <li v-for="(hashtag, index) in hashtags" :key="index">
                                                <a href="#" @click.prevent="() => {
                                                    hashtagFIlter.value = hashtag.hashtag;
                                                }" >{{ hashtag.hashtag }}</a>
                                            </li>
                                            
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</template>

<style scoped>
.pagination {
    margin-left: auto;
    margin-right: auto;
}
</style>