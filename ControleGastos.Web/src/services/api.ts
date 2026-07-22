import axios from "axios";

export const api = axios.create({
  baseURL: "http://localhost:5214/api", // Altere para a URL da sua API .NET
});
