<script setup>
import LoginForm from '@/components/LoginForm.vue';
import { injectMainJS, removeMainJS } from '@/utils/asynchronous';
import { onMounted, onUnmounted, onBeforeMount } from 'vue';
import { axios } from '@/configs';
import { useRouter } from 'vue-router';
import { useLoading } from 'vue-loading-overlay';
import { useToast } from 'vue-toastification';

const $loading = useLoading();
const router = useRouter();
const toast = useToast();

onMounted(() => {
    injectMainJS();
});
onUnmounted(() => {
    removeMainJS();
});

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
                    toast.warning('You has logged in already!')
                    router.push('/');
                }
            })
            .catch( () => {
                loader.hide();
                // console.log(err);
                toast.error("Token is invalid!")
                localStorage.removeItem('token');
            });
    }
});

</script>

<template>
    <div class="login-container">
        <LoginForm />
    </div>
</template>

<style scoped>
.login-container {
    background-color: #F9F9F9;
}
</style>