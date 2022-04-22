import { Grid, Heading, Stack } from "@chakra-ui/layout";
import { Form, Formik } from "formik";
import { InputControl, SubmitButton } from "formik-chakra-ui";
import { useCreateUserMutation } from "../../store/api/authApi";

const Signup = () => {
  const [createUser, { data, isLoading }] = useCreateUserMutation();
  console.log(data);

  return (
    <Formik
      initialValues={{ name: "", password: "" }}
      onSubmit={(values) => {
        createUser({ ...values });
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
              Signup
            </Heading>
            <InputControl
              name="name"
              label="Name"
              inputProps={{
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
            <SubmitButton isLoading={isLoading}>Signup</SubmitButton>
          </Stack>
        </Grid>
      </Form>
    </Formik>
  );
};

export default Signup;
