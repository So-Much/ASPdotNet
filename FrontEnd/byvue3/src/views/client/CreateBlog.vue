<script setup>
import { axios } from '@/configs';
import { injectMainJS, removeMainJS } from '@/utils/asynchronous';
import { onBeforeMount, onMounted, onUnmounted, ref } from 'vue';
import { useLoading } from 'vue-loading-overlay';
import { useRouter } from 'vue-router';
import { useToast } from 'vue-toastification';

const router = useRouter();
const toast = useToast();
const $loading = useLoading();

const title = ref('');
const description = ref('');
const image = ref(null);
const imageUrl = ref('');
const isPublished = ref(false);

onBeforeMount(() => {

});

onMounted(() => {
    document.title = 'Create Blog';
    injectMainJS();
});

onUnmounted(() => {
    removeMainJS();
});

const handleImageChange = (event) => {
    const files = event.target.files;
    if (files.length > 0) {
        image.value = files[0];
        imageUrl.value = URL.createObjectURL(files[0]);
    }
};

const handleSubmit = () => {
    if (localStorage.getItem('token')) {
        const loader = $loading.show();
        axios.get('/api/User/isloggedin', {
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }).then(res => {
            if (res.status === 200) {
                // Handle form submission logic
                axios.post('/api/blog', {
                    title: title.value,
                    description: description.value,
                    image: image.value,
                    isPublished: isPublished.value
                }, {
                    headers: {
                        Authorization: 'Bearer ' + localStorage.getItem('token'),
                        'Content-Type': 'multipart/form-data'
                    }
                })
                    .then(res => {
                        if (res.status === 200) {
                            toast.success('Blog created successfully');
                            router.go(-1); // Go back to the previous route
                        }
                        loader.hide();
                    })
                    .catch(err => {
                        loader.hide();
                        toast.error('Error creating blog');
                        console.error(err);
                    });
            }
        }).catch(() => {
            loader.hide();
            toast.error('Your Token has expired. Please login again');
            router.push('/login');
        })
    }

};
</script>

<template>
    <div class="d-flex justify-content-center align-items-center vh-100">
        <div class="w-50">
            <h2 class="text-center mb-4 title-bold">Create Blog</h2>
            <form @submit.prevent="handleSubmit" class="p-4 border rounded">
                <div class="mb-3">
                    <label for="title" class="form-label label-bold">Title</label>
                    <input id="title" v-model="title" type="text" class="form-control" required />
                </div>
                <div class="mb-3">
                    <label for="description" class="form-label label-bold">Description</label>
                    <textarea id="description" v-model="description" class="form-control" required></textarea>
                </div>
                <div class="mb-3">
                    <label for="image" class="form-label label-bold">Image</label>
                    <div class="image-placeholder" @click="() => $refs.imageInput.click()">
                        <span v-if="!imageUrl">Click to upload image</span>
                        <img v-else :src="imageUrl" alt="Selected Image" class="img-fluid" />
                    </div>
                    <input id="image" ref="imageInput" @change="handleImageChange" type="file" class="d-none"
                        accept="image/*" required />
                </div>
                <div class="form-check mb-3">
                    <input id="isPublished" v-model="isPublished" type="checkbox" class="form-check-input" />
                    <label for="isPublished" class="form-check-label label-bold">Is Published</label>
                </div>
                <button type="submit" class="btn btn-primary w-100 py-4">Create Blog</button>
            </form>
        </div>
    </div>
</template>

<style scoped>
.title-bold {
    font-weight: bold;
}

.label-bold {
    font-weight: bold;
}

.image-placeholder {
    width: 100%;
    height: auto;
    border: 2px dashed #ccc;
    display: flex;
    justify-content: center;
    align-items: center;
    cursor: pointer;
    text-align: center;
    aspect-ratio: 16 / 9;
}

.image-placeholder img {
    max-height: 100%;
    max-width: 100%;
}
</style>