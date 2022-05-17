import {Flex, Grid, Heading, Stack, Text} from "@chakra-ui/layout";
import {useToast} from "@chakra-ui/react";
import {Form, Formik} from "formik";
import {InputControl, SubmitButton} from "formik-chakra-ui";
import jwt from "jwt-decode";
import React, {useEffect} from "react";
import {useNavigate} from "react-router";
import {Link} from "react-router-dom";
import * as Yup from "yup";
import {useLoginMutation} from "../../store/api/authApi";
import {useAppDispatch} from "../../store/hook";
import {setUser} from "../../store/state/authSlice";
import {UserToken} from "../../Types/UserToken";

const Login = () => {
    const dispatch = useAppDispatch();
    const toast = useToast();
    const navigate = useNavigate();
    const [Login, {data: token, isLoading, error, isError, isSuccess}] = useLoginMutation();

    useEffect(() => {
        if (isError && error) {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            if ((error as any)?.data) {
                toast({
                    // eslint-disable-next-line @typescript-eslint/no-explicit-any
                    title: (error as any).data.title,
                    status: "error",
                    duration: 5000,
                });
            }
        }
        if (isSuccess && token) {
            const user: UserToken = jwt(token);

            dispatch(setUser({token: token, name: user.unique_name, id: user.nameid}));

            localStorage.setItem("token", token);
            navigate("/");
        }
    }, [error, token, dispatch]);

    return (
        <Formik
            initialValues={{username: "", password: ""}}
            validationSchema={LoginSchema}
            onSubmit={(values) => {
                Login({...values});
            }}
        >
            <Form>
                <Grid h="100vh" placeItems="center">
                    <Stack p="4" boxShadow="xl" borderRadius="md">
                        <Heading color="teal" textAlign="center" fontSize="lg" fontWeight="semibold">
                            Login
                        </Heading>
                        <InputControl
                            name="username"
                            label="Username"
                            inputProps={{
                                type: "text",
                                placeholder: "Enter Username...",
                            }}
                        />
                        <InputControl
                            name="password"
                            label="Password"
                            inputProps={{
                                placeholder: "Enter Password...",
                                type: "password",
                            }}
                        />
                        <Flex justify="flex-end">
                            <Text as={Link} to="/Signup" color="teal">
                                Signup
                            </Text>
                        </Flex>
                        <SubmitButton isLoading={isLoading} colorScheme="purple">
                            Login
                        </SubmitButton>
                    </Stack>
                </Grid>
            </Form>
        </Formik>
    );
};

const LoginSchema = Yup.object().shape({
    username: Yup.string().required("Required"),
    password: Yup.string().required("Required"),
});

export default Login;
