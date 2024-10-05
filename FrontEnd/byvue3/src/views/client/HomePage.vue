<script setup>
import HeaderSection from '@/components/HeaderSection.vue'
import HeroSection from '@/components/HeroSection.vue';
// import IntroSection from '@/components/IntroSection.vue';
import FooterSection from '@/components/FooterSection.vue';
import SearchModel from '@/components/SearchModel.vue';
import { onMounted, onUnmounted } from 'vue';
import { injectMainJS, removeMainJS } from '@/utils/asynchronous';
import FeatureSection from '@/components/FeatureSection.vue';
import FeatureSectionV7 from '@/components/FeatureSectionV7.vue';
import FeatureSectionV6 from '@/components/FeatureSectionV6.vue';
import NewLetter from '@/components/NewLetter.vue';

onMounted(() => {
    injectMainJS();
});

onUnmounted(() => {
    removeMainJS();
})
</script>

<template>
    <HeaderSection />
    <HeroSection />
    <!-- <IntroSection /> -->
    <div class="">
        <div class="item"> <FeatureSection /> </div>
        <div class="item"> <FeatureSectionV7 /> </div>
        <div class="item"> <FeatureSectionV6 /> </div>
        <div class="item"> <NewLetter /> </div>
    </div>
    <FooterSection />
    <SearchModel />
</template>

<style scoped>
.item {
    position: relative;
    padding: 10px;
}

.item:not(:last-child)::after {
    content: '';
    display: block;
    height: 1px;
    background: #ccc;
    position: absolute;
    bottom: 0;
    left: 0;
    right: 0;
    max-width: 80%;
    margin-left: auto;
    margin-right: auto;
}
</style>