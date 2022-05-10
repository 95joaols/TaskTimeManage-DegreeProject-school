import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { WorkItem } from "../../Types/WorkItem";
import { RootState } from "..";
import { WorkTime } from "../../Types/WorkTime";

// Define a service using a base URL and expected endpoints
export const workApi = createApi({
    reducerPath: "workApi",
    tagTypes: ["WorkItem"],
    baseQuery: fetchBaseQuery({
        baseUrl: "https://localhost/api/WorkItem",

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
                url: "/WorkItemForUser/" + body,
                method: "GET",
            }),
            providesTags: (result) =>
                result
                    ? [
                          ...result.map(({ publicId }) => ({
                              type: "WorkItem" as const,
                              publicId,
                          })),
                          "WorkItem",
                      ]
                    : ["WorkItem"],
        }),
        getWorkItem: builder.query<WorkItem, string>({
            query: (body) => ({
                url: "/WorkItemById/" + body,
                method: "GET",
            }),

            providesTags: (result) =>
                result
                    ? [
                          {
                              type: "WorkItem" as const,
                              id: result.publicId,
                          },
                          "WorkItem",
                      ]
                    : ["WorkItem"],
        }),
        CreateWorkItem: builder.mutation<WorkItem, WorkItem>({
            query: (body) => ({
                url: "/",
                method: "POST",
                body: body,
            }),
            invalidatesTags: () => [{ type: "WorkItem" }],
        }),
        EditWorkTime: builder.mutation<WorkTime, { workTime: WorkTime; TimeItemPublicId: string }>({
            query: (body) => ({
                url: "/WorkTime/" + body.TimeItemPublicId,
                method: "PUT",
                body: body.workTime,
            }),
            invalidatesTags: () => [{ type: "WorkItem" }],
        }),
        // eslint-disable-next-line prettier/prettier
        CreateWorkTime: builder.mutation<WorkTime, { workTime: WorkTime; publicId: string }>({
            query: (body) => ({
                url: "/WorkTime",
                method: "POST",
                body: body,
            }),
            invalidatesTags: (result, error, arg) => [{ type: "WorkItem", id: arg.publicId }],
        }),
        DeleteWorkTime: builder.mutation<boolean, { workTime: WorkTime; TimeItemPublicId: string }>({
            query: (body) => ({
                url: "/WorkTime/" + body.TimeItemPublicId,
                method: "DELETE",
                body: body.workTime,
                responseHandler: (response) => {
                    return response.json();
                },
            }),
            invalidatesTags: () => [{ type: "WorkItem" }],
        }),
        DeleteWorkItem: builder.mutation<boolean, string>({
            query: (body) => ({
                url: "/" + body,
                method: "DELETE",
                responseHandler: (response) => {
                    return response.json();
                },
            }),
            invalidatesTags: () => [{ type: "WorkItem" }],
        }),
    }),
});

// Export hooks for usage in functional components, which are
// auto-generated based on the defined endpoints
export const {
    useGetWorkItemForUserQuery,
    useLazyGetWorkItemQuery,
    useCreateWorkItemMutation,
    useCreateWorkTimeMutation,
    useDeleteWorkTimeMutation,
    useEditWorkTimeMutation,
    useDeleteWorkItemMutation,
} = workApi;
