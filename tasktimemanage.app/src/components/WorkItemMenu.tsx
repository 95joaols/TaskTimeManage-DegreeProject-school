import { AddIcon } from "@chakra-ui/icons";
import { Box, Button, Stack, Text } from "@chakra-ui/react";
import React from "react";
import { useGetWorkItemForUserQuery } from "../store/api/WorkApi";
import { useAppSelector } from "../store/hook";
import { selectLoginUser } from "../store/state/authSlice";
import WorkItemBox from "./WorkItemBox";

type Props = {
    activeWorkItem: string | undefined;
    AddWorkItemPress: () => void;
    onWorkItemPress: (id: string) => void;
};

function WorkItemMenu({ AddWorkItemPress, onWorkItemPress, activeWorkItem }: Props) {
    const user = useAppSelector(selectLoginUser);
    const { data: WorkItemList, isLoading } = useGetWorkItemForUserQuery(user.id!);

    return (
        <Box>
            <Stack p="4" boxShadow="xl" borderRadius="md">
                <Button size="xs" borderRadius="md" mt={2} mb={4} colorScheme="purple" onClick={AddWorkItemPress}>
                    Add New
                    <AddIcon ml="2" />
                </Button>

                {WorkItemList &&
                    WorkItemList.length > 0 &&
                    WorkItemList.map((wi) => {
                        return (
                            <WorkItemBox
                                key={wi.publicId}
                                workItem={wi}
                                onPress={onWorkItemPress}
                                activeWorkItem={activeWorkItem}
                            />
                        );
                    })}
                {isLoading && !WorkItemList && <Text>Loading...</Text>}
                {!isLoading && ((WorkItemList && WorkItemList.length === 0) || !WorkItemList) && (
                    <Text>No Data...</Text>
                )}
            </Stack>
        </Box>
    );
}

export default WorkItemMenu;
