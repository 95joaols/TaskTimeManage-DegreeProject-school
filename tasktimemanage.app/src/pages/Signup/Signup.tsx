import { Flex, Grid, Heading, Stack, Text } from "@chakra-ui/layout";
import { useToast } from "@chakra-ui/react";
import { Form, Formik } from "formik";
import { InputControl, SubmitButton } from "formik-chakra-ui";
import React, { useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import * as Yup from "yup";
import { useCreateUserMutation } from "../../store/api/authApi";

const Signup = () => {
    const [createUser, { data, isLoading, error, isError }] = useCreateUserMutation();
    const toast = useToast();
    const navigate = useNavigate();

    useEffect(() => {
        console.log("data", data, "error", error, "isError", isError);
        if (isError && error) {
            console.log(error);
            console.log((error as any).data);
            toast({
                title: (error as any).data.title,
                status: "error",
                duration: 5000,
            });
        }
        if (data) {
            navigate("/Login");
        }
    }, [error, data, isError]);

    return (
        <Formik
            initialValues={{ name: "", password: "", RepeatPassword: "" }}
            validationSchema={SignupSchema}
            onSubmit={(values) => {
                const form = { ...values };
                console.log("form", form);

                if (form.password === form.RepeatPassword) {
                    createUser({ name: form.name, password: form.password });
                } else {
                    toast({
                        title: "The password and Repeat Password are not the same",
                        status: "error",
                        duration: 5000,
                    });
                }
            }}
        >
            <Form>
                <Grid h="100vh" placeItems="center">
                    <Stack p="4" boxShadow="xl" borderRadius="md">
                        <Heading color="teal" textAlign="center" fontSize="lg" fontWeight="semibold">
                            Signup
                        </Heading>
                        <InputControl
                            name="name"
                            label="User name"
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
                        <InputControl
                            name="RepeatPassword"
                            label="Repeat Password"
                            inputProps={{
                                placeholder: "Repeat Password...",
                                type: "password",
                            }}
                        />
                        <Flex justify="flex-end">
                            <Text as={Link} to="/Login" color="teal">
                                Alredy user
                            </Text>
                        </Flex>
                        <SubmitButton isLoading={isLoading} colorScheme="purple">
                            Signup
                        </SubmitButton>
                    </Stack>
                </Grid>
            </Form>
        </Formik>
    );
};

const SignupSchema = Yup.object().shape({
    name: Yup.string().required("Required"),
    password: Yup.string().required("Required"),
    RepeatPassword: Yup.string().required("Required"),
});

export default Signup;
