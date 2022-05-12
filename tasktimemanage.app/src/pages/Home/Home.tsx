import { Box, Flex, useDisclosure } from "@chakra-ui/react";
import React, { useState } from "react";
import CreateWorkItemModel from "../../components/Models/CreateWorkItemModel";
import UserInfo from "../../components/UserInfo";
import WorkItemControl from "../../components/WorkItemControl";
import WorkItemMenu from "../../components/WorkItemMenu";

const Home = () => {
    const { isOpen, onOpen, onClose } = useDisclosure();
    const [ActiveWorkItem, setActiveWorkItem] = useState<string>();

    return (
        <Flex>
            <Flex direction="column">
                <UserInfo />
                <WorkItemMenu
                    AddWorkItemPress={onOpen}
                    onWorkItemPress={setActiveWorkItem}
                    activeWorkItem={ActiveWorkItem}
                />
                <CreateWorkItemModel
                    isOpen={isOpen}
                    onClose={onClose}
                    createWorkItem={(wi) => setActiveWorkItem(wi.publicId)}
                />
            </Flex>
            {ActiveWorkItem && (
                <Box ml="4" p="2" bg="gray" mt="3" boxShadow="xl" borderRadius="md">
                    <WorkItemControl
                        activeWorkItem={ActiveWorkItem}
                        onReset={() => {
                            setActiveWorkItem(undefined);
                        }}
                    />
                </Box>
            )}
        </Flex>
    );
};

export default Home;
