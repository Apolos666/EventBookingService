import { getUser } from "@/services/states/getUser";
import axios from "axios";

export const axiosPrivate = axios.create(
    {
        baseURL: import.meta.env.VITE_API_BASE_URL,
        withCredentials: true
    }
);

axiosPrivate.interceptors.request.use(
    config => {
        if (!config.headers.Authorization) {
            config.headers.Authorization = `Bearer ${getUser()?.access_token}`;
        }

        return config;
    },
    error => {
        return Promise.reject(error);
    }
)