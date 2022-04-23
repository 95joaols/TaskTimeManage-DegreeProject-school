import { Flex, Grid, Heading, Stack, Text } from "@chakra-ui/layout";
import { useToast } from "@chakra-ui/react";
import { Form, Formik } from "formik";
import { InputControl, SubmitButton } from "formik-chakra-ui";
import { useNavigate } from "react-router";
import { Link } from "react-router-dom";
import { useLoginMutation } from "../../store/api/authApi";
import { useAppDispatch } from "../../store/hook";
import { setUser } from "../../store/state/authSlice";
import jwt from "jwt-decode";
import { UserToken } from "../../Types/UserToken";
import { useEffect } from "react";

const Login = () => {
  const dispatch = useAppDispatch();
  const toast = useToast();
  const navigate = useNavigate();
  const [Login, { data: token, isLoading, error, isError, isSuccess }] = useLoginMutation();

  useEffect(() => {
  
    console.log(token);
    if (isError) {
      toast({
        title: (error as any).data.message,
        status: "error",
        duration: 5000,
      });
    }
    if (isSuccess && token) {
      const user: UserToken = jwt(token);

      dispatch(setUser({ token: token, name: user.unique_name, id: user.nameid }));

      console.log("test");
      
      localStorage.setItem("token", token);
      navigate("/");
      }
  }, [error, token])

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
            <Flex justify="flex-end">
              <Text as={Link} to="/Signup" color="teal">
              Signup
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
