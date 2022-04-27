import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { WorkItem } from "../../Types/WorkItem";
import { RootState } from "..";

// Define a service using a base URL and expected endpoints
export const workApi = createApi({
    reducerPath: "workApi",
    baseQuery: fetchBaseQuery({
        baseUrl: "https://localhost:1337/api/",

        prepareHeaders: (headers, { getState }) => {
            // By default, if we have a token in the store, let's use that for authenticated requests
            const token = (getState() as RootState).auth.token;
            if (token) {
                headers.set("Authorization", `Bearer ${token}`);
            }
            return headers;
        }
    }),
    endpoints: (builder) => ({
        getWorkItemForUser: builder.query<WorkItem[], string>({
            query: (body) => ({
                url: "WorkItem/getWorkItemForUser/" + body,
                method: "GET",

            })
        }),
        CreateWorkItem: builder.mutation<WorkItem, WorkItem>({
            query: (body) => ({
                url: "WorkItem",
                method: "POST",
                body: body,
            })
        })
    }),
});

// Export hooks for usage in functional components, which are
// auto-generated based on the defined endpoints
export const {
    useGetWorkItemForUserQuery,
    useCreateWorkItemMutation
} = workApi;
