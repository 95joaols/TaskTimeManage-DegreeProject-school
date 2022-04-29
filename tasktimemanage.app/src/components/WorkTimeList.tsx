import { Flex, Grid, GridItem, Text } from "@chakra-ui/react";
import React from "react";
import { WorkTime } from "../Types/WorkTime";
import WorkTimeListButtonOption from "./WorkTimeListButtonOption";

type Props = {
    workTimes: WorkTime[] | undefined;
    activeWorkItem: string | undefined;
};

function WorkTimeList({ workTimes, activeWorkItem }: Props) {
    return (
        <>
            {workTimes && workTimes.length > 0 && (
                <Grid templateColumns="repeat(2, 1fr)" gap={2}>
                    <GridItem w="100%" h="10" bg="blue.500">
                        <Text>Start</Text>
                    </GridItem>
                    <GridItem w="100%" h="10" bg="blue.500">
                        <Text>Stop</Text>
                    </GridItem>
                    {workTimes?.map((wt: WorkTime) => (
                        <GridItem key={wt.publicId} w="100%" bg="gray">
                            <Flex>
                                <Text>
                                    {new Intl.DateTimeFormat("se-se", {
                                        year: "numeric",
                                        month: "2-digit",
                                        day: "2-digit",
                                        hour: "2-digit",
                                        minute: "2-digit",
                                    }).format(new Date(wt.time))}
                                </Text>
                                {activeWorkItem && (
                                    <WorkTimeListButtonOption workTime={wt} activeWorkItem={activeWorkItem} />
                                )}
                            </Flex>
                        </GridItem>
                    ))}
                </Grid>
            )}
        </>
    );
}

export default WorkTimeList;
