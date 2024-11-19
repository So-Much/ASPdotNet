<script setup>
import PostCard from "@/components/PostCard.vue";
const dummyData = [
  {
    Id: 1,
    Title: "Blog 1",
    Description: "Description 1",
    CreatedAt: "2021-10-10",
    UpdatedAt: "2021-10-10",
    Posts: [
      {
        Id: 1,
        Title: "Post 1",
        Content: "Content 1",
        Images: [
          'https://via.placeholder.com/450x300',
          'https://via.placeholder.com/450x300',
        ],
        Numlikes: 10,
        NumDisLike: 5,
        IsPublished: true,
        HashTags: [
          'tag1',
          'tag2',
        ],
        CreatedAt: '2021-10-10',
      },
      {
        Id: 2,
        Title: "Post 2",
        Content: "Content 2",
        Images: [
          'https://via.placeholder.com/450x300',
          'https://via.placeholder.com/450x300',
        ],
        Numlikes: 10,
        NumDisLike: 5,
        IsPublished: true,
        HashTags: [
          'tag1',
          'tag2',
        ],
        CreatedAt: '2021-10-10',
      },
      {
        Id: 3,
        Title: "Post 3",
        Content: "Content 3",
        Images: [
          'https://via.placeholder.com/450x300',
          'https://via.placeholder.com/450x300',
        ],
        Numlikes: 10,
        NumDisLike: 5,
        IsPublished: true,
        HashTags: [
          'tag1',
          'tag2',
        ],
        CreatedAt: '2021-10-10',
      }
    ]
  },
  {
    Id: 2,
    Title: "Blog 2",
    Description: "Description 2",
    CreatedAt: "2021-10-10",
    UpdatedAt: "2021-10-10",
    Posts: [
      {
        Id: 1,
        Title: "Post 1",
        Content: "Content 1",
        Images: [
          'https://via.placeholder.com/450x300',
          'https://via.placeholder.com/450x300',
        ],
        Numlikes: 10,
        NumDisLike: 5,
        IsPublished: true,
        HashTags: [
          'tag1',
          'tag2',
        ],
        CreatedAt: '2021-10-10',
      },
      {
        Id: 2,
        Title: "Post 2",
        Content: "Content 2",
        Images: [
          'https://via.placeholder.com/450x300',
          'https://via.placeholder.com/450x300',
        ],
        Numlikes: 10,
        NumDisLike: 5,
        IsPublished: true,
        HashTags: [
          'tag1',
          'tag2',
        ],
        CreatedAt: '2021-10-10',
      },
      {
        Id: 3,
        Title: "Post 3",
        Content: "Content 3",
        Images: [
          'https://via.placeholder.com/450x300',
          'https://via.placeholder.com/450x300',
        ],
        Numlikes: 10,
        NumDisLike: 5,
        IsPublished: true,
        HashTags: [
          'tag1',
          'tag2',
        ],
        CreatedAt: '2021-10-10',
      }
    ]
  }
]
</script>

<template>
  <div>
    <div class="content-wrapper">
      <div class="container-fluid flex-grow-1 container-p-y">
        <div v-for="blog in dummyData" :key="blog.Id">
          <h2 class="userblog_title">{{ blog.Title }}</h2>
          <p>{{ blog.Description }}</p>
          <div class="row mb-5">
            <div v-for="post in blog.Posts" :key="post.Id" class="col col-12 col-md-6 col-lg-4">
<!--              link to post detail-->
              <PostCard
                  :carouselId="post.Id"
                  :title="post.Title"
                  :content="post.Content"
                  :images="post.Images"
                  :numlikes="post.Numlikes"
                  :numdislike="post.NumDisLike"
                  :ispublished="post.IsPublished"
                  :hashtags="post.HashTags"
                  :createdat="post.CreatedAt"
              />
            </div>
          </div>

        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>

</style>