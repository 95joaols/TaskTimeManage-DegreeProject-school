import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { WorkItem } from "../../Types/WorkItem";
import { RootState } from "..";
import { CreateWorkItemRequest } from "../../Types/Requests/CreateWorkItemRequest";
import { EditWorkItemRequest } from "../../Types/Requests/EditWorkItemRequest";
import { WorkTime } from "../../Types/WorkTime";
import { CreateWorkTimeRequest } from "../../Types/Requests/CreateWorkTimeRequest";

// Define a service using a base URL and expected endpoints
export const workApi = createApi({
    reducerPath: "workApi",
    tagTypes: ["WorkItem"],
    baseQuery: fetchBaseQuery({
        baseUrl: "/api/",

        prepareHeaders: (headers, { getState }) => {
            // By default, if we have a token in the store, let's use that for authenticated requests
            const token = (getState() as RootState).auth.token;
            if (token) {
                headers.set("Authorization", `Bearer ${token}`);
            }
            return headers;
        },
    }),
    endpoints: (builder) => ({
        getWorkItemForUser: builder.query<WorkItem[], string>({
            query: (body) => ({
                url: "WorkItem/UserId/" + body,
                method: "GET",
                responseHandler: (response) => {
                    if (response.status === 204) {
                        return response.text();
                    }
                    if (response.ok) {
                        return response.json();
                    }
                    if (
                        !response.ok &&
                        (response.status === 502 || response.status === 504 || response.status === 404)
                    ) {
                        return response.text();
                    }
                    return response.json();
                },
            }),
            providesTags: ["WorkItem"],
        }),
        getWorkItem: builder.query<WorkItem, string>({
            query: (body) => ({
                url: "WorkItem/" + body,
                method: "GET",
                responseHandler: (response) => {
                    if (response.status === 204) {
                        return response.text();
                    }
                    if (response.ok) {
                        return response.json();
                    }
                    if (
                        !response.ok &&
                        (response.status === 502 || response.status === 504 || response.status === 404)
                    ) {
                        return response.text();
                    }
                    return response.json();
                },
            }),

            providesTags: ["WorkItem"],
        }),
        CreateWorkItem: builder.mutation<WorkItem, CreateWorkItemRequest>({
            query: (body) => ({
                url: "WorkItem/",
                method: "POST",
                body: body,
                responseHandler: (response) => {
                    if (response.status === 204) {
                        return response.text();
                    }
                    if (response.ok) {
                        return response.json();
                    }
                    if (
                        !response.ok &&
                        (response.status === 502 || response.status === 504 || response.status === 404)
                    ) {
                        return response.text();
                    }
                    return response.json();
                },
            }),
            invalidatesTags: ["WorkItem"],
        }),
        EditWorkItem: builder.mutation<WorkItem, { WorkItemId: string; editWorkItemRequest: EditWorkItemRequest }>({
            query: (body) => ({
                url: "WorkItem/" + body.WorkItemId,
                method: "PUT",
                body: body.editWorkItemRequest,
                responseHandler: (response) => {
                    if (response.status === 204) {
                        return response.text();
                    }
                    if (response.ok) {
                        return response.json();
                    }
                    if (
                        !response.ok &&
                        (response.status === 502 || response.status === 504 || response.status === 404)
                    ) {
                        return response.text();
                    }
                    return response.json();
                },
            }),
            invalidatesTags: ["WorkItem"],
        }),
        DeleteWorkItem: builder.mutation<boolean, string>({
            query: (body) => ({
                url: "WorkItem/" + body,
                method: "DELETE",
                responseHandler: (response) => {
                    if (response.status === 204) {
                        return response.text();
                    }
                    if (response.ok) {
                        return response.json();
                    }
                    if (
                        !response.ok &&
                        (response.status === 502 || response.status === 504 || response.status === 404)
                    ) {
                        return response.text();
                    }
                    return response.json();
                },
            }),
            invalidatesTags: ["WorkItem"],
        }),
        CreateWorkTime: builder.mutation<
            WorkTime,
            { WorkItemId: string; createWorkTimeRequest: CreateWorkTimeRequest }
        >({
            query: (body) => ({
                url: `WorkItem/${body.WorkItemId}/WorkTime`,
                method: "POST",
                body: body.createWorkTimeRequest,
                responseHandler: (response) => {
                    if (response.status === 204) {
                        return response.text();
                    }
                    if (response.ok) {
                        return response.json();
                    }
                    if (
                        !response.ok &&
                        (response.status === 502 || response.status === 504 || response.status === 404)
                    ) {
                        return response.text();
                    }
                    return response.json();
                },
            }),
            invalidatesTags: ["WorkItem"],
        }),
        DeleteWorkTime: builder.mutation<boolean, { WorkItemId: string; workTimeId: string }>({
            query: (body) => ({
                url: `WorkItem/${body.WorkItemId}/WorkTime/${body.workTimeId}`,
                method: "DELETE",
                responseHandler: (response) => {
                    if (response.status === 204) {
                        return response.text();
                    }
                    if (response.ok) {
                        return response.json();
                    }
                    if (
                        !response.ok &&
                        (response.status === 502 || response.status === 504 || response.status === 404)
                    ) {
                        return response.text();
                    }
                    return response.json();
                },
            }),
            invalidatesTags: ["WorkItem"],
        }),
    }),
});

// Export hooks for usage in functional components, which are
// auto-generated based on the defined endpoints
export const {
    useGetWorkItemForUserQuery,
    useLazyGetWorkItemQuery,
    useCreateWorkItemMutation,
    useEditWorkItemMutation,
    useCreateWorkTimeMutation,
    useDeleteWorkTimeMutation,
    useDeleteWorkItemMutation,
} = workApi;
