import { Box, Center, Flex, Grid, GridItem, Heading } from "@chakra-ui/layout";
import { Button, ButtonGroup, IconButton, Text } from "@chakra-ui/react";
import React, { useEffect, useState } from "react";
import { useCreateWorkTimeMutation, useLazyGetWorkItemQuery } from "../store/api/WorkApi";
import { WorkTime } from "../Types/WorkTime";
import CalculateTime from "./CalculateTime";
import WorkTimeList from "./WorkTimeList";

type Props = {
    activeWorkItem: string | undefined;
};

function WorkItemControl({ activeWorkItem }: Props) {
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
        if (activeWorkItem) {
            createWorkTimeApi({ workTime: { time: new Date() }, publicId: activeWorkItem });
        }
        console.log("Press");
    };
    return (
        <Box>
            <Center>
                <Heading as="h1" size="lg">
                    {result.isUninitialized && !activeWorkItem && <Text>No Selected</Text>}
                    {result.data && result.data.name}
                </Heading>
            </Center>
            <CalculateTime WorkTimes={result.data?.workTimes} />
            <Flex gap={5}>
                <WorkTimeList workTimes={result.data?.workTimes} activeWorkItem={activeWorkItem} />

                {activeWorkItem && (
                    <Button
                        colorScheme={workTimesCount % 2 === 1 ? "red" : "purple"}
                        onClick={onPress}
                        isLoading={isLoading}
                    >
                        {workTimesCount % 2 === 1 ? "Stop" : "Start"}
                    </Button>
                )}
            </Flex>
        </Box>
    );
}

export default WorkItemControl;
