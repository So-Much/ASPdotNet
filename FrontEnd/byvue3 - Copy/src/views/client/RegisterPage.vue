<script setup>
import RegisterForm from '@/components/RegisterForm.vue';
import { injectMainJS, removeMainJS } from '@/utils/asynchronous';
import { onBeforeMount, onMounted, onUnmounted } from 'vue';
import { axios } from '@/configs';
import { useRouter } from 'vue-router';
import { useLoading } from 'vue-loading-overlay';

const $loading = useLoading();
const router = useRouter();

onBeforeMount(() => {
    if (localStorage.getItem('token')) {
        const loader = $loading.show();
        axios.get('/api/user/isloggedin',
            {
                headers: {
                    Authorization: 'Bearer ' + localStorage.getItem('token')
                }
            }
        )
            .then(res => {
                loader.hide();
                if (res.status === 200) {
                    router.push('/');
                }
            })
            .catch(err => {
                loader.hide();
                console.log(err);
                localStorage.removeItem('token');
            });
    }
})

onMounted(() => {
    injectMainJS();
});
onUnmounted(() => {
    removeMainJS();
});



</script>

<template>
    <div class="register-container">
        <RegisterForm />
    </div>
</template>

<style scoped>
.register-container {
    background-color: #F9F9F9;
}
</style>