import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { UserRequest } from "../../Types/Requests/UserRequest";

// Define a service using a base URL and expected endpoints
export const authApi = createApi({
    reducerPath: "authApi",
    baseQuery: fetchBaseQuery({ baseUrl: "/api/Authentication/" }),
    endpoints: (builder) => ({
        login: builder.mutation<string, UserRequest>({
            query: (body) => {
                return {
                    url: "Login",
                    method: "post",
                    body,
                    responseHandler: (response) => {
                        return response.text();
                    },
                };
            },
        }),
        createUser: builder.mutation<boolean, UserRequest>({
            query: (body) => {
                return {
                    url: "CreateUser",
                    method: "post",
                    body,
                    responseHandler: (response) => {
                        return response.json();
                    },
                };
            },
        }),
    }),
});

// Export hooks for usage in functional components, which are
// auto-generated based on the defined endpoints
export const { useLoginMutation, useCreateUserMutation } = authApi;
