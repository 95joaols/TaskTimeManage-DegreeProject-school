import { Flex, Grid, Heading, Stack, Text } from "@chakra-ui/layout";
import { Form, Formik } from "formik";
import { InputControl, SubmitButton } from "formik-chakra-ui";
import React, { useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import UseMessage from "../../Hooks/UseMessage";
import { useCreateUserMutation } from "../../store/api/authApi";
import * as Yup from "yup";
import YupPassword from "yup-password";
YupPassword(Yup);

const Signup = () => {
    const [createUser, { data, isLoading, error, isError }] = useCreateUserMutation();
    const message = UseMessage();
    const navigate = useNavigate();

    useEffect(() => {
        if (isError && error) {
            message({ errorOrMessage: error, type: "error", objectType: "object" });
        }
        if (data) {
            message({ errorOrMessage: "Created", type: "success", objectType: "text" });

            navigate("/Login");
        }
    }, [error, data, isError]);

    return (
        <Formik
            initialValues={{ username: "", password: "", repeatPassword: "" }}
            validationSchema={SignupSchema}
            onSubmit={(values) => {
                const form = { ...values };

                if (form.password === form.repeatPassword) {
                    createUser(form);
                } else {
                    message({
                        errorOrMessage: "The password and Repeat Password are not the same",
                        type: "error",
                        objectType: "text",
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
                        <InputControl
                            name="repeatPassword"
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
    username: Yup.string().required(),
    password: Yup.string().password().required(),
    repeatPassword: Yup.string().password().required(),
});

export default Signup;
