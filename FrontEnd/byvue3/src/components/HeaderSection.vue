<script setup>
// import { axios } from '@/configs';
import { axios } from '@/configs';
import { injectMainJS, removeMainJS } from '@/utils/asynchronous';
import { onBeforeMount, onMounted, onUnmounted, reactive, ref } from 'vue';
// import { useLoading } from 'vue-loading-overlay';

onMounted(() => {
	injectMainJS();
});
onUnmounted(() => {
	removeMainJS();
});

const isLogged = ref(false);

const user = reactive({

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
				res => {
					if (res.status === 200) {
						isLogged.value = true;
						axios.get('/api/user/information',
							{
								headers: {
									Authorization: 'Bearer ' + localStorage.getItem('token')
								}
							}
						)
							.then(
								res => {
									if (res.status === 200) {
										user.username = res.data.name;
										user.userID = res.data.uid;
									}
								}
							)
					}
				}
			)
			.catch(
				err => {
					isLogged.value = false;
					console.log(err);
					localStorage.removeItem('token');
				}
			);
	} else {
		isLogged.value = false;
	}
});

// const $loading = useLoading();




</script>

<template>
	<!-- Header section  -->
	<header class="header-section">
		<router-link to="/" class="site-logo">
			<img src="../assets/logo.png" @error="e => e.target.src = ''" alt="logo" />
		</router-link>
		<div class="header-controls">
			<button class="nav-switch-btn"><i class="fa fa-bars"></i></button>
			<button class="search-btn"><i class="fa fa-search"></i></button>
		</div>
		<ul class="main-menu">
			<li><router-link to="/">Home</router-link></li>
			<li><router-link to="/store">Store</router-link></li>
			<li><router-link to="/blogs">Blogs</router-link></li>
			<li>
				<!-- <router-link to="/my-portfolio">Portfolio</router-link> -->
				<router-link to="/photographers">Photographers</router-link>
				<!-- <ul class="sub-menu">
					<li><a href="portfolio.html">Portfolio 1</a></li>
					<li><a href="portfolio-1.html">Portfolio 2</a></li>
					<li><a href="portfolio-2.html">Portfolio 3</a></li>
				</ul> -->
			</li>
			<!-- <li><a href="elements.html">Elements</a></li> -->
			<li><router-link to="/contact">Contact</router-link></li>
			<li><router-link v-if="isLogged" :to="'/user/' + user.username">{{`Welcome, ${user.username} ` }}</router-link></li>
			<li><router-link v-if="isLogged" to="/login">Logout</router-link></li>
			<li>
				<router-link v-if="!isLogged" to="/login">Login</router-link>
			</li>
			<li>
				<router-link v-if="!isLogged" to="/register">Register</router-link>
			</li>
			<li class="search-mobile">
				<button class="search-btn"><i class="fa fa-search"></i></button>
			</li>
		</ul>
	</header>
	<div class="clearfix"></div>
	<!-- Header section end  -->
</template>

<style scoped></style>