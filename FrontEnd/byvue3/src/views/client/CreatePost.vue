<script setup>
import PostForm from '@/components/PostForm.vue';
import { axios } from '@/configs';

import { injectMainJS, removeMainJS } from '@/utils/asynchronous';
import { onBeforeMount, onMounted, onUnmounted } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter();

onBeforeMount(() => {
    axios.get('/api/User/isloggedin', {
      headers: {
        Authorization: 'Bearer ' + localStorage.getItem('token')
      }
    }).then( () => {

    }).catch( () => {
        router.push('/login');
    })
})

onMounted(() => {
    injectMainJS();
});
onUnmounted(() => {
    removeMainJS();
});

</script>

<template>
    <PostForm />
</template>

<style scoped>

</style>