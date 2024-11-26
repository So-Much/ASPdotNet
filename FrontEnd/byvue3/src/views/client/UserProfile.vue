<script setup>
import HeaderSection from '@/components/HeaderSection.vue';
import UserSidebar from '@/components/UserSidebar.vue';
import { axios } from '@/configs';
import { injectMainJS, removeMainJS } from '@/utils/asynchronous';
import { onBeforeMount, onMounted, onUnmounted } from 'vue';
import { useRouter } from 'vue-router';
import { useToast } from 'vue-toastification';

const router = useRouter();
const toast = useToast();
// import { useRoute } from 'vue-router';
onMounted(() => {
    injectMainJS();
});
onUnmounted(() => {
    removeMainJS();
});

onBeforeMount(() => {
    if (localStorage.getItem('token')) {
		axios.get('/api/user/isloggedin',
			{
				headers: {
					Authorization: 'Bearer ' + localStorage.getItem('token')
				}
			}
		)
			.then(
			)
			.catch(
				() => {
					localStorage.removeItem('token');
                    toast.error("Token is Expired!")
                    router.push('/login')
				}
			);
	} else {
        router.push('/login');
        toast.error("Token is Expired!")
    }
})


// const route = useRoute();
// const userID = route.params.userId;

</script>

<template>
    <HeaderSection />
    <UserSidebar />
    <div class="user_profile_view">
        <router-view></router-view>
    </div>
</template>

<style scoped>

.user_profile_view {
    margin-left: 296px;    
    height: auto;
}
</style>