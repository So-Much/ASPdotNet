<script setup>
import PhotoBlogs from './minis/PhotoBlogs.vue';

const array = Array.from({ length: 21 }, (_, i) => i + 1);
var array1 = array.slice(0, 7).map((item) => {
	return {
		id: item,
		data_setbg: `img/portfolio/${item}.jpg`,
		link_to: '/portfolio',
		styles: {},
	};
});

var array2 = array.slice(7, 14).map((item) => {
	return {
		id: item,
		data_setbg: `img/portfolio/${item}.jpg`,
		link_to: '/portfolio',
		styles: {},
	};
});

var array3 = array.slice(14, 21).map((item) => {
	return {
		id: item,
		data_setbg: `img/portfolio/${item}.jpg`,
		link_to: '/portfolio',
		styles: {},
	};
});

console.log(array1)
</script>

<template>
	<!-- Hero section  -->
	<div class="hero-section">
		<PhotoBlogs :list_blog="array1" />
		<PhotoBlogs :list_blog="array2" />
		<PhotoBlogs :list_blog="array3" />
		<div class="hero-social-links">
			<a href=""><i class="fa fa-pinterest"></i></a>
			<a href=""><i class="fa fa-facebook"></i></a>
			<a href=""><i class="fa fa-twitter"></i></a>
			<a href=""><i class="fa fa-dribbble"></i></a>
			<a href=""><i class="fa fa-behance"></i></a>
		</div>
	</div>
	<!-- Hero section end  -->
</template>

<style scoped></style>