import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { userDto } from "../../Types/UserDto";

// Define a service using a base URL and expected endpoints
export const authApi = createApi({
  reducerPath: "authApi",
  baseQuery: fetchBaseQuery({ baseUrl: "https://localhost:1337/api/user" }),
  endpoints: (builder) => ({
    login: builder.mutation<string, userDto>({
      query: (body) => {
        return {
          url: "/Login",
          method: "post",
          body,
          responseHandler: (response) => {
            return response.text();
          },
        };
      },
    }),
    createUser: builder.mutation<boolean, userDto>({
      query: (body) => {
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
