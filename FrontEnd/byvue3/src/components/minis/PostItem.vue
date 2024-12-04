<script setup>
import { injectMainJS, removeMainJS } from '@/utils/asynchronous';
import { defineProps, onMounted, onUnmounted, ref } from 'vue';
import { useRouter } from 'vue-router';

onMounted(() => {
    injectMainJS();
    for (const selector in props.styles) {
        document.documentElement.style.setProperty(selector, props.styles[selector]);
    }
});
onUnmounted(() => {
    removeMainJS();
});

const router = useRouter();

const formatDate = (dateString) => {
    const date = new Date(dateString);
    const pad = (num) => num.toString().padStart(2, '0');
    return `${pad(date.getHours())}:${pad(date.getMinutes())}:${pad(date.getSeconds())} ${pad(date.getDate())}/${pad(date.getMonth() + 1)}/${date.getFullYear()}`;
};

const props = defineProps({
    // Normal property declarations
    post: Object,
    blogId: String,
    // Styles property declarations
    styles: Object
});

const imageUrl = ref(props.post.images[0]);

const handlePostClick = () => {
    console.log('Post clicked');
    console.log('Post: ', props.post);
    router.push(`/blogs/${props.blogId}/post/${props.post.id}`)
}

</script>

<template>
    <div class="blog-post blog-post-cus" @click="handlePostClick">
        <div class="blog-thumb">
            <img :src="imageUrl || 'https://via.placeholder.com/600x400'" alt="No Image in this post">
        </div>
        <div class="down-content">
            <!-- <span>Hello world</span> -->
            <a href="#" @click.prevent="">
                <h4>{{ props.post.title }}</h4>
            </a>
            <ul class="post-info">
                <div>
                    <li><a href="#" @click.prevent="">{{ formatDate(props.post.createAt) }}</a></li>
                </div>
                <li><a href="#" @click.prevent="">{{ props.post.numLikes }} <i class="fa-solid fa-heart"></i></a></li>
                <li><a href="#" @click.prevent="">{{ props.post.numDislike }} <i class="fa fa-thumbs-o-down"></i></a></li>
                <li><a href="#" @click.prevent="">{{ props.post.comments.length }} Comments</a></li>
            </ul>
            <!-- <p>Nullam nibh mi, tincidunt sed sapien ut, rutrum hendrerit velit. Integer
                auctor a mauris sit amet eleifend.</p> -->
            <div class="post-options">
                <div class="row">
                    <div class="col-lg-12">
                        <ul class="post-tags" v-if="!(props.post.hashtags.length == 0)">
                            <li><i class="fa fa-tags"></i></li>
                            <li v-for="(tag, index) in props.post.hashtags" :key="index">
                                <a href="#" @click.prevent="" class="px-2" >{{ tag }}</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped>
.blog-post-cus {
    cursor: pointer;
}
</style>