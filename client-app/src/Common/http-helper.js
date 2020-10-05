import axios from "axios";
import { Configs } from "./constants";

const instance = axios.create({
  baseURL: Configs.URL,
  headers: {
    "Content-Type": "application/json",
    Accept: "application/json"
  },
});

export default instance;
