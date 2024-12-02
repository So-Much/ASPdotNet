<script setup>
import PostCard from "@/components/PostCard.vue";
import { axios } from "@/configs";
import { onBeforeMount, ref } from "vue";
import { useLoading } from "vue-loading-overlay";
import { useRouter } from "vue-router";
import { useToast } from "vue-toastification";

const $loading = useLoading();
const router = useRouter();
const toast = useToast();

const blogs = ref();
// const dummyData = [
//   {
//     Id: 1,
//     Title: "Blog 1",
//     Description: "Description 1",
//     CreatedAt: "2021-10-10",
//     UpdatedAt: "2021-10-10",
//     Posts: [
//     ]
//   },
//   {
//     Id: 2,
//     Title: "Blog 2",
//     Description: "Description 2",
//     CreatedAt: "2021-10-10",
//     UpdatedAt: "2021-10-10",
//     Posts: [
//       {
//         Id: 1,
//         Title: "Post 1",
//         Content: "Content 1",
//         Images: [
//           'https://via.placeholder.com/450x300',
//           'https://via.placeholder.com/450x300',
//         ],
//         Numlikes: 10,
//         NumDisLike: 5,
//         IsPublished: true,
//         HashTags: [
//           'tag1',
//           'tag2',
//         ],
//         CreatedAt: '2021-10-10',
//       },
//       {
//         Id: 2,
//         Title: "Post 2",
//         Content: "Content 2",
//         Images: [
//           'https://via.placeholder.com/450x300',
//           'https://via.placeholder.com/450x300',
//         ],
//         Numlikes: 10,
//         NumDisLike: 5,
//         IsPublished: false,
//         HashTags: [
//           'tag1',
//           'tag2',
//         ],
//         CreatedAt: '2021-10-10',
//       },
//       {
//         Id: 3,
//         Title: "Post 3",
//         Content: "Content 3",
//         Images: [
//           'https://via.placeholder.com/450x300',
//           'https://via.placeholder.com/450x300',
//         ],
//         Numlikes: 10,
//         NumDisLike: 5,
//         IsPublished: true,
//         HashTags: [
//           'tag1',
//           'tag2',
//         ],
//         CreatedAt: '2021-10-10',
//       }
//     ]
//   }
// ]


onBeforeMount(() => {
  document.title = 'User Post';

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
  <div>
    <div class="content-wrapper">
      <div class="container-fluid flex-grow-1 container-p-y">
        <div v-for="blog in blogs" :key="blog.id">
          <h2 class="userblog_title">{{ blog.title }} 
            <button class="btn btn-primary mb-4"
              @click="router.push(`/blogs/${blog.id }/post/create`)">
              Create Post
            </button>
          </h2>
          <p>{{ blog.description }}</p>
          <div class="row mb-5">
            <div v-for="post in blog.posts" :key="post.Id" class="col col-12 col-md-6 col-lg-4">
              <!--              link to post detail-->
              <PostCard :carouselId="post.id" :blogId="blog.id" :title="post.title" :content="post.content" :images="post.images"
                :numlikes="post.numLikes" :numdislike="post.numDislike" :ispublished="post.isPublished"
                :hashtags="post.hashtags" :createdat="post.createAt" />
            </div>
          </div>

        </div>
      </div>
    </div>
  </div>
</template>

<style scoped></style>