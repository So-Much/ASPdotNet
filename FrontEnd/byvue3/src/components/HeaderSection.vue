<script setup>

</script>

<template>
	<!-- Header section  -->
	<header class="header-section">
		<a href="index.html" class="site-logo">
			<img src="../assets/logo.png" @error="e=>e.target.src=''" alt="logo" />
		</a>
		<div class="header-controls">
			<button class="nav-switch-btn"><i class="fa fa-bars"></i></button>
			<button class="search-btn"><i class="fa fa-search"></i></button>
		</div>
		<ul class="main-menu">
			<li><router-link to="/">Home</router-link></li>
			<li><router-link to="/store">Store</router-link></li>
			<li>
				<router-link to="/my-portfolio">Portfolio</router-link>
				<!-- <ul class="sub-menu">
					<li><a href="portfolio.html">Portfolio 1</a></li>
					<li><a href="portfolio-1.html">Portfolio 2</a></li>
					<li><a href="portfolio-2.html">Portfolio 3</a></li>
				</ul> -->
			</li>
			<li><router-link to="/my-blogs">Blogs</router-link></li>
			<!-- <li><a href="elements.html">Elements</a></li> -->
			<li><router-link to="/contact">Contact</router-link></li>
			<li class="search-mobile">
				<button class="search-btn"><i class="fa fa-search"></i></button>
			</li>
		</ul>
	</header>
	<div class="clearfix"></div>
	<!-- Header section end  -->
</template>

<style scoped></style>