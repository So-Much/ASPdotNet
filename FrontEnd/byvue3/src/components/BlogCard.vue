<script setup>
import { defineProps, onMounted } from 'vue';

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
</script>

<template>
    <article class="card">
        <img alt=""
            src="https://images.unsplash.com/photo-1524758631624-e2822e304c36?ixlib=rb-1.2.1&ixid=MnwxMjA3fDF8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=2070&q=80"
            class="card-image" />

        <div class="card-content">
            <time datetime="2022-10-10" class="date">{{ date }}</time>

            <a href="#">
                <h3 class="title">{{ title }}</h3>
            </a>

            <p class="description">
                {{ description }}
            </p>
        </div>
    </article>

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
</style>