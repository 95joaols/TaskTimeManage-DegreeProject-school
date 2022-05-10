import { DeleteIcon } from "@chakra-ui/icons";
import { Box, Button, Center, Flex, Heading, IconButton, Text, useDisclosure } from "@chakra-ui/react";
import React, { useEffect, useState } from "react";
import { useCreateWorkTimeMutation, useLazyGetWorkItemQuery } from "../store/api/WorkApi";
import CalculateTime from "./CalculateTime";
import RemoveWorkItemMode from "./Models/RemoveWorkItemMode";
import WorkTimeList from "./WorkTimeList";

type Props = {
    activeWorkItem: string;
    onReset: () => void;
};

function WorkItemControl({ activeWorkItem, onReset }: Props) {
    const { isOpen, onOpen, onClose } = useDisclosure();

    const [LastActive, setLastActive] = useState<string>();
    const [workTimesCount, setworkTimesCount] = useState(0);

    const [trigger, result] = useLazyGetWorkItemQuery();
    const [createWorkTimeApi, { isLoading, error }] = useCreateWorkTimeMutation();

    useEffect(() => {
        setworkTimesCount(result.data?.workTimes?.length ?? 0);
    }, [result]);

    useEffect(() => {
        if (activeWorkItem) {
            if (!LastActive || activeWorkItem !== LastActive) {
                if (workTimesCount % 2 === 1) {
                    if (LastActive) {
                        createWorkTimeApi({ workTime: { time: new Date() }, publicId: LastActive });
                    }
                }
                setLastActive(activeWorkItem);
                trigger(activeWorkItem);
            }
        }
    }, [activeWorkItem]);

    const onPress = () => {
        createWorkTimeApi({ workTime: { time: new Date() }, publicId: activeWorkItem });
    };
    return (
        <Box>
            <Center>
                <Heading as="h1" size="lg">
                    {result.isUninitialized && !activeWorkItem && <Text>No Selected</Text>}
                    {result.data && result.data.name}
                    <IconButton
                        aria-label="Delete"
                        icon={<DeleteIcon />}
                        w={"min"}
                        h={"min"}
                        onClick={onOpen}
                        colorScheme={"red"}
                    />
                </Heading>
            </Center>
            <Flex bg={"white"} w={"min"} borderRadius="md" p={2} my={2}>
                <Button
                    colorScheme={workTimesCount % 2 === 1 ? "red" : "purple"}
                    onClick={onPress}
                    isLoading={isLoading}
                >
                    {workTimesCount % 2 === 1 ? "Stop" : "Start"}
                </Button>
                <Center ml="3">
                    <CalculateTime WorkTimes={result.data?.workTimes} />
                </Center>
            </Flex>
            <Flex gap={5}>
                <WorkTimeList workTimes={result.data?.workTimes} activeWorkItem={activeWorkItem} />
            </Flex>
            {activeWorkItem && (
                <RemoveWorkItemMode
                    onDeleted={onReset}
                    isOpen={isOpen}
                    onClose={onClose}
                    activeWorkItem={activeWorkItem}
                />
            )}
        </Box>
    );
}

export default WorkItemControl;
