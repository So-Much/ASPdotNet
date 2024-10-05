import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import Toast from 'vue-toastification';

/* import font awesome icon component */
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import { CkeditorPlugin } from '@ckeditor/ckeditor5-vue';
import "vue-toastification/dist/index.css";
import { LoadingPlugin } from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/css/index.css';
const toastifyOptions = {
    // You can set your default vue-toastification options here
};

const app = createApp(App)
    .use(CkeditorPlugin)
    .component('font-awesome-icon', FontAwesomeIcon)
    .use(LoadingPlugin)
    .use(store)
    .use(Toast, toastifyOptions)
    .use(router)
    .mount('#app');

export default app;