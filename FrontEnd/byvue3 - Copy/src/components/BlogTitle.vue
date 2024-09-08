<script setup>
import { defineProps, onMounted } from 'vue';
const props = defineProps({
  // Normal property declarations
  author_name: String,
  author_role: String,
  author_intro: String,
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
    <section class="call-to-action">
      <div class="container">
        <div class="row">
          <div class="col-lg-12">
            <div class="main-content">
              <div class="row">
                <div class="col-lg-8">
                  <span>{{ `${author_name} | ${author_role}` }}</span>
                  <h4>{{ author_intro }}</h4>
                </div>
                <div class="col-lg-4">
                  <div class="main-button">
                    <a href="https://templatemo.com/tm-551-stand-blog" target="_parent">
                      Follow Now!
                    </a>
                  </div>
                  <span style="display: flex; justify-content: end; padding-top: 20px;"><i>Create at: 01/01/2000</i></span>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
</template>

<style scoped>

</style>