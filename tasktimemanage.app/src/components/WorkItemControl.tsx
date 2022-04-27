import { Box, Center, Flex, Grid, GridItem, Heading } from "@chakra-ui/layout";
import { Button, Text } from "@chakra-ui/react";
import React, { useEffect, useState } from "react";
import { useLazyGetWorkItemQuery } from "../store/api/WorkApi";

type Props = {
    activeWorkItem: string | undefined;
};

function WorkItemControl({ activeWorkItem }: Props) {
    const [LastActive, setLastActive] = useState<string>();
    const [trigger, result, lastPromiseInfo] = useLazyGetWorkItemQuery();

    console.log("result", result, "activeWorkItem", activeWorkItem);

    useEffect(() => {
        if (activeWorkItem) {
            if (!LastActive || activeWorkItem !== LastActive) {
                setLastActive(activeWorkItem);
                trigger(activeWorkItem);
            }
        }
    }, [activeWorkItem]);

    const onPress = () => {
        console.log("Press");
    };

    const WorkTimesCount = result.data?.WorkTimes?.length ?? 0;

    return (
        <Box>
            <Center>
                <Heading as="h1" size="lg">
                    {result.isUninitialized && !activeWorkItem && <Text>No Selected</Text>}
                    {result.data && result.data.name}
                </Heading>
            </Center>
            <Flex gap={5}>
                {WorkTimesCount > 0 && (
                    <Grid templateColumns="repeat(2, 1fr)" gap={2}>
                        <GridItem w="100%" h="10" bg="blue.500">
                            <Text>Start</Text>
                        </GridItem>
                        <GridItem w="100%" h="10" bg="blue.500">
                            <Text>Stop</Text>
                        </GridItem>
                        {result.data?.WorkTimes?.map((wt) => (
                            <GridItem key={wt.id} w="100%" h="10" bg="blue.500">
                                <Text>wt.time</Text>
                            </GridItem>
                        ))}
                    </Grid>
                )}
                <Button colorScheme={"purple"} onClick={onPress}>
                    {WorkTimesCount % 2 === 1 ? "Stop" : "Start"}
                </Button>
            </Flex>
        </Box>
    );
}

export default WorkItemControl;
