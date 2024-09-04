import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import Toast from 'vue-toastification';

import "vue-toastification/dist/index.css";

const options = {
    // You can set your default vue-toastification options here
};

createApp(App)
    .use(store)
    .use(Toast, options)
    .use(router)
    .mount('#app')
