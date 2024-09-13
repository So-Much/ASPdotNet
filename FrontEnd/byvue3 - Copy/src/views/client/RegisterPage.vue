<script setup>
import RegisterForm from '@/components/RegisterForm.vue';
import { injectMainJS, removeMainJS } from '@/utils/asynchronous';
import { onMounted, onUnmounted } from 'vue';

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