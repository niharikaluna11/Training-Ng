import axios from "axios";

function requestInterceptor(config) {
    const token = sessionStorage.getItem("token");
    const username = sessionStorage.getItem("given_name");

    if (token) {
        config.headers['Authorization'] = 'Bearer ' + token;
    }

    // Optionally log or use the given_name if needed
    if (username) {
        console.log(`Request made by: ${username}`);
    }

    return config;
}

axios.interceptors.request.use(requestInterceptor);
export default axios;
