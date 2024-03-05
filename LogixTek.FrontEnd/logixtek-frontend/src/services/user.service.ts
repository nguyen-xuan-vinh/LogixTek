
import axios from 'axios';

const API_URL = "https://localhost:7278/api/User/";

export const register = (name: string, username: string, password: string) => {
    return axios.post(API_URL + "register", {
        name,
        username,
        password,
    });
};

export const login = (username: string, password: string) => {
    return axios
        .post(API_URL + "login", {
            username,
            password,
        })
        .then((response: any) => {
            if (response.data.token) {
                localStorage.setItem("user", JSON.stringify(response.data));
            }

            return response.data;
        });
};

export const logout = () => {
    localStorage.removeItem("user");
};

export const getCurrentUser = () => {
    const userStr = localStorage.getItem("user");
    if (userStr) return JSON.parse(userStr);

    return null;
};