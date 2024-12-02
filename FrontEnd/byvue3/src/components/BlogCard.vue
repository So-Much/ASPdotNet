<script setup>
import { defineProps, onMounted, ref } from 'vue';
import ModalComponent from "@/components/ModalComponent.vue";
import { axios } from '@/configs';
import { useLoading } from 'vue-loading-overlay';
// import { useRouter } from 'vue-router';
import { useToast } from 'vue-toastification';

const $loading = useLoading();
// const router = useRouter();
const toast = useToast();

const props = defineProps({
  // Normal property declarations
  id: Number,
  title: String,
  date: String,
  description: String,
  img: String,
  isPushlish: Boolean,
  // Styles property declarations
  styles: Object,
  postCount: Number
});
onMounted(() => {
  for (const selector in props.styles) {
    document.documentElement.style.setProperty(selector, props.styles[selector]);
  }
});

const handleClosed = () => {
  const loader = $loading.show();
  axios.put(`/api/blog/${props.id}`, {
    title: titleData.value,
    description: descriptionData.value,
    isPublished: isPushlishData.value
  }, {
    headers: {
      Authorization: 'Bearer ' + localStorage.getItem('token')
    }
  }
  ).then(res => {
    window.location.reload();
    loader.hide();
    toast.success("Blog is changed!");
    console.log(res);
  })
    .catch(err => {
      loader.hide();
      console.log(err)
    })
}
const showModal = ref(false);
const descriptionData = ref(props.description);
const titleData = ref(props.title);
const isPushlishData = ref(props.isPushlish);
const imgData = ref(props.img);

const deleteModal = ref(false);

const handleUploadBlogImg = (e) => {
  const loader = $loading.show();
  const file = e.target.files[0];
  axios.put(`/api/blog/img/${props.id}`, {
    image: file,
  }, {
    headers: {
      Authorization: "Bearer " + localStorage.getItem('token'),
      "Content-Type": "multipart/form-data"
    }
  }).then(res => {
    loader.hide();
    imgData.value = res.data;
    toast.success("Successfully uploaded image");
  }).catch(err => {
    loader.hide();
    console.log(err)
  })
}

const handleDeleteBlog = () => {
  const loader = $loading.show();
  axios.delete(`/api/blog/${props.id}`, {
    headers: {
      Authorization: 'Bearer ' + localStorage.getItem('token')
    }
  }).then(res => {
    window.location.reload();
    loader.hide();
    toast.success("Successfully deleted blog");
    console.log(res);
  }).catch(err => {
    loader.hide();
    console.log(err);
  })
}
</script>

<template>
  <div class="col-md-6 col-lg-4 mb-3">
    <div class="card h-100">
      <div class="card-body">
        <h5 class="card-title">{{ title }}</h5>
        <h6 class="card-subtitle text-muted">{{ date }} - {{ postCount }} post</h6>
      </div>
      <img class="img-fluid" :src="imgData" alt="Card image cap" height="450" />
      <div class="card-body">
        <p class="card-text">{{ description }}</p>
        <div class="my-4">
          <span class="card-text" :class="{ 'published': isPushlish, 'not-published': !isPushlish }">
            {{ isPushlishData ? 'Published' : 'Privated' }}
          </span>
        </div>

        <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalToggle"
          @click="showModal = true">
          Edit This Blog
        </button>
        <button type="button" class="btn btn-danger mx-3" data-bs-toggle="modal" data-bs-target="#deleteModal"
          @click="deleteModal = true">
          Deleted This Blog
        </button>
      </div>
    </div>
  </div>

  <!-- Delete Confirmation Modal -->
  <div v-if="deleteModal" class="modal fade show" id="deleteModal" tabindex="-1" aria-labelledby="deleteModalLabel"
    aria-hidden="true" style="display: block;">
    <div class="modal-dialog modal-dialog-centered">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="deleteModalLabel">Confirm Delete</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"
            @click="deleteModal = false"></button>
        </div>
        <div class="modal-body">
          <p>Are you sure you want to delete "{{ titleData }}" blog?</p>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-danger" @click="handleDeleteBlog">Yes, Delete</button>
          <button type="button" class="btn btn-secondary" @click="deleteModal = false">Cancel</button>
        </div>
      </div>
    </div>
  </div>
  <!-- End of Delete Confirmation Modal -->

  <ModalComponent v-model="showModal" id="abc" :onClosed="handleClosed">
    <h2>Edit {{ titleData }}</h2>
    <div class="d-flex align-items-start align-items-sm-center gap-4">

      <img :src="imgData ? imgData : '/png-transparent-default-avatar-thumbnail.png'"
        @error="e => e.target.src = '/png-transparent-default-avatar-thumbnail.png'" alt="user-avatar"
        class="d-block rounded img avatar-xl" height="200" id="uploadedAvatar" />
      <div class="button-wrapper">
        <label for="upload" class="btn btn-primary me-2 mb-4" tabindex="0">
          <span class="d-none d-sm-block">Upload new photo</span>
          <i class="bx bx-upload d-block d-sm-none"></i>
          <input type="file" id="upload" class="account-file-input" hidden accept="image/png, image/jpeg"
            @input="handleUploadBlogImg" />
        </label>

        <p class="text-muted mb-0">Allowed JPG, GIF or PNG. Max size of 800K</p>
      </div>
    </div>

    <input v-model="titleData" type="text" />
    <div class="checkbox-wrapper">
      <label for="isPushlish">Pushlish?</label>
      <input id="isPushlish" v-model="isPushlishData" type="checkbox" />
    </div>
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

.published {
  color: green;
  font-weight: bold;
  border: 1px solid green;
  padding: 0.25rem;
  border-radius: 0.25rem;
}

.not-published {
  color: red;
  font-weight: bold;
  border: 1px solid red;
  padding: 0.25rem;
  border-radius: 0.25rem;
}

.modal input[type="text"] {
  border-radius: 0.25rem;
  padding: 0.5rem;
  border: 1px solid rgba(0, 0, 0, 0.1);
  margin: 0.5rem 0;
}

.checkbox-wrapper {
  display: flex;
  align-items: center;
  margin: 0.5rem 0;
}

.checkbox-wrapper label {
  margin-right: 0.5rem;
}

.checkbox-wrapper input[type="checkbox"] {
  padding: 0.25rem;
}

@import '@/assets/vendor/css/core.css';
@import '@/assets/vendor/css/theme-default.css';
@import '@/assets/vendor/css/pages/page-auth.css';
</style>