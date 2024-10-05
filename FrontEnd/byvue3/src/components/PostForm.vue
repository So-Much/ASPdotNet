<script setup>
import {
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
import { useRouter } from 'vue-router';
import { useToast } from 'vue-toastification';

onMounted(() => {
    injectMainJS();
});
onUnmounted(() => {
    removeMainJS();
});

const router = useRouter();
const toast = useToast();

const editorData = ref('');
watch(editorData,() => {
    document.getElementById('preview').innerHTML = editorData.value;
})
const createPost = e => {
    e.preventDefault();
    console.log(JSON.stringify(editorData.value));
    axios.post('/api/post', {
        content: editorData.value
    }, {
        headers: {
            "Content-Type": "application/x-www-form-urlencoded",
            Authorization: 'Bearer ' + localStorage.getItem('token')
        }
    })
        .then(res => {
            console.log(res);
        })
        .catch(err => {
            if (err.code === 404 || err.code === 401) {
                console.log('Unauthorized');
                toast.error('You are haven\'t been logged in yet!');
                router.push('/login');
            }
        });
}

// const APIPostRoute = process.env.VUE_APP_SERVER_URL + '/api/post/metadata';
</script>

<template>
    <div class="createpost">
        <div>
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
                    'horizontalLine', 'bulletedList', 'numberedList', 'todoList','|',
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
            }
                " />
        </div>
        <button type="submit" @click="createPost">
            Submit
        </button>
        <!-- <input type="file" name="" id="" accept="image/*"> -->
    </div>
    <h1>Form data is get it:</h1>
    <div id="preview" class="ck-content">
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
</style>
