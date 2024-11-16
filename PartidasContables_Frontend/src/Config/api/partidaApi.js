import axios from "axios";

const API_URL = "https://localhost:7274/api";
axios.defaults.baseURL = API_URL;

const setAuthToken = () => {
  const auth = getAuth();
  if (auth) {
    axios.defaults.headers.common["Authorization"] = `Bearer ${auth.token}`;
  } else {
    delete axios.defaults.headers.common["Authorization"];
  }
};

const getAuth = () => {
  const lsToken = localStorage.getItem("token");
  const lsRefreshToken = localStorage.getItem("refreshToken");

  if (lsToken && lsRefreshToken) {
    return { token: lsToken, refreshToken: lsRefreshToken };
  }
  return null;
};

setAuthToken();

const partidaApi = axios.create({
  baseURL: API_URL,
  headers: {
    "Content-Type": "application/json",
  },
});

export { partidaApi, API_URL, setAuthToken };
