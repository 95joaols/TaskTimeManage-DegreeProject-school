import { Box, Center, Flex, Heading } from "@chakra-ui/layout";
import { Button, Text } from "@chakra-ui/react";
import React, { useEffect, useState } from "react";
import { useCreateWorkTimeMutation, useLazyGetWorkItemQuery } from "../store/api/WorkApi";
import CalculateTime from "./CalculateTime";
import WorkTimeList from "./WorkTimeList";

type Props = {
    activeWorkItem: string;
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
        createWorkTimeApi({ workTime: { time: new Date() }, publicId: activeWorkItem });
    };
    return (
        <Box>
            <Center>
                <Heading as="h1" size="lg">
                    {result.isUninitialized && !activeWorkItem && <Text>No Selected</Text>}
                    {result.data && result.data.name}
                </Heading>
            </Center>
            <Flex>
                <Button
                    colorScheme={workTimesCount % 2 === 1 ? "red" : "purple"}
                    onClick={onPress}
                    isLoading={isLoading}
                >
                    {workTimesCount % 2 === 1 ? "Stop" : "Start"}
                </Button>
                <CalculateTime WorkTimes={result.data?.workTimes} />
            </Flex>
            <Flex gap={5}>
                <WorkTimeList workTimes={result.data?.workTimes} activeWorkItem={activeWorkItem} />
            </Flex>
        </Box>
    );
}

export default WorkItemControl;
