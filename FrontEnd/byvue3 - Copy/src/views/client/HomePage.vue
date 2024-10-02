<script setup>
import HeaderSection from '@/components/HeaderSection.vue'
import HeroSection from '@/components/HeroSection.vue';
// import IntroSection from '@/components/IntroSection.vue';
import FooterSection from '@/components/FooterSection.vue';
import SearchModel from '@/components/SearchModel.vue';
import { onMounted, onUnmounted } from 'vue';
import { injectMainJS,removeMainJS } from '@/utils/asynchronous';
// import HomeBannerList from '@/components/HomeBannerList.vue';
import LocalIntroSection from '@/components/LocalIntroSection.vue';

onMounted(() => {
    injectMainJS();
});

onUnmounted(()=> {
    removeMainJS();
})
</script>

<template>
    <HeaderSection />
    <HeroSection />
    <!-- <IntroSection /> -->
     <LocalIntroSection />
      <!-- <div class="content">
          <HomeBannerList />
      </div> -->
    <FooterSection />
    <SearchModel />
</template>

<style scoped>
.content {
    margin-top: 5rem
}
</style>