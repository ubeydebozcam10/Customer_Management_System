// İstek atarken her seferinde http localhost .... yazmadan istek atmamızı sağlar

import axios from 'axios';
export const axiosClient = axios.create({
    baseURL: 'https://localhost:44360/api',
    headers: {
        'Content-Type': 'application/json',
    },
});