<script setup>
import { onBeforeMount, ref } from "vue";
import { axios } from '@/configs';
import { useToast } from 'vue-toastification';
import { useRouter } from 'vue-router';
import { useLoading } from 'vue-loading-overlay';
import BlogCard from '@/components/BlogCard.vue';
// import ModalComponent from "@/components/ModalComponent.vue";

const $loading = useLoading();
const router = useRouter();
const toast = useToast();
const blogs = ref();

onBeforeMount(() => {
  document.title = 'User Blogs';

  if (localStorage.getItem('token')) {
    const loader = $loading.show();
    axios.get('/api/User/isloggedin', {
      headers: {
        Authorization: 'Bearer ' + localStorage.getItem('token')
      }
    }).then(res => {
      if (res.status === 200) {
        axios.get('/api/blog/allblogsbyuser', {
          headers: {
            Authorization: 'Bearer ' + localStorage.getItem('token')
          }
        }).then(res => {
          blogs.value = res.data;
          console.log(blogs)
          loader.hide();
        }).catch(err => {
          console.log(err);
          loader.hide();
        })
      }
    }).catch(() => {
      loader.hide();
      toast.error('Your Token has expired. Please login again');
      router.push('/login');
    })
  }
});

</script>

<template>
  <div class="userblog">


    <div class="content-wrapper">
      <div class="container-fluid flex-grow-1 container-p-y">

        <button class="btn btn-primary mb-4" @click="router.push('/blog/create')">
          CreateBlog
        </button>
        <div class="row mb-5">
          <BlogCard v-for="(blog, index) in blogs" :key="index"
          :id="blog.id"
            :isPushlish="blog.isPublished"
            :img="blog.image"
            :title="blog.title"
            :date="new Date(blog.createAt).toLocaleDateString()"
            :description="blog.description"
            :postCount="blog.posts.length"
          />
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.userblog {
  margin-top: 20px;
  display: flex;
  flex-direction: column;
  gap: 1rem;
}
</style>