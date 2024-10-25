<script setup>
import { axios } from '@/configs';
import { ref, useTemplateRef } from 'vue';
import { useLoading } from 'vue-loading-overlay';
import { useRouter } from 'vue-router';
import { useToast } from 'vue-toastification';

const $loading = useLoading();
const toast = useToast();
const router = useRouter();


const name = ref('Olivia Gribben');
const defaultAvatar = () => ref('https://i.pinimg.com/736x/8c/4c/cb/8c4ccbc7c4e16aa94b4a61ef05f59af4.jpg');
const avatar = ref('');
const avatarInputRef = useTemplateRef('avatar');
const avatarPreview = useTemplateRef('avatarPreview');

const onFileUploaded = (e) => {
    const file = e.target.files[0];
    const uploading = $loading.show();
    axios.post('/api/user/uploadavatar',
        {
            avatar: file
        }, 
        {
            headers: {
                "Content-Type": 'multipart/form-data',
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }
    ).then(res => {
        uploading.hide();
        avatar.value = res.data;
        console.log("res.data: ", typeof(res.data))
        console.log("avatar.value catch data: ", avatar.value)
        console.log("type avatar.value: ", typeof(avatar.value))
        toast.success("Avatar uploaded successfully");
    })
    .catch(e => {
        uploading.hide();
        if(e.response.status == 401) {
            toast.error("You are haven't been logged in yet!");
            router.push('/login');
        }
        console.log(e)
    })
};

const changeStatus = (e) => {
    const statusEle = e.target;
    if(statusEle.classList.contains('avatar__status--active')) {
        statusEle.classList.remove('avatar__status--active');
        statusEle.classList.add('avatar__status--busy');
    } else {
        statusEle.classList.remove('avatar__status--busy');
        statusEle.classList.add('avatar__status--active');
    }
};
</script>

<template>
    <input type="file" accept="image/*" ref="avatar" class="hidden" @change="onFileUploaded">
    <div class="avatar avatar--lg">
        <figure class="avatar__figure" role="img" :aria-label="name" @click="avatarInputRef.click()">

            <svg class="avatar__placeholder" aria-hidden="true" viewBox="0 0 20 20" stroke-linecap="round"
                stroke-linejoin="round">
                <circle cx="10" cy="6" r="2.5" stroke="currentColor" />
                <path d="M10,10.5a4.487,4.487,0,0,0-4.471,4.21L5.5,15.5h9l-.029-.79A4.487,4.487,0,0,0,10,10.5Z"
                    stroke="currentColor" />
            </svg>
            <img class="avatar__img" :src="console.log("avatar.value in src: ",avatar.value)" ref="avatarPreview" :alt="name" :title="name" v-if="avatar.value">
            <img class="avatar__img" :src="defaultAvatar().value" ref="avatarPreview" :alt="name" :title="name" v-else>

        </figure>

        <span role="status" class="avatar__status avatar__status--active" aria-label="Active" @click="changeStatus"></span>
    </div>
</template>

<style scoped>
/* -------------------------------- 

File#: _1_avatar
Title: Avatar
Descr: A user profile image component
Usage: codyhouse.co/license

-------------------------------- */
.hidden {
    display: none;
}

/* reset */
*,
*::after,
*::before {
    box-sizing: border-box;
}

* {
    font: inherit;
    margin: 0;
    padding: 0;
    border: 0;
}

html {
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
}

body {
    background-color: hsl(0, 0%, 100%);
    font-family: system-ui, sans-serif;
    color: hsl(230, 7%, 23%);
    font-size: 1.125rem;
    /* 18px */
    line-height: 1.4;
}

h1,
h2,
h3,
h4 {
    line-height: 1.2;
    color: hsl(230, 13%, 9%);
    font-weight: 700;
}

h1 {
    font-size: 2.5rem;
    /* 40px */
}

h2 {
    font-size: 2.125rem;
    /* 34px */
}

h3 {
    font-size: 1.75rem;
    /* 28px */
}

h4 {
    font-size: 1.375rem;
    /* 22px */
}

ol,
ul,
menu {
    list-style: none;
}

button,
input,
textarea,
select {
    background-color: transparent;
    border-radius: 0;
    color: inherit;
    line-height: inherit;
    -webkit-appearance: none;
    appearance: none;
}

textarea {
    resize: vertical;
    overflow: auto;
    vertical-align: top;
}

a {
    color: hsl(250, 84%, 54%);
}

table {
    border-collapse: collapse;
    border-spacing: 0;
}

img,
video,
svg {
    display: block;
    max-width: 100%;
}

/* -------------------------------- 

Component 

-------------------------------- */

#avatar {
    display: none;
}

.avatar {
    position: relative;
    display: block;
    width: 1em;
    height: 1em;
    font-size: 1em;
    /* avatar size */
}

.avatar--sm {
    font-size: 4rem;
}

.avatar--md {
    font-size: 6rem;
}

.avatar--lg {
    font-size: 8rem;
}

.avatar__figure {
    width: 1em;
    height: 0;
    padding-bottom: 1em;
    border-radius: 50%;
    /* avatar border-radius */
    overflow: hidden;
}

.avatar__figure:hover {
    cursor: pointer;
}


.avatar__img,
.avatar__placeholder {
    position: absolute;
    display: block;
    top: 0;
    left: 0;
    width: 1em;
    height: 1em;
    border-radius: inherit;
}

.avatar__placeholder {
    background-color: hsl(240, 4%, 95%);
    color: hsl(225, 4%, 47%);
    /* icon color */
    fill: transparent;
}

.avatar--btn {
    background-color: transparent;
    padding: 0;
    border: 0;
    border-radius: 0;
    color: inherit;
    line-height: inherit;
    -webkit-appearance: none;
    appearance: none;
    cursor: pointer;
}

/* status */
.avatar__status {
    position: absolute;
    bottom: 0;
    right: 0;
    display: inline-block;
    width: 0.2em;
    height: 0.2em;
    border-radius: 50%;
    box-shadow: 0 0 0 2px hsl(0, 0%, 100%);
    background-color: hsl(225, 4%, 47%);
}

.avatar__status:hover {
    cursor: pointer;
}

.avatar__status--active {
    background-color: hsl(170, 78%, 36%);
}

.avatar__status--busy {
    background-color: hsl(342, 89%, 48%);
}

.avatar__initials,
.avatar__users-counter {
    position: absolute;
    top: 0;
    left: 0;
    width: 1em;
    height: 1em;
    border-radius: inherit;
    background-color: hsl(240, 4%, 85%);
    color: hsl(230, 13%, 9%);
    display: flex;
    justify-content: center;
    align-items: center;
}

.avatar__initials {
    /* initials - show letters if you don't have img */
    text-align: center;
}

.avatar__initials span {
    font-size: 0.5em;
}

/* tot number of users in a group of avatars */
.avatar__users-counter span {
    font-size: 0.42em;
}

/* group */
.avatar-group {
    display: flex;
    /* 👇 optional -> edit the size of all the avatars in the group */
    /* font-size: 1.75em; */
}

.avatar-group .avatar {
    margin-left: -0.1em;
}

.avatar-group .avatar__figure {
    box-shadow: 0 0 0 3px hsl(0, 0%, 100%);
}
</style>