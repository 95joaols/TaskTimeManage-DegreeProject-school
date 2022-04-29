import { Box, Center, Flex, Grid, GridItem, Heading } from "@chakra-ui/layout";
import { Button, Text } from "@chakra-ui/react";
import React, { useEffect, useState } from "react";
import { useCreateWorkTimeMutation, useLazyGetWorkItemQuery } from "../store/api/WorkApi";
import { WorkTime } from "../Types/WorkTime";
import CalculateTime from "./CalculateTime";

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
                setLastActive(activeWorkItem);
                trigger(activeWorkItem);
            }
        }
    }, [activeWorkItem]);

    const onPress = () => {
        if (activeWorkItem) {
            const dateTime = new Date();

            createWorkTimeApi({ workTime: { time: dateTime }, publicId: activeWorkItem });
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
                {workTimesCount > 0 && (
                    <Grid templateColumns="repeat(2, 1fr)" gap={2}>
                        <GridItem w="100%" h="10" bg="blue.500">
                            <Text>Start</Text>
                        </GridItem>
                        <GridItem w="100%" h="10" bg="blue.500">
                            <Text>Stop</Text>
                        </GridItem>
                        {result.data?.workTimes?.map((wt: WorkTime, index) => (
                            <GridItem key={index} w="100%" h="10" bg="blue.500">
                                <Text>
                                    {new Intl.DateTimeFormat("se-se", {
                                        year: "numeric",
                                        month: "2-digit",
                                        day: "2-digit",
                                        hour: "2-digit",
                                        minute: "2-digit",
                                        second: "2-digit",
                                    }).format(new Date(wt.time))}
                                </Text>
                            </GridItem>
                        ))}
                    </Grid>
                )}
                {activeWorkItem && (
                    <Button colorScheme={"purple"} onClick={onPress} isLoading={isLoading}>
                        {workTimesCount % 2 === 1 ? "Stop" : "Start"}
                    </Button>
                )}
            </Flex>
        </Box>
    );
}

export default WorkItemControl;
