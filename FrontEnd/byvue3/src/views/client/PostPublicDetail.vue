<script setup>
import { axios } from '@/configs';
import { injectMainJS, removeMainJS } from '@/utils/asynchronous';
import { onBeforeMount, onMounted, onUnmounted, ref } from 'vue';
import { useLoading } from 'vue-loading-overlay';
import { useRoute, useRouter } from 'vue-router';
import { useToast } from 'vue-toastification';
import 'ckeditor5/ckeditor5.css';

const $loading = useLoading();
const route = useRoute();
const router = useRouter();
const toast = useToast();

console.log("🚀 ~ route:", route)
console.log("🚀 ~ $loading:", $loading)
console.log("🚀 ~ router:", router)
console.log("🚀 ~ toast:", toast)

const postId = route.params.postId;
const post = ref();
const newComment = ref('');

onMounted(() => {
    injectMainJS();
});
onUnmounted(() => {
    removeMainJS();
});

const submitComment = () => {
    if (!localStorage.getItem('token')) {
        toast.error('You need to login to comment');
        return;
    }
    if (newComment.value.trim()) {
        // Logic to submit the comment
        console.log('New comment:', newComment.value);
        newComment.value = '';
    } else {
        toast.error('Comment cannot be empty');
    }
};

onBeforeMount(() => {
    const loader = $loading.show();
    axios.get(`/api/post/${postId}`)
    .then(res => {
        document.title = res.data.title;
        post.value = res.data;
        loader.hide();
        console.log(res);
    })
    .catch(err => {
        loader.hide();
        console.log(err);
    })
});

const status = ref();
const handleStatus = (e) => {
    if (!localStorage.getItem('token')) {
        toast.error('You need to login to like/dislike');
        return;
    }
    const type = e.target.classList.contains('like-button') ? 'like' : 'dislike';
    status.value = status.value === type ? null : type;
    console.log('Status:', status.value);
}

</script>

<template>
    <div class="post-container">
        <div class="ck-content" v-if="post">
            <h1>{{ post.title }}</h1>
            <div v-html="post.content"></div>
        </div>
        <div v-else>
            Loading...
        </div>
        <hr class="divider" />
        <div class="action-buttons">
            <button :class="`like-button ${status == 'like' ? 'active' : ''}`" @click="handleStatus"><i class="fa fa-thumbs-up"></i> Like</button>
            <button :class="`dislike-button ${status == 'dislike' ? 'active' : ''}`" @click="handleStatus"><i class="fa fa-thumbs-down"></i> Dislike</button>
        </div>
    </div>
    <div class="comment-section">
        <h2>Comments</h2>
        <!-- Example comments -->
        <div class="comment">
            <p><strong>Jane Doe:</strong> Great post! I really enjoyed reading it and found it very informative.</p>
            <small>Posted on October 10, 2023</small>
        </div>
        <div class="comment">
            <p><strong>John Smith:</strong> I disagree with some points, but overall it's a well-written article.</p>
            <small>Posted on October 11, 2023</small>
        </div>
        <div class="comment">
            <p><strong>Emily Johnson:</strong> Thanks for sharing this. It was very helpful!</p>
            <small>Posted on October 12, 2023</small>
        </div>
        <!-- Comment input -->
        <div class="comment-input">
            <textarea v-model="newComment" placeholder="Add a comment..."></textarea>
            <button @click="submitComment">Submit</button>
        </div>
    </div>
</template>

<style scoped>
.post-container {
    width: 70%;
    max-width: 80%;
    margin: 0 auto;
    border-radius: 10px;
    padding: 20px;
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
    background-color: #fff;
}

.divider {
    margin: 20px 0;
    border: none;
    border-top: 1px solid #ddd;
}

.action-buttons {
    display: flex;
    justify-content: flex-start;
    margin-top: 20px;
}

.like-button, .dislike-button {
    background: none;
    border: none;
    cursor: pointer;
    font-size: 1.5em;
    margin: 0 10px;
    color: #333;
}

.like-button:hover {
    color: #007bff;
}

.dislike-button:hover {
    color: #ff0000;
}

.like-button.active {
    color: #007bff;
}

.dislike-button.active {
    color: #ff0000;
}

.comment-section {
    width: 70%;
    max-width: 80%;
    margin: 20px auto;
    padding: 20px;
    border-radius: 10px;
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
    background-color: #f9f9f9;
}

.comment {
    margin-bottom: 10px;
}

.comment p {
    margin: 0;
}

.comment small {
    display: block;
    color: #888;
    margin-top: 5px;
}

.comment-input {
    display: flex;
    flex-direction: column;
    margin-top: 20px;
}

.comment-input textarea {
    width: 100%;
    padding: 10px;
    border-radius: 5px;
    border: 1px solid #ddd;
    resize: none;
}

.comment-input button {
    align-self: flex-end;
    margin-top: 10px;
    padding: 10px 20px;
    border: none;
    border-radius: 5px;
    background-color: #007bff;
    color: #fff;
    cursor: pointer;
}

.comment-input button:hover {
    background-color: #0056b3;
}
</style>