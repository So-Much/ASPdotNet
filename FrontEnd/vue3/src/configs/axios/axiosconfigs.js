import axiosBase from 'axios';

const axios = axiosBase.create({
    baseURL: process.env.VUE_APP_SERVER_URL || 'http://localhost:8080',
})

export {
    axios
};