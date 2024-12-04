<script setup>
import FooterSection from '@/components/FooterSection.vue';
import HeaderSection from '@/components/HeaderSection.vue';
import BlogItem from '@/components/minis/BlogItem.vue';
import SearchModel from '@/components/SearchModel.vue';
import { axios } from '@/configs';
import { injectMainJS, removeMainJS } from '@/utils/asynchronous';
import { computed, onBeforeMount, onMounted, onUnmounted, ref } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute();
onMounted(() => {
    injectMainJS();
});
onUnmounted(() => {
    removeMainJS();
});


const currentPage = computed(() => parseInt(route.query.page || 1))
const pageSize = ref(10);
const totalPages = ref(0);
const numpages = computed(() => Array.from({ length: totalPages.value }, (_, i) => i + 1));

const blogs = ref([]);


onBeforeMount(() => {
    axios.get(`/api/blog/getpublishedblogs?page=${currentPage.value}&limit=${pageSize.value}`)
    .then(res => {
        console.log("🚀 ~ onBeforeMount ~ res:", res)
        blogs.value = res.data;
        const pagination = JSON.parse(res.headers['x-pagination']);
        totalPages.value = pagination.TotalPages;
        console.log("🚀 ~ onBeforeMount ~ blogs:", blogs)
        console.log("🚀 ~ onBeforeMount ~ pagination:", pagination)
        console.log("🚀 ~ onBeforeMount ~ totalPages:", totalPages)
    })
    .catch(err => {
        console.log(err);
    })
})

</script>

<template>
    <!-- Page Preloder -->
    <HeaderSection />
    <div class="search_container">
        <div class="group">
            <svg class="icon" aria-hidden="true" viewBox="0 0 24 24">
                <g>
                    <path
                        d="M21.53 20.47l-3.66-3.66C19.195 15.24 20 13.214 20 11c0-4.97-4.03-9-9-9s-9 4.03-9 9 4.03 9 9 9c2.215 0 4.24-.804 5.808-2.13l3.66 3.66c.147.146.34.22.53.22s.385-.073.53-.22c.295-.293.295-.767.002-1.06zM3.5 11c0-4.135 3.365-7.5 7.5-7.5s7.5 3.365 7.5 7.5-3.365 7.5-7.5 7.5-7.5-3.365-7.5-7.5z">
                    </path>
                </g>
            </svg>
            <input placeholder="Search" type="search" class="input">
        </div>
        <div class="categories_list">
            <div class="checkbox-wrapper-10">
                <input checked="" type="checkbox" id="cb5" class="tgl tgl-flip">
                <label for="cb5" data-tg-on="Yeah!" data-tg-off="Nope" class="tgl-btn"></label>
            </div>
            <div class="checkbox-wrapper-10">
                <input checked="" type="checkbox" id="cb6" class="tgl tgl-flip">
                <label for="cb6" data-tg-on="Yeah!" data-tg-off="Nope" class="tgl-btn"></label>
                <!-- <input checked="" type="checkbox" id="cb8" class="tgl tgl-flip">
                <label for="cb8" data-tg-on="Yeah!" data-tg-off="Nope" class="tgl-btn"></label> -->
            </div>
            <div class="checkbox-wrapper-10">
                <input checked="" type="checkbox" id="cb7" class="tgl tgl-flip">
                <label for="cb7" data-tg-on="Yeah!" data-tg-off="Nope" class="tgl-btn"></label>
            </div>
        </div>

    </div>

    <!-- Blog section  -->
    <section class="blog-section">
        <div class="blog-warp">
            <div class="row">
                <div class="col-lg-4 col-sm-6" v-for="(blog) in blogs" :key="blog.id">
                    <BlogItem :blogId="blog.blogId" :blog_metadata="blog.image" :blog_date="blog.createAt"
                        :blog_title="blog.title"
                        :blog_description="blog.description"
                        :blog_categories="blog.topHashtags" />
                </div>
            </div>
            <div class="site-pagination">
                <router-link :class="{
                    'current': currentPage === numpage
                }" :to="`/blogs?page=${numpage}`" v-for="numpage in numpages" :key="numpage">
                    {{ `${numpage}.` }}
                </router-link>
            </div>
        </div>
    </section>
    <!-- Blog section end  -->

    <FooterSection />

    <SearchModel />
</template>

<style scoped>
.search_container {
    margin-top: 20px;
    max-width: 80%;
    margin-left: auto;
    margin-right: auto;
}

.categories_list {
    width: 80%;
    margin-left: auto;
    margin-right: auto;
    display: flex;
    gap: 20px;
    margin-top: 20px;
}

/* From Uiverse.io by alexruix */
.group {
    width: 80%;
    display: flex;
    line-height: 28px;
    align-items: center;
    position: relative;
    margin-left: auto;
    margin-right: auto;
}

