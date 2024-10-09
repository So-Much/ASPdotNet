<script setup>
import BlogCard from '@/components/BlogCard.vue';
import BtnCreate from '@/components/minis/BtnCreate.vue';

const createBlog = () => {
    console.log("Create Blog");
}

</script>

<template>
    <div class="userblog">
        <BtnCreate
        btnName="Create Blog"
        :calltofn="createBlog"
        />
        <h2 class="userblog_title">Recent Blogs:</h2>
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
        <h1>UserBlogs</h1>
    </div>
</template>

<style scoped>
.userblog {
    display: flex;
    flex-direction: column;
    gap: 1rem;
}
.userblog_title {
    margin-left: 1.2rem;
}
</style>