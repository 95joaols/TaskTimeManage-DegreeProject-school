import { Box, Stack } from "@chakra-ui/layout";
import { Button, Heading } from "@chakra-ui/react";
import React from "react";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import UseMessage from "../Hooks/UseMessage";
import { useAppSelector } from "../store/hook";
import { defaultState, selectLoginUser } from "../store/state/authSlice";

function UserInfo() {
    const user = useAppSelector(selectLoginUser);
    const navigate = useNavigate();
    const dispatch = useDispatch();
    const message = UseMessage();

    const signout = () => {
        localStorage.removeItem("token");
        dispatch(defaultState());

        message({ errorOrMessage: "Goodbye", type: "success", objectType: "text" });

        navigate("/login");
    };
    return (
        <Box>
            <Stack p="4" boxShadow="xl" borderRadius="md">
                <Heading as="h1" size="md">
                    Task Time Manage
                </Heading>
                <Button size="xs" borderRadius="md" colorScheme="red" color="white" onClick={signout}>
                    Logout: {user.username}
                </Button>
            </Stack>
        </Box>
    );
}

export default UserInfo;
