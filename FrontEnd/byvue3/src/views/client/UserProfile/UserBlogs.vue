<script setup>
import BlogCard from '@/components/BlogCard.vue';
import BtnCreate from '@/components/minis/BtnCreate.vue';
import ModalComponent from "@/components/ModalComponent.vue";
import { ref } from "vue";

const createBlog = () => {
  console.log("Create Blog");
}

const showModal = ref(false);
</script>

<template>
  <div class="userblog">

    <h2 class="userblog_title">Recent Blogs:</h2>

    <div class="content-wrapper">
      <div class="container-fluid flex-grow-1 container-p-y">
        <BtnCreate
            btnName="Create Blog"
            :calltofn="createBlog"
            class="mb-3"
        />
        <div class="row mb-5">
          <BlogCard
              title="How to position your furniture for positivity"
              date="10th Oct 2022"
              description="Lorem ipsum dolor sit amet, consectetur adipisicing elit. Recusandae dolores, possimus
        pariatur animi temporibus nesciunt praesentium dolore sed nulla ipsum eveniet corporis quidem,  mollitia itaque minus soluta, voluptates neque explicabo tempora nisi culpa eius atque dignissimos. Molestias explicabo corporis voluptatem?"
          />
          <BlogCard
              title="How to position your furniture for positivity"
              date="10th Oct 2022"
              description="Lorem ipsum dolor sit amet, consectetur adipisicing elit. Recusandae dolores, possimus
        pariatur animi temporibus nesciunt praesentium dolore sed nulla ipsum eveniet corporis quidem,  mollitia itaque minus soluta, voluptates neque explicabo tempora nisi culpa eius atque dignissimos. Molestias explicabo corporis voluptatem?"
          />
          <BlogCard
              title="How to position your furniture for positivity"
              date="10th Oct 2022"
              description="Lorem ipsum dolor sit amet, consectetur adipisicing elit. Recusandae dolores, possimus
        pariatur animi temporibus nesciunt praesentium dolore sed nulla ipsum eveniet corporis quidem,  mollitia itaque minus soluta, voluptates neque explicabo tempora nisi culpa eius atque dignissimos. Molestias explicabo corporis voluptatem?"
          />
        </div>
      </div>
    </div>
  </div>
  <button
      type="button"
      class="btn btn-primary"
      data-bs-toggle="modal"
      data-bs-target="#modalToggle"
      @click="showModal = true"
  >
    Launch modal
  </button>
  <ModalComponent v-model="showModal">
    <h2>Modal Content</h2>
    <p>This is a simple modal component in Vue 3!</p>
  </ModalComponent>
</template>

<style scoped>
.userblog {
  margin-top: 20px;
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.userblog_title {
  margin-left: 1.2rem;
}
</style>