import { Flex, Grid, Heading, Stack, Text } from "@chakra-ui/layout";
import { useToast } from "@chakra-ui/react";
import { Form, Formik } from "formik";
import { InputControl, SubmitButton } from "formik-chakra-ui";
import { useNavigate } from "react-router";
import { Link } from "react-router-dom";
import { useLoginMutation } from "../../store/api/authApi";
import { useAppDispatch } from "../../store/hook";
import { setUser } from "../../store/state/authSlice";

const Login = () => {
  const dispatch = useAppDispatch();
  const toast = useToast();
  const navigate = useNavigate();
  const [Login, { data, isLoading, error, isError, isSuccess }] =
    useLoginMutation();
  console.log(data);
  if (isError) {
    toast({
      title: (error as any).data.message,
      status: "error",
      duration: 5000,
    });
  }
  if (isSuccess) {
    dispatch(setUser({ token: data.token, name: data.name }));
    navigate("/");
    localStorage.setItem("token", data.token);
  }

  console.log(error);

  return (
    <Formik
      initialValues={{ name: "", password: "" }}
      onSubmit={(values) => {
        Login({ ...values });
      }}
    >
      <Form>
        <Grid h="100vh" placeItems="center">
          <Stack p="4" boxShadow="xl" borderRadius="md">
            <Heading
              color="teal"
              textAlign="center"
              fontSize="lg"
              fontWeight="semibold"
            >
              Signin
            </Heading>
            <InputControl
              name="name"
              label="name"
              inputProps={{
                type: "text",
                placeholder: "Enter Name...",
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
              <Text as={Link} to="/forgot-password" color="teal">
                Forgot Password
              </Text>
            </Flex>
            <SubmitButton isLoading={isLoading}>Signin</SubmitButton>
          </Stack>
        </Grid>
      </Form>
    </Formik>
  );
};

export default Login;
