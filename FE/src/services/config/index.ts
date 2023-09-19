import { fetchBaseQuery } from "@reduxjs/toolkit/dist/query/react";

const config = {
  baseQuery: fetchBaseQuery({
    baseUrl: process.env.REACT_APP_API_SERVER,
    prepareHeaders: (headers) => {
      const token = localStorage.getItem("token");
      if (token) {
        headers.set("authorization", `Bearer ${token}`);
        return headers;
      }
    },
  }),
};

export default config;
