<script setup>
import { ref } from 'vue';
import {
    ClassicEditor, Bold, Essentials, Italic,
    Mention, Paragraph, Undo, Underline, Image, ImageInsert,
    SimpleUploadAdapter, ImageResize,
    // Base64UploadAdapter,
    Alignment, Table, TableToolbar, TableColumnResize,
    Strikethrough, Subscript, Superscript, RemoveFormat,
    MediaEmbed, List, TodoList, Font, Heading,
    TableProperties,
    TableCellProperties,
    ImageStyle,
} from 'ckeditor5';
import 'ckeditor5/ckeditor5.css';

import { injectMainJS, removeMainJS } from '@/utils/asynchronous';
import { onMounted, onUnmounted } from 'vue';

onMounted(() => {
    injectMainJS();
});
onUnmounted(() => {
    removeMainJS();
});

const editorData = ref('');

const APIPostRoute = process.env.VUE_APP_SERVER_URL + '/api/post/upload';
</script>

<template>
    <div class="editor">
        <Ckeditor v-model="editorData" :editor="ClassicEditor" :config="{
            plugins: [
                Bold, Essentials, Italic, Strikethrough, Subscript, Superscript,
                Mention, Paragraph, Undo, Underline,
                Image, ImageInsert,
                Alignment, ImageStyle,
                Table, TableToolbar, TableColumnResize, TableProperties, TableCellProperties,
                RemoveFormat, MediaEmbed, List, TodoList,
                Font, Heading,
                // Base64UploadAdapter,
                SimpleUploadAdapter, Image, ImageResize
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
                'bulletedList', 'numberedList', 'todoList',
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
            simpleUpload: {
                // The URL that the images are uploaded to.
                uploadUrl: APIPostRoute,

                // Enable the XMLHttpRequest.withCredentials property.
                withCredentials: true,
                // Headers sent along with the XMLHttpRequest to the upload server.
                headers: {
                    'X-CSRF-TOKEN': 'CSRF-Token',
                    Authorization: 'Bearer <JSON Web Token>'
                }
            },
            
            licenseKey: '<YOUR_LICENSE_KEY>',
            // Other configuration options...
        }
            " />
    </div>
</template>

<style scoped>
.editor {
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