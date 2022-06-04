import { AddIcon } from "@chakra-ui/icons";
import { Box, Button, Stack, Text } from "@chakra-ui/react";
import React, { useEffect } from "react";
import UseMessage from "../Hooks/UseMessage";
import { useGetWorkItemForUserQuery } from "../store/api/WorkApi";
import { useAppSelector } from "../store/hook";
import { selectLoginUser } from "../store/state/authSlice";
import WorkItemBox from "./WorkItemBox";

type Props = {
    AddWorkItemPress: () => void;
};

function WorkItemMenu({ AddWorkItemPress }: Props) {
    const user = useAppSelector(selectLoginUser);
    // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
    const { data: WorkItemList, isLoading, error } = useGetWorkItemForUserQuery(user.id!);
    const message = UseMessage();

    useEffect(() => {
        if (error) {
            message({ errorOrMessage: error, type: "error", objectType: "object" });
        }
    }, [error]);

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
                        return <WorkItemBox key={wi.publicId} workItem={wi} />;
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
