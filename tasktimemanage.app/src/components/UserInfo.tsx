import { Box, Stack } from "@chakra-ui/layout";
import { Text, Heading, Button} from '@chakra-ui/react';
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { useAppSelector } from "../store/hook";
import { defaultState, selectLoginUser } from "../store/state/authSlice";

function UserInfo() {
    const user = useAppSelector(selectLoginUser);
    const navigate = useNavigate();
    const dispatch = useDispatch();

    const signout = () => {
        localStorage.removeItem("token");
        dispatch(defaultState());
        navigate("/login");
      };
    return (
        <Box>
            <Stack p="4" boxShadow="xl" borderRadius="md">
                <Heading as='h1' size='md'>Task Time Manage</Heading>
                <Text fontSize='md'>User: {user.name}</Text>
                <Button size='xs' borderRadius='md' colorScheme='red' color='white'  onClick={signout}>Signout</Button>
            </Stack>
        </Box>
    )
}

export default UserInfo;