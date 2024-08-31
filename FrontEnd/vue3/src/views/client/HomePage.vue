<script setup>
import HeaderSection from '@/components/HeaderSection.vue'
import HeroSection from '@/components/HeroSection.vue';
import IntroSection from '@/components/IntroSection.vue';
import FooterSection from '@/components/FooterSection.vue';
import SearchModel from '@/components/SearchModel.vue';

import { importedScripts } from '@/utils/ImportScripts';
import { onMounted } from 'vue';
onMounted(() => {
    importedScripts()
});
</script>

<template>
    <HeaderSection />
    <HeroSection />
    <IntroSection />
    <FooterSection />
    <SearchModel />
</template>

<style scoped>

</style>