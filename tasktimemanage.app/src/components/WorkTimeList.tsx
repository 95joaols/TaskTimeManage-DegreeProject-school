import { EditIcon } from "@chakra-ui/icons";
import { Center, Flex, Grid, GridItem, IconButton, Stack, Text, useDisclosure } from "@chakra-ui/react";
import React, { useState } from "react";
import { WorkTime } from "../Types/WorkTime";
import EditWorkTimeModel from "./Models/EditWorkTimeModel";

type Props = {
    workTimes: WorkTime[] | undefined;
    activeWorkItem: string;
};

function WorkTimeList({ workTimes, activeWorkItem }: Props) {
    const { isOpen, onOpen, onClose } = useDisclosure();
    const [selectedWorkTime, setSelectedWorkTime] = useState<WorkTime>();

    const OpenModel = (workTime: WorkTime) => {
        setSelectedWorkTime(workTime);
        onOpen();
    };

    return (
        <>
            {workTimes && workTimes.length > 0 && (
                <Stack p="4" bg={"white"} boxShadow="xl" borderRadius="md">
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
                                            month: "2-digit",
                                            day: "2-digit",
                                            hour: "2-digit",
                                            minute: "2-digit",
                                        }).format(new Date(wt.time))}
                                    </Text>
                                    <Center>
                                        <IconButton
                                            aria-label="Edit Or Delete"
                                            icon={<EditIcon />}
                                            w={"min"}
                                            h={"min"}
                                            colorScheme={"gray"}
                                            onClick={() => {
                                                OpenModel(wt);
                                            }}
                                        />
                                    </Center>
                                </Flex>
                            </GridItem>
                        ))}
                    </Grid>
                    {selectedWorkTime && (
                        <EditWorkTimeModel
                            isOpen={isOpen}
                            onClose={onClose}
                            workTime={selectedWorkTime}
                            activeWorkItem={activeWorkItem}
                        />
                    )}
                </Stack>
            )}
        </>
    );
}

export default WorkTimeList;
