<script setup>
import {
    onBeforeMount,
    ref,
    watch
} from 'vue';
import {
    ClassicEditor, Bold, Essentials, Italic,
    Mention, Paragraph, Undo, Underline, Image, ImageInsert,
    // SimpleUploadAdapter,
    ImageResize,
    Base64UploadAdapter,
    Alignment, Table, TableToolbar, TableColumnResize,
    Strikethrough, Subscript, Superscript, RemoveFormat,
    MediaEmbed, List, TodoList, Font, Heading,
    TableProperties,
    TableCellProperties,
    ImageStyle,
    ImageToolbar,
    ImageCaption,
    LinkImage,
    HorizontalLine,
    Indent,
    IndentBlock,
} from 'ckeditor5';
import 'ckeditor5/ckeditor5.css';

import { injectMainJS, removeMainJS } from '@/utils/asynchronous';
import { onMounted, onUnmounted } from 'vue';
import { axios } from '@/configs';
import { useRoute, useRouter } from 'vue-router';
import { useToast } from 'vue-toastification';
import { useLoading } from 'vue-loading-overlay';

onMounted(() => {
    injectMainJS();
});
onUnmounted(() => {
    removeMainJS();
});

const router = useRouter();
const toast = useToast();
const $loading = useLoading();
const route = useRoute();

const postTitle = ref('');
const isPushlished = ref(true);
const blogId = route.params.blogId;
const blog = ref();

const editorData = ref('');
watch(editorData, () => {
    document.getElementById('preview').innerHTML = editorData.value;
})

onBeforeMount(() => {
    // console.log(route.params)
    axios.get(`/api/blog/${blogId}`, {
        headers: {
            Authorization: "Bearer " + localStorage.getItem('token')
        }
    }).then(res => {
        blog.value = res.data;
        console.log("🚀 ~ onBeforeMount ~ blog:", blog)
        console.log(blog.value.title)
    }).catch(err => {
        if (err.status == 401) {
            toast.error('Token is expired. Please login again');
            router.push('/login');
        }
        console.log(err);
    })
})

const createPost = () => {
    console.log("🚀 ~ createPost ~ postTitle.value:", postTitle.value);
    console.log("🚀 ~ createPost ~ (JSON.stringify(editorData.value):", (JSON.stringify(editorData.value)));
    console.log("🚀 ~ createPost ~ blogId:", blogId);
    if(postTitle.value === '' || editorData.value === '') {
        toast.error('Title and Content are required!');
        return;
    }
    const loader = $loading.show();
    axios.post('/api/post', {
        blogId: blogId,
        title: postTitle.value,
        content: editorData.value,
        isPublished: isPushlished.value
    }, {
        headers: {
            Authorization: 'Bearer ' + localStorage.getItem('token'),
            "Content-Type": "application/x-www-form-urlencoded"
        }
    })
        .then(res => {
            loader.hide();
            router.go(-1);
            console.log(res);
        })
        .catch(err => {
            loader.hide();
            if (err.code === 404 || err.code === 401) {
                console.log('Unauthorized');
                toast.error('You are haven\'t been logged in yet!');
                router.push('/login');
            }
            console.log(err)
        });
}


</script>

<template>
    <div class="createpost">
        <h2 class="text-center font-weight-bold text-uppercase">Create Post</h2>
        <div class="form-group">
            <label class="form-check-label" for="posttitle">
                <h3><b>TITLE:</b></h3>
            </label>
            <input id="posttitle" type="text" class="form-control text-center fz-title" v-model="postTitle">
        </div>
        <div class="editor-preview-container">
            <div class="editor-container">
                <Ckeditor v-model="editorData" :editor="ClassicEditor" :config="{
                    plugins: [
                        Bold, Essentials, Italic, Strikethrough, Subscript, Superscript,
                        Mention, Paragraph, Undo, Underline,
                        Image, ImageInsert,
                        Alignment, ImageStyle,
                        Table, TableToolbar, TableColumnResize, TableProperties, TableCellProperties,
                        RemoveFormat, MediaEmbed, List, TodoList,
                        Font, Heading, HorizontalLine,
                        Base64UploadAdapter, ImageToolbar, ImageCaption, ImageResize, LinkImage,
                        Indent, IndentBlock
                        // SimpleUploadAdapter,

                    ],
                    toolbar: [
                        'undo', 'redo', '|',
                        'heading', '|',
                        'fontFamily', 'fontSize', 'fontColor', 'fontBackgroundColor', '|',
                        'bold', 'italic', 'underline', 'strikethrough', '|',
                        'subscript', 'superscript', '|',
                        'removeFormat', '|',
                        'insertImage', 'mediaEmbed', '|',
                        'alignment',
                        'insertTable', 'tableProperties', 'tableCellProperties', '|',
                        'horizontalLine', 'bulletedList', 'numberedList', 'todoList', '|',
                        'outdent', 'indent'
                    ],
                    table: {
                        contentToolbar: ['tableColumn', 'tableRow', 'mergeTableCells']
                    },
                    list: {
                        properties: {
                            styles: true,
                            startIndex: true,
                        }
                    },
                    image: {
                        toolbar: [
                            'imageStyle:block',
                            'imageStyle:side',
                            'imageStyle:inline',
                            'imageStyle:wraptext',
                            '|',
                            'toggleImageCaption',
                            'imageTextAlternative',
                            '|',
                            'linkImage'
                        ]
                    },
                    licenseKey: '<YOUR_LICENSE_KEY>',
                    // Other configuration options...
                }" />
                <div class="pa-2 my-2 mx-3">
                    <input type="radio" name="publish" id="Publish" v-model="isPushlished" :value="true">
                    <label class="mr-3 pl-1" for="Publish">Publish</label>
                    <input type="radio" name="publish" id="Private" v-model="isPushlished" :value="false">
                    <label class="mr-3 pl-1" for="Private">Private</label>
                </div>
                <div>
                    <button type="submit" class="btn btn-primary px-4" @click.prevent="createPost">
                        Create Post
                    </button>
                </div>
            </div>
            <div class="preview-container">
                <h1>Review:</h1>
                <div id="preview" class="ck-content"></div>
            </div>
        </div>
    </div>
</template>

<style scoped>
.createpost {
    margin: 20px;
    padding: 20px;
    border: 1px solid #ccc;
    border-radius: 5px;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    background: #fff;
    font-family: 'Arial', sans-serif;
    font-size: 16px;
    line-height: 1.5;
    color: #333;
}

.editor-preview-container {
    display: flex;
    justify-content: space-between;
}

.editor-container,
.preview-container {
    width: 48%;
}

.preview-container {
    padding-left: 20px;
}

.ck-content {
    border: 1px solid #ccc;
    padding: 10px;
    border-radius: 5px;
    background: #f9f9f9;
}
.fz-title {
    font-size: 2rem;
    padding-top: 8px;
    padding-bottom: 8px;;
}
</style>
