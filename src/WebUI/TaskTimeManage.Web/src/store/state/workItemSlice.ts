import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { RootState } from "..";

export interface WorkItemSlice {
    activeWorkItemId: string | undefined;
}

const initialState: WorkItemSlice = {
    activeWorkItemId: undefined,
};

export const WorkItemSlice = createSlice({
    name: "workItem",
    initialState,
    reducers: {
        setSelectedWorkItemId: (state, action: PayloadAction<{ newActiveWorkItemId: string | undefined }>) => {
            state.activeWorkItemId = action.payload.newActiveWorkItemId;
        },
    },
});

// Action creators are generated for each case reducer function
export const { setSelectedWorkItemId } = WorkItemSlice.actions;
export const selectActiveWorkItemId = (state: RootState) => state.workItem.activeWorkItemId;

export default WorkItemSlice.reducer;
