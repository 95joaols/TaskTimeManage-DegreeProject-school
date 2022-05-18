import { useToast } from "@chakra-ui/react";
import { SerializedError } from "@reduxjs/toolkit";
import { FetchBaseQueryError } from "@reduxjs/toolkit/dist/query";

type Message =
    | {
          type: "error";
          objectType: "object";
          errorOrMessage: FetchBaseQueryError | SerializedError | string | undefined;
      }
    | {
          type: "success" | "error";
          objectType: "text";
          errorOrMessage: string;
      };
interface errorResponseType {
    type: string;
    title: string;
    detail: string;
    status: number;
}

const UseMessage = () => {
    const toast = useToast();

    const Message = ({ errorOrMessage, type, objectType: manual }: Message) => {
        const fetchBaseQueryError = errorOrMessage as FetchBaseQueryError;
        const message = errorOrMessage as string;
        if (manual === "text") {
            if (message) {
                toast({
                    // eslint-disable-next-line @typescript-eslint/no-explicit-any
                    title: message,
                    status: type,
                    duration: 5000,
                });
                return;
            }
        }

        if (fetchBaseQueryError) {
            switch (fetchBaseQueryError.status) {
                case 400:
                case 500:
                    // eslint-disable-next-line @typescript-eslint/no-explicit-any
                    if ((fetchBaseQueryError.data as any).title) {
                        toast({
                            // eslint-disable-next-line @typescript-eslint/no-explicit-any
                            title: (fetchBaseQueryError.data as any).title,
                            // eslint-disable-next-line @typescript-eslint/no-explicit-any
                            description: (fetchBaseQueryError.data as any).detail,
                            status: type,
                            duration: 5000,
                        });
                        return;
                    }
                    const errorMessageObj: errorResponseType = JSON.parse(fetchBaseQueryError.data as string);
                    if (errorMessageObj && errorMessageObj.detail && errorMessageObj.title !== errorMessageObj.detail) {
                        toast({
                            title: errorMessageObj.title,
                            description: errorMessageObj.detail,
                            status: type,
                            duration: 5000,
                        });
                    } else if (errorMessageObj) {
                        toast({
                            title: errorMessageObj.title,
                            description: errorMessageObj.detail,
                            status: type,
                            duration: 5000,
                        });
                    }
                    return;
                case "CUSTOM_ERROR":
                case "FETCH_ERROR":
                case "PARSING_ERROR":
                    toast({
                        // eslint-disable-next-line @typescript-eslint/no-explicit-any
                        title: fetchBaseQueryError.error,
                        status: type,
                        duration: 5000,
                    });
                    return;
                default:
                    toast({
                        title: "Error",
                        description: fetchBaseQueryError.status,
                        status: type,
                        duration: 5000,
                    });
                    return;
            }
        }
    };

    return Message;
};

export default UseMessage;
