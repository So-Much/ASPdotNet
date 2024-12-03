<script setup>
import { axios } from '@/configs';
import { injectMainJS, removeMainJS } from '@/utils/asynchronous';
import 'ckeditor5/ckeditor5.css';
import {
    Alignment,
    Base64UploadAdapter,
    Bold,
    ClassicEditor,
    Essentials,
    Font,
    Heading,
    HorizontalLine,
    Image,
    ImageCaption,
    ImageInsert,
    ImageResize,
    ImageStyle,
    ImageToolbar,
    Indent,
    IndentBlock,
    Italic,
    LinkImage,
    List,
    MediaEmbed,
    Mention,
    Paragraph,
    RemoveFormat,
    Strikethrough,
    Subscript,
    Superscript,
    Table,
    TableCellProperties,
    TableColumnResize,
    TableProperties,
    TableToolbar,
    TodoList,
    Underline,
    Undo
} from 'ckeditor5';
import { onBeforeMount, onMounted, onUnmounted, ref } from 'vue';
import {
    useRoute,
    // useRouter
} from 'vue-router';
import router from '@/router';
import { useToast } from 'vue-toastification';
import { useLoading } from 'vue-loading-overlay';

onMounted(() => {
    injectMainJS();
})

onUnmounted(() => {
    removeMainJS();
})

// const router = useRouter();
const route = useRoute();
const toast = useToast();
const $loading = useLoading();

const postId = route.query.id;
const postTitle = route.params.title;

const content = ref('');
const isPublished = ref(true);
const showDeleteModal = ref(false);

onBeforeMount(() => {
    console.log("🚀 ~ onBeforeMount ~ postId:", postId)
    console.log("🚀 ~ onBeforeMount ~ postTitle:", postTitle)
    const loader = $loading.show();
    axios.get('/api/User/isloggedin', {
      headers: {
        Authorization: 'Bearer ' + localStorage.getItem('token')
      }
    }).then(() => {
        axios.get(`/api/post/${postId}`)
        .then((res) => {
            loader.hide();
            content.value = res.data.content;
            isPublished.value = res.data.isPublished;
            console.log(res)
        })
        .catch(err => {
            loader.hide();
            router.go(-1);
            toast.error("Post not found");
            console.log(err);
        })
    }).catch(() => {
        loader.hide();
        toast.error("Token is expired. Please login again");
        router.push('/login');
    })
    
})

const saveChanges = () => {
    const loader = $loading.show();
    axios.put(`/api/post/${postId}`, {
        content: content.value,
        isPublished: isPublished.value
    }, {
        headers: {
            Authorization: "Bearer " + localStorage.getItem('token'),
            "Content-Type": "application/x-www-form-urlencoded"
        }
    }).then((res) => {
        loader.hide();
        toast.success('Post updated successfully');
        router.go(-1);
        console.log(res);
    }).catch((err) => {
        router.push('/login');
        toast.error('token is expired. Please login again');
        console.log(err)
    });
};

const cancelChanges = () => {
    // Implement cancel changes logic here
    router.go(-1);
    console.log("Changes canceled");
};

const deletePost = () => {
    const loader = $loading.show();
    axios.delete(`/api/post/${postId}`, {
        headers: {
            Authorization: "Bearer " + localStorage.getItem('token')
        }
    }).then((res) => {
        loader.hide();
        toast.success('Post deleted successfully');
        router.go(-1);
        console.log(res);
    }).catch((err) => {
        loader.hide();
        toast.error('Failed to delete post');
        console.log(err);
    });
};

</script>

<template>
    <div class="editpost">
        <h2 class="text-center font-weight-bold text-uppercase">Edit Post</h2>
        <div class="form-group">
            <label class="form-check-label" for="posttitle">
                <h3><b>TITLE:</b></h3>
            </label>
            <input id="posttitle" type="text" class="form-control text-center fz-title" v-model="postTitle" disabled>
        </div>
        <div class="editor-preview-container">
            <div class="editor-container">
                <Ckeditor v-model="content" :editor="ClassicEditor" :config="{
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
                    <input type="radio" name="publish" id="Publish" v-model="isPublished" :value="true">
                    <label class="mr-3 pl-1" for="Publish">Publish</label>
                    <input type="radio" name="publish" id="Private" v-model="isPublished" :value="false">
                    <label class="mr-3 pl-1" for="Private">Private</label>
                </div>
                <div class="pa-2 my-2 mx-3">
                    <button type="button" class="btn btn-primary px-4 mr-3" @click.prevent="saveChanges">
                        Save Changes
                    </button>
                    <button type="button" class="btn btn-danger px-4 mr-3" @click.prevent="showDeleteModal = true">
                        Delete
                    </button>
                    <button type="button" class="btn btn-secondary px-4" @click.prevent="cancelChanges">
                        Cancel
                    </button>
                </div>
            </div>
            <div class="preview-container">
                <h1>Review:</h1>
                <div id="preview" class="ck-content" v-html="content"></div>
            </div>
        </div>
    </div>
    <!-- Delete Confirmation Modal -->
    <div v-if="showDeleteModal" class="modal fade show" id="deleteModal" tabindex="-1" aria-labelledby="deleteModalLabel"
        aria-hidden="true" style="display: block;">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="deleteModalLabel">Confirm Delete</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"
                        @click="showDeleteModal = false"></button>
                </div>
                <div class="modal-body">
                    <p>Are you sure you want to delete this post?</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" @click="deletePost">Yes, Delete</button>
                    <button type="button" class="btn btn-secondary" @click="showDeleteModal = false">Cancel</button>
                </div>
            </div>
        </div>
    </div>
    <!-- End of Delete Confirmation Modal -->
</template>

<style scoped>
.editpost {
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
    padding-bottom: 8px;
}
</style>