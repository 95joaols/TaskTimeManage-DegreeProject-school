import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { RootState } from "../store";
import { LoginDto } from "../../Entity/LoginDto"

export const authApi = createApi({
    baseQuery: fetchBaseQuery({
        baseUrl: "https://API:433/api",
        prepareHeaders: (headers, { getState }) => {
            // By default, if we have a token in the store, let's use that for authenticated requests
            const token = (getState() as RootState).auth.token;
            if (token) {
                headers.set("authentication", `Bearer ${token}`);
            }
            return headers;
        }
    }),
    endpoints: (builder) => ({
        login: builder.mutation<String, LoginDto>({
            query: (credentials) => ({
                url: "Login",
                method: "POST",
                body: credentials
            })
        }),
        protected: builder.mutation({
            query: () => "protected"
        })
    })
});

export const { useLoginMutation, useProtectedMutation } = authApi;
