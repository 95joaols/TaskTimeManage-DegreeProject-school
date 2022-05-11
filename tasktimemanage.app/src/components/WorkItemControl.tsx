import { DeleteIcon, EditIcon } from "@chakra-ui/icons";
import { Box, Button, Center, Flex, Heading, IconButton, Text, useDisclosure } from "@chakra-ui/react";
import React, { useEffect, useState } from "react";
import { useCreateWorkTimeMutation, useLazyGetWorkItemQuery } from "../store/api/WorkApi";
import CalculateTime from "./CalculateTime";
import EditWorkItemModel from "./Models/EditWorkItemModel";
import RemoveWorkItemMode from "./Models/RemoveWorkItemMode";
import WorkTimeList from "./WorkTimeList";

type Props = {
    activeWorkItem: string;
    onReset: () => void;
};

function WorkItemControl({ activeWorkItem, onReset }: Props) {
    const { isOpen, onOpen, onClose } = useDisclosure();
    const { isOpen: isOpenEditModel, onOpen: onOpenEditModel, onClose: onCloseEditModel } = useDisclosure();

    const [LastActive, setLastActive] = useState<string>();
    const [workTimesCount, setWorkTimesCount] = useState(0);

    const [trigger, WorkItemResult] = useLazyGetWorkItemQuery();
    const [createWorkTimeApi, { isLoading, error }] = useCreateWorkTimeMutation();

    useEffect(() => {
        setWorkTimesCount(WorkItemResult.data?.workTimes?.length ?? 0);
    }, [WorkItemResult]);

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
                    {!activeWorkItem && <Text>No Selected</Text>}
                    {WorkItemResult.data && WorkItemResult.data.name}
                    <IconButton aria-label="Delete" icon={<DeleteIcon />} onClick={onOpen} colorScheme={"red"} ml="2" />
                </Heading>
            </Center>
            <Flex bg={"white"} justifyContent="space-between" alignItems="center" borderRadius="md" p={2} my={2}>
                <Button
                    colorScheme={workTimesCount % 2 === 1 ? "red" : "purple"}
                    onClick={onPress}
                    isLoading={isLoading}
                >
                    {workTimesCount % 2 === 1 ? "Stop" : "Start"}
                </Button>
                <Center m="3">
                    <CalculateTime WorkTimes={WorkItemResult.data?.workTimes} />
                </Center>
                <IconButton aria-label="Edit" icon={<EditIcon />} colorScheme={"blue"} onClick={onOpenEditModel} />
                {WorkItemResult.data && (
                    <EditWorkItemModel
                        isOpen={isOpenEditModel}
                        onClose={onCloseEditModel}
                        workItem={WorkItemResult.data}
                    />
                )}
            </Flex>
            <Flex gap={5}>
                <WorkTimeList workTimes={WorkItemResult.data?.workTimes} />
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
