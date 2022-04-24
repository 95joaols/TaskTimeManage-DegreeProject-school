import { useGetWorkItemForUserQuery } from "../store/api/WorkApi";
import { useAppSelector } from "../store/hook";
import { selectLoginUser } from "../store/state/authSlice";
import { Text, Box, Stack } from '@chakra-ui/react'

type Props = {
    AddWorkItemPress: () => void;
};

function WorkItemMenu({ AddWorkItemPress}:Props) {
    const user = useAppSelector(selectLoginUser);
    const {
        data: WorkItemList,
        isLoading,
    } = useGetWorkItemForUserQuery(user.id!);

    return (
        <Box>
            <Stack p="4" boxShadow="xl" borderRadius="md">
            <Box as="button" borderRadius='md' mt={2} bg='purple' color='white'  onClick={AddWorkItemPress}>Add New</Box>

                {WorkItemList && WorkItemList.length > 0 &&
                    WorkItemList.map(wi => {
                        return (
                            <Box p={5}/>
                        )
                    })
                }
                {(isLoading && !WorkItemList) &&
                <Text>Loading...</Text>
                }
                {(!isLoading && ((WorkItemList && WorkItemList.length === 0) || !WorkItemList )) &&
                <Text>No Data...</Text>
                }
            </Stack>
        </Box>
    )
}

export default WorkItemMenu;