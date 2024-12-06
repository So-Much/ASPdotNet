<script setup>
import { onMounted } from 'vue';


onMounted(() => {
  document.title = "BlogPots";
});

</script>

<template>
  <!-- <div id="app">
    <router-link to="/">Home</router-link>
    <router-link to="/store">Store</router-link>
    <router-link to="/blogs">Blogs List</router-link>
    <router-link to="/login">Login</router-link>
    <router-link to="/register">Register</router-link>
    <router-link to="/dashboard">Dashboard</router-link>
    <router-link to="/dashboard/products">Products</router-link>
    <router-link to="/dashboard/blogs">Blogs Admin</router-link>
    <router-link to="/post/create">Editor</router-link>
  </div> -->
  <router-view />
</template>

<style scoped>
/* @import "./global.css"; */

#app {
  display: flex;
  margin: 1rem;
  gap: 2rem;
  font-size: 1.5rem;
}

</style>