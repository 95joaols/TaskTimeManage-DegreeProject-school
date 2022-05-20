import {EditIcon} from "@chakra-ui/icons";
import {Box, Button, Center, Flex, Heading, IconButton, Text, useDisclosure} from "@chakra-ui/react";
import React, {useEffect, useState} from "react";
import UseMessage from "../Hooks/UseMessage";
import {useCreateWorkTimeMutation, useLazyGetWorkItemQuery} from "../store/api/WorkApi";
import CalculateTime from "./CalculateTime";
import EditWorkItemModel from "./Models/EditWorkItemModel";
import WorkTimeList from "./WorkTimeList";

type Props = {
    activeWorkItem: string;
    onReset: () => void;
};

function WorkItemControl({activeWorkItem, onReset}: Props) {
    const {isOpen: isOpenEditModel, onOpen: onOpenEditModel, onClose: onCloseEditModel} = useDisclosure();

    const [LastActive, setLastActive] = useState<string>();
    const [workTimesCount, setWorkTimesCount] = useState(0);

    const [trigger, WorkItemResult] = useLazyGetWorkItemQuery();
    const [createWorkTimeApi, {isSuccess, isLoading, error: CreateWorkTimeError}] = useCreateWorkTimeMutation();

    const message = UseMessage();

    useEffect(() => {
        setWorkTimesCount(WorkItemResult.data?.workTimes?.length ?? 0);
    }, [WorkItemResult]);

    useEffect(() => {
        if (WorkItemResult.isError && WorkItemResult.error) {
            message({errorOrMessage: WorkItemResult.error, type: "error", objectType: "object"});
        }
    }, [WorkItemResult.error, WorkItemResult.isError]);
    useEffect(() => {
        if (isSuccess) {
            message({errorOrMessage: "Create", type: "success", objectType: "text"});
        }
    }, [isSuccess]);

    useEffect(() => {
        if (CreateWorkTimeError) {
            message({errorOrMessage: CreateWorkTimeError, type: "error", objectType: "object"});
        }
    }, [CreateWorkTimeError]);

    useEffect(() => {
        if (activeWorkItem) {
            if (!LastActive || activeWorkItem !== LastActive) {
                if (workTimesCount % 2 === 1) {
                    if (LastActive) {
                        createWorkTimeApi({time: new Date(), workItemPublicId: LastActive});
                    }
                }
                setLastActive(activeWorkItem);
                trigger(activeWorkItem);
            }
        }
    }, [activeWorkItem]);

    const onPress = () => {
        createWorkTimeApi({time: new Date(), workItemPublicId: activeWorkItem});
    };
    return (
        <Box>
            <Center>
                <Heading as="h1" size="lg">
                    {!activeWorkItem && <Text>No Selected</Text>}
                    {WorkItemResult.data && WorkItemResult.data.name}
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
                    <CalculateTime WorkTimes={WorkItemResult.data?.workTimes}/>
                </Center>
                <IconButton aria-label="Edit" icon={<EditIcon/>} colorScheme={"blue"} onClick={onOpenEditModel}/>
                {WorkItemResult.data && (
                    <EditWorkItemModel
                        isOpen={isOpenEditModel}
                        onClose={onCloseEditModel}
                        workItem={WorkItemResult.data}
                        onReset={onReset}
                    />
                )}
            </Flex>
            <Flex gap={5}>
                <WorkTimeList workTimes={WorkItemResult.data?.workTimes}/>
            </Flex>
        </Box>
    );
}

export default WorkItemControl;
