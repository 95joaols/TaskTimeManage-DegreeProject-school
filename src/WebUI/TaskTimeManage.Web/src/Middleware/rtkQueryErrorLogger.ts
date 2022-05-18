import { toast } from "@chakra-ui/react";
import { MiddlewareAPI, isRejectedWithValue, Middleware } from "@reduxjs/toolkit";
import UseMessage from "../Hooks/UseMessage";

/**
 * Log a warning and show a toast!
 */
export const rtkQueryErrorLogger: Middleware = (api: MiddlewareAPI) => (next) => (action) => {
    //const message = UseMessage();

    // RTK Query uses `createAsyncThunk` from redux-toolkit under the hood, so we're able to utilize these matchers!
    if (isRejectedWithValue(action)) {
        console.warn("We got a rejected action!", action);
        //message({ errorOrMessage: action.error.data.message, type: "error", objectType: "text" });
    }

    return next(action);
};
