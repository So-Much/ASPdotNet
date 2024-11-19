<template>
  <div class="card my-3">
    <!-- Carousel with unique ID, autoplay, indicators, and captions -->
    <div
        v-if="images && images.length"
        :id="carouselId"
        class="carousel slide"
        data-bs-ride="carousel"
        data-bs-interval="3000"
    >
      <!-- Carousel Indicators -->
      <ol class="carousel-indicators">
        <li
            v-for="(image, index) in images"
            :key="'indicator-' + index"
            :data-bs-target="'#' + carouselId"
            :data-bs-slide-to="index"
            :class="{ active: index === 0 }"
        ></li>
      </ol>

      <!-- Carousel Inner with captions -->
      <div class="carousel-inner">
        <div
            v-for="(image, index) in images"
            :key="index"
            :class="['carousel-item', { active: index === 0 }]"
        >
          <img :src="image" class="d-block w-100" :alt="`Slide ${index + 1}`" />
          <div class="carousel-caption d-none d-md-block">
            <h3>{{ image.title }}</h3>
            <p>{{ image.caption }}</p>
          </div>
        </div>
      </div>

      <!-- Carousel controls -->
      <a class="carousel-control-prev" :href="'#' + carouselId" role="button" data-bs-slide="prev">
        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
        <span class="visually-hidden">Previous</span>
      </a>
      <a class="carousel-control-next" :href="'#' + carouselId" role="button" data-bs-slide="next">
        <span class="carousel-control-next-icon" aria-hidden="true"></span>
        <span class="visually-hidden">Next</span>
      </a>
    </div>

    <div class="card-body">
      <h5 class="card-title">{{ title }}</h5>
      <p class="card-text">{{ content }}</p>

      <div class="d-flex justify-content-between align-items-center">
        <div>
          <span class="text-success me-2">👍 {{ numlikes }}</span>
          <span class="text-danger">👎 {{ numdislike }}</span>
        </div>
        <div>
          <span v-if="ispublished" class="badge bg-success">Published</span>
          <span v-else class="badge bg-secondary">Unpublished</span>
        </div>
      </div>

      <div class="mt-3">
        <span v-for="(hashtag, index) in hashtags" :key="index" class="badge bg-primary me-1">
          #{{ hashtag }}
        </span>
      </div>

      <p class="text-muted mt-3">{{ formattedDate }}</p>
    </div>
  </div>
</template>

<script>
export default {
  name: "PostCard",
  props: {
    title: String,
    content: String,
    images: {
      type: Array,
      default: () => [],
    },
    numlikes: Number,
    numdislike: Number,
    ispublished: Boolean,
    hashtags: Array,
    createdat: String,
    carouselId: {
      type: String,
      required: true,
    },
  },
  computed: {
    formattedDate() {
      return new Date(this.createdat).toLocaleDateString();
    },
  },
};
</script>

<style scoped>
.carousel-item img {
  max-height: 400px;
  object-fit: cover;
}

@import '@/assets/vendor/css/core.css';
@import '@/assets/vendor/css/theme-default.css';
@import '@/assets/vendor/css/pages/page-auth.css';
</style>
