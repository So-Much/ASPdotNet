<script setup>

import { defineProps, onMounted } from 'vue';
const props = defineProps({
  // Normal property declarations
   blog_name: String,
   blog_title: String,
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
    <div class="heading-page header-text">
      <section class="page-heading">
        <div class="container">
          <div class="row">
            <div class="col-lg-12">
              <div class="text-content">
                <h4>{{ blog_name }}</h4>
                <h2>{{ blog_title }}</h2>
              </div>
            </div>
          </div>
        </div>
      </section>
    </div>
</template>

<style scoped>

</style>