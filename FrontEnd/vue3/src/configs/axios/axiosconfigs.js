import axiosBase from 'axios';

const baseURL = process.env.VUE_APP_SERVER_URL || 'http://localhost:8080';
console.log("baseURL:",baseURL);

const axios = axiosBase.create({
    baseURL: process.env.VUE_APP_SERVER_URL || 'http://localhost:8080',
})

export {
    axios
};