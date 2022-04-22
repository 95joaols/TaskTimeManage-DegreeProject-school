import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

// Define a service using a base URL and expected endpoints
export const authApi = createApi({
  reducerPath: "authApi",
  baseQuery: fetchBaseQuery({ baseUrl: "https://Api:433/api/user" }),
  endpoints: (builder) => ({
    login: builder.mutation({
      query: (body: { name: string; password: string }) => {
        return {
          url: "/Login",
          method: "post",
          body,
        };
      },
    }),
    createUser: builder.mutation({
      query: (body: { name: string; password: string }) => {
        return {
          url: "/CreateUser",
          method: "post",
          body,
        };
      },
    }),
  }),
});

// Export hooks for usage in functional components, which are
// auto-generated based on the defined endpoints
export const {
  useLoginMutation,
  useCreateUserMutation
} = authApi;
