import { Flex, Grid, GridItem, Stack, Text } from "@chakra-ui/react";
import React from "react";
import { WorkTime } from "../Types/WorkTime";

type Props = {
    workTimes: WorkTime[] | undefined;
};

function WorkTimeList({ workTimes }: Props) {
    return (
        <>
            {workTimes && workTimes.length > 0 && (
                <Stack p="4" bg={"white"} boxShadow="xl" borderRadius="md">
                    <Grid templateColumns="repeat(2, 1fr)" gap={2}>
                        <GridItem w="100%" h="10" bg="blueviolet">
                            <Flex h="10" pl="2" alignItems={"center"}>
                                <Text color={"white"}>Start</Text>
                            </Flex>
                        </GridItem>
                        <GridItem w="100%" h="10" bg="blueviolet" alignItems={"center"}>
                            <Flex h="10" pl="2" alignItems={"center"}>
                                <Text color={"white"}>Stop</Text>
                            </Flex>
                        </GridItem>
                        {workTimes.map((wt: WorkTime) => (
                            <GridItem key={wt.publicId} w="100%" bg="gray">
                                <Flex px="2" py="1">
                                    <Text>
                                        {new Intl.DateTimeFormat("se-se", {
                                            month: "2-digit",
                                            day: "2-digit",
                                            hour: "2-digit",
                                            minute: "2-digit",
                                        }).format(new Date(wt.time))}
                                    </Text>
                                </Flex>
                            </GridItem>
                        ))}
                    </Grid>
                </Stack>
            )}
        </>
    );
}

export default WorkTimeList;
