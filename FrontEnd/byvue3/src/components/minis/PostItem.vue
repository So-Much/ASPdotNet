<script setup>
import { onMounted, onUnmounted } from 'vue';
import { injectMainJS, removeMainJS } from '@/utils/asynchronous';

onMounted(() => {
    injectMainJS();
});
onUnmounted(() => {
    removeMainJS();
});
</script>

<template>
    <div class="blog-post">
        <div class="blog-thumb">
            <img src="/assets/images/blog-thumb-01.jpg" alt="">
        </div>
        <div class="down-content">
            <span>Hello world</span>
            <a href="post-details.html">
                <h4>Donec tincidunt leo</h4>
            </a>
            <ul class="post-info">
                <li><a href="#">26 <i class="fa-solid fa-heart"></i></a></li>
                <li><a href="#">12 Comments</a></li>
                <li><a href="#">May 31, 2020</a></li>
            </ul>
            <p>Nullam nibh mi, tincidunt sed sapien ut, rutrum hendrerit velit. Integer
                auctor a mauris sit amet eleifend.</p>
            <div class="post-options">
                <div class="row">
                    <div class="col-lg-12">
                        <ul class="post-tags">
                            <li><i class="fa fa-tags"></i></li>
                            <li><a href="#">Best Templates</a>,</li>
                            <li><a href="#">TemplateMo</a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped></style>