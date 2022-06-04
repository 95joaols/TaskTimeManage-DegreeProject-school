import { configureStore } from "@reduxjs/toolkit";
import authReducer from "./state/authSlice";
import { setupListeners } from "@reduxjs/toolkit/query";
import { authApi } from "./api/authApi";
import { workApi } from "./api/WorkApi";
import WorkItemReducer from "./state/workItemSlice";

export const store = configureStore({
    reducer: {
        auth: authReducer,
        workItem: WorkItemReducer,
        [authApi.reducerPath]: authApi.reducer,
        [workApi.reducerPath]: workApi.reducer,
    },
    middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(authApi.middleware).concat(workApi.middleware),
});

// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<typeof store.getState>;
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch;
setupListeners(store.dispatch);
