<script setup>
</script>

<template>
  <div id="app">
    <router-link to="/">Home</router-link>
    <router-link to="/login">Login</router-link>
    <router-link to="/register">Register</router-link>
  </div>
  <router-view />
</template>

<style scoped>
@import "./global.css";

#app {
  display: flex;
  margin: 1rem;
  gap: 2rem;
  font-size: 1.5rem;
}
</style>