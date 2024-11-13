import axios from "axios";

const API_URL = "https://localhost:7274/api";
axios.defaults.baseURL = API_URL;

const partidaApi = axios.create({
  baseURL: API_URL,
  headers: {
    "Content-Type": "application/json",
  },
});

export { partidaApi, API_URL };
