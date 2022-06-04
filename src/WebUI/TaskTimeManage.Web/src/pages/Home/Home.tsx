import { Box, Flex, useDisclosure } from "@chakra-ui/react";
import React from "react";
import CreateWorkItemModel from "../../components/Models/CreateWorkItemModel";
import UserInfo from "../../components/UserInfo";
import WorkItemControl from "../../components/WorkItemControl";
import WorkItemMenu from "../../components/WorkItemMenu";
import { useAppSelector } from "../../store/hook";
import { selectActiveWorkItemId } from "../../store/state/workItemSlice";

const Home = () => {
    const { isOpen, onOpen, onClose } = useDisclosure();
    const activeWorkItem = useAppSelector(selectActiveWorkItemId);

    return (
        <Flex>
            <Flex direction="column">
                <UserInfo />
                <WorkItemMenu AddWorkItemPress={onOpen} />
                <CreateWorkItemModel isOpen={isOpen} onClose={onClose} />
            </Flex>
            {activeWorkItem && (
                <Box ml="4" p="2" bg="gray" mt="3" boxShadow="xl" borderRadius="md">
                    <WorkItemControl />
                </Box>
            )}
        </Flex>
    );
};

export default Home;
