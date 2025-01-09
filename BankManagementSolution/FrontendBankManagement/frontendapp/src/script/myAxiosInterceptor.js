import axios from "axios";

function requestInterceptor(config) {
    const token = sessionStorage.getItem("token");

    if (token) {
        config.headers['Authorization'] = 'Bearer ' + token;
    }

    return config;
}

axios.interceptors.request.use(requestInterceptor);

function setToken(token, name, role) {
    sessionStorage.setItem("token", token);

    if (name) {
        sessionStorage.setItem("name", name);
    }
    if (role) {
        sessionStorage.setItem("role", role);
    }
}

export { setToken };
export default axios;
