// src/scripts/myAxiosInterceptor.js
import axios from "axios";

function requestInterceptor(config) {
    const token = sessionStorage.getItem('token');
    if (token) {
        config.headers['Authorization'] = 'Bearer ' + token;
    }
    return config;
}

axios.interceptors.request.use(requestInterceptor);
export default axios;
