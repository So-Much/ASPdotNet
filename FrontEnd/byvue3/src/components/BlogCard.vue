<script setup>
import { defineProps, onMounted, ref } from 'vue';
import ModalComponent from "@/components/ModalComponent.vue";
// import ModalComponent from "@/components/ModalComponent.vue";
const props = defineProps({
  // Normal property declarations
  title: String,
  date: String,
  description: String,
  // Styles property declarations
  styles: Object
});
onMounted(() => {
  for (const selector in props.styles) {
    document.documentElement.style.setProperty(selector, props.styles[selector]);
  }
});

const handleClosed = () => {
  console.log("Modal Closed");
}
const showModal = ref(false);
const descriptionData = ref(props.description);
</script>

<template>
  <div class="col-md-6 col-lg-4 mb-3">
    <div class="card h-100">
      <div class="card-body">
        <h5 class="card-title">{{ title }}</h5>
        <h6 class="card-subtitle text-muted">{{ date }}</h6>
      </div>
      <img class="img-fluid" src="/assets/images/banner-item-01.jpg" alt="Card image cap" height="450"/>
      <div class="card-body">
        <p class="card-text">{{ descriptionData }}</p>

        <button
            type="button"
            class="btn btn-primary"
            data-bs-toggle="modal"
            data-bs-target="#modalToggle"
            @click="showModal = true"
        >
          Edit This Blog
        </button>
        <a href="javascript:void(0);" class="card-link">Another link</a>
      </div>
    </div>
  </div>

  <ModalComponent v-model="showModal" id="abc" :onClosed="handleClosed">
    <h2>Edit {{ title }}</h2>
    <p>This is a simple modal component in Vue 3!</p>
    <textarea v-model="descriptionData" class="form-control" rows="3"></textarea>
  </ModalComponent>


</template>

<style scoped>
.card {
  overflow: hidden;
  border-radius: 0.5rem;
  /* rounded-lg */
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  /* shadow */
  transition: box-shadow 0.3s;
  /* transition */
}

.card:hover {
  box-shadow: 0 4px 20px rgba(0, 0, 0, 0.2);
  /* hover:shadow-lg */
}

.card-image {
  height: 14rem;
  /* h-56 */
  width: 100%;
  /* w-full */
  object-fit: cover;
  /* object-cover */
}

.card-content {
  background-color: white;
  /* bg-white */
  padding: 1rem;
  /* p-4 */
}

.date {
  display: block;
  /* block */
  font-size: 0.75rem;
  /* text-xs */
  color: #6b7280;
  /* text-gray-500 */
}

.title {
  margin-top: 0.125rem;
  /* mt-0.5 */
  font-size: 1.125rem;
  /* text-lg */
  color: #1f2937;
  /* text-gray-900 */
}

.description {
  margin-top: 0.5rem;
  /* mt-2 */
  line-height: 1.5;
  /* line-clamp-3 / text-sm/relaxed */
  color: #6b7280;
  /* text-gray-500 */
}

@import '@/assets/vendor/css/core.css';
@import '@/assets/vendor/css/theme-default.css';
@import '@/assets/vendor/css/pages/page-auth.css';
</style>