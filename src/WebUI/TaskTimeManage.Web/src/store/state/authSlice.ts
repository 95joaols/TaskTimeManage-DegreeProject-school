import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { RootState } from "..";

export interface AuthState {
    id: string | null;
    username: string | null;
    token: string | null;
}

const initialState: AuthState = {
    id: null,
    username: null,
    token: null,
};

export const authSlice = createSlice({
    name: "auth",
    initialState,
    reducers: {
        setUser: (state, action: PayloadAction<{ id: string; name: string; token: string }>) => {
            state.id = action.payload.id;
            state.username = action.payload.name;
            state.token = action.payload.token;
        },
        defaultState: (state) => {
            state.id = initialState.id;
            state.username = initialState.username;
            state.token = initialState.token;
        },
    },
});

// Action creators are generated for each case reducer function
export const { setUser, defaultState } = authSlice.actions;
export const selectLoginUser = (state: RootState) => state.auth;

export default authSlice.reducer;
