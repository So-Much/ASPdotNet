<script setup>
import { defineProps, onMounted } from 'vue';
const props = defineProps({
    // Normal property declarations
     data_setbg : String,
     link_to : String,
    // Styles property declarations
     styles: Object
});
onMounted(() => {
for(const selector in props.styles) {
        document.documentElement.style.setProperty(selector, props.styles[selector]);
    }
});
</script>

<template>
    <div class="hero-item portfolio-item set-bg" :data-setbg=props.data_setbg>
        <router-link :to=props.link_to class="hero-link">
            <h2>Take a look at my Portfolio</h2>
        </router-link>
    </div>
</template>

<style scoped>
</style>