.input {
    width: 100%;
    height: 40px;
    line-height: 28px;
    padding: 0 1rem;
    padding-left: 2.5rem;
    border: 2px solid transparent;
    border-radius: 8px;
    outline: none;
    background-color: #f3f3f4;
    color: #0d0c22;
    transition: .3s ease;
}

.input::placeholder {
    color: #9e9ea7;
}

.input:focus,
input:hover {
    outline: none;
    border-color: rgba(234, 76, 137, 0.4);
    background-color: #fff;
    box-shadow: 0 0 0 4px rgb(234 76 137 / 10%);
}

.icon {
    position: absolute;
    left: 1rem;
    fill: #9e9ea7;
    width: 1rem;
    height: 1rem;
}

/* From Uiverse.io by Dev-MdTuhin */
.checkbox-wrapper-10 .tgl {
    display: none;
}

.checkbox-wrapper-10 .tgl,
.checkbox-wrapper-10 .tgl:after,
.checkbox-wrapper-10 .tgl:before,
.checkbox-wrapper-10 .tgl *,
.checkbox-wrapper-10 .tgl *:after,
.checkbox-wrapper-10 .tgl *:before,
.checkbox-wrapper-10 .tgl+.tgl-btn {
    box-sizing: border-box;
}

.checkbox-wrapper-10 .tgl::-moz-selection,
.checkbox-wrapper-10 .tgl:after::-moz-selection,
.checkbox-wrapper-10 .tgl:before::-moz-selection,
.checkbox-wrapper-10 .tgl *::-moz-selection,
.checkbox-wrapper-10 .tgl *:after::-moz-selection,
.checkbox-wrapper-10 .tgl *:before::-moz-selection,
.checkbox-wrapper-10 .tgl+.tgl-btn::-moz-selection,
.checkbox-wrapper-10 .tgl::selection,
.checkbox-wrapper-10 .tgl:after::selection,
.checkbox-wrapper-10 .tgl:before::selection,
.checkbox-wrapper-10 .tgl *::selection,
.checkbox-wrapper-10 .tgl *:after::selection,
.checkbox-wrapper-10 .tgl *:before::selection,
.checkbox-wrapper-10 .tgl+.tgl-btn::selection {
    background: none;
}

.checkbox-wrapper-10 .tgl+.tgl-btn {
    outline: 0;
    display: block;
    width: 4em;
    height: 2em;
    position: relative;
    cursor: pointer;
    -webkit-user-select: none;
    -moz-user-select: none;
    -ms-user-select: none;
    user-select: none;
}

.checkbox-wrapper-10 .tgl+.tgl-btn:after,
.checkbox-wrapper-10 .tgl+.tgl-btn:before {
    position: relative;
    display: block;
    content: "";
    width: 50%;
    height: 100%;
}

.checkbox-wrapper-10 .tgl+.tgl-btn:after {
    left: 0;
}

.checkbox-wrapper-10 .tgl+.tgl-btn:before {
    display: none;
}

.checkbox-wrapper-10 .tgl:checked+.tgl-btn:after {
    left: 50%;
}

.checkbox-wrapper-10 .tgl-flip+.tgl-btn {
    padding: 2px;
    transition: all 0.2s ease;
    font-family: sans-serif;
    perspective: 100px;
}

.checkbox-wrapper-10 .tgl-flip+.tgl-btn:after,
.checkbox-wrapper-10 .tgl-flip+.tgl-btn:before {
    display: inline-block;
    transition: all 0.4s ease;
    width: 100%;
    text-align: center;
    position: absolute;
    line-height: 2em;
    font-weight: bold;
    color: #fff;
    position: absolute;
    top: 0;
    left: 0;
    -webkit-backface-visibility: hidden;
    backface-visibility: hidden;
    border-radius: 4px;
}

.checkbox-wrapper-10 .tgl-flip+.tgl-btn:after {
    content: attr(data-tg-on);
    background: #02C66F;
    transform: rotateY(-180deg);
}

.checkbox-wrapper-10 .tgl-flip+.tgl-btn:before {
    background: #FF3A19;
    content: attr(data-tg-off);
}

.checkbox-wrapper-10 .tgl-flip+.tgl-btn:active:before {
    transform: rotateY(-20deg);
}

.checkbox-wrapper-10 .tgl-flip:checked+.tgl-btn:before {
    transform: rotateY(180deg);
}

.checkbox-wrapper-10 .tgl-flip:checked+.tgl-btn:after {
    transform: rotateY(0);
    left: 0;
    background: #7FC6A6;
}

.checkbox-wrapper-10 .tgl-flip:checked+.tgl-btn:active:after {
    transform: rotateY(20deg);
}
</style>