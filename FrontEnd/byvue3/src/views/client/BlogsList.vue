<script setup>
import FooterSection from '@/components/FooterSection.vue';
import HeaderSection from '@/components/HeaderSection.vue';
import BlogItem from '@/components/minis/BlogItem.vue';
import SearchModel from '@/components/SearchModel.vue';
import { injectMainJS, removeMainJS } from '@/utils/asynchronous';
import { computed, onMounted, onUnmounted } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute();
onMounted(() => {
    injectMainJS();
});
onUnmounted(() => {
    removeMainJS();
});

var numpages = Array.from({ length: 5 }, (_, i) => i + 1);
const currentPage = computed(() => parseInt(route.query.page || 3))

const arr = Array.from({ length: 9 }, (_, i) => i + 1);
var blogs = arr.map((item) => `img/portfolio/${item}.jpg`);


</script>

<template>
    <!-- Page Preloder -->
    <HeaderSection />

    <!-- Blog section  -->
    <section class="blog-section">
        <div class="blog-warp">
            <div class="row">
                <div class="col-lg-4 col-sm-6" v-for="(blog, index) in blogs" :key="blog">
                    <BlogItem 
                    :blogId="index"
                    :blog_metadata="blog" 
                    blog_date="Feb 11, 2019"
                    blog_title="Photography cousrses 101"
                    :blog_categories="['Lifestyle','Photography','Travel']"
                    />
                </div>
            </div>
            <div class="site-pagination">
                <router-link 
                :class="{
                    'current': currentPage === numpage
                }" 
                :to="`/blogs?page=${numpage}`" 
                v-for="numpage in numpages" 
                :key="numpage">
                    {{ `${numpage}.` }}
                </router-link>
            </div>
        </div>
    </section>
    <!-- Blog section end  -->

    <FooterSection />

    <SearchModel />
</template>

<style scoped></style>