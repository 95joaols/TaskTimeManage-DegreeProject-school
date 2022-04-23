import { createSlice, PayloadAction } from "@reduxjs/toolkit";

export interface AuthState {
  id: string | null;
  name: string | null;
  token: string | null;
}

const initialState: AuthState = {
  id: null,
  name: null,
  token: null,
};

export const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    setUser: (
      state,
      action: PayloadAction<{ id: string, name: string; token: string }>
    ) => {
      console.log("Test");

      state.id = action.payload.id;
      state.name = action.payload.name;
      state.token = action.payload.token;
    },
    defaultState: (state) => {
      console.log("initialState", initialState);
      state.id = initialState.id;
      state.name = initialState.name;
      state.token = initialState.token;
    },
  },
});

// Action creators are generated for each case reducer function
export const { setUser, defaultState } = authSlice.actions;

export default authSlice.reducer;
