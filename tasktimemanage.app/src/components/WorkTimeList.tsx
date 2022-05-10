import { DeleteIcon, EditIcon } from "@chakra-ui/icons";
import { ButtonGroup, Center, Flex, Grid, GridItem, IconButton, Stack, Text, useDisclosure } from "@chakra-ui/react";
import React, { useState } from "react";
import { WorkTime } from "../Types/WorkTime";
import EditWorkTimeModel from "./Models/EditWorkTimeModel";
import RemoveWorkTimeModel from "./Models/RemoveWorkTimeModel";

type Props = {
    workTimes: WorkTime[] | undefined;
    activeWorkItem: string;
};

function WorkTimeList({ workTimes, activeWorkItem }: Props) {
    const { isOpen, onOpen, onClose } = useDisclosure();
    const { isOpen: isOpenDelete, onOpen: onOpenDelete, onClose: onCloseDelete } = useDisclosure();
    const [selectedWorkTime, setSelectedWorkTime] = useState<WorkTime>();

    const OpenDeleteModel = (workTime: WorkTime) => {
        setSelectedWorkTime(workTime);
        onOpenDelete();
    };
    const OpenEditModel = (workTime: WorkTime) => {
        setSelectedWorkTime(workTime);
        onOpen();
    };

    return (
        <>
            {workTimes && workTimes.length > 0 && (
                <Stack p="4" bg={"white"} boxShadow="xl" borderRadius="md">
                    <Grid templateColumns="repeat(2, 1fr)" gap={2}>
                        <GridItem w="100%" h="10" bg="blue">
                            <Flex h="10" pl="2" alignItems={"center"}>
                                <Text>Start</Text>
                            </Flex>
                        </GridItem>
                        <GridItem w="100%" h="10" bg="blue" alignItems={"center"}>
                            <Flex h="10" pl="2" alignItems={"center"}>
                                <Text>Stop</Text>
                            </Flex>
                        </GridItem>
                        {workTimes?.map((wt: WorkTime) => (
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
                                    <Center>
                                        <ButtonGroup size="sm" ml={2} isAttached variant="solid">
                                            <IconButton
                                                aria-label="Edit"
                                                icon={<EditIcon />}
                                                w={"min"}
                                                h={"min"}
                                                colorScheme={"blue"}
                                                onClick={() => {
                                                    OpenEditModel(wt);
                                                }}
                                            />
                                            <IconButton
                                                aria-label="Edit Or Delete"
                                                icon={<DeleteIcon />}
                                                w={"min"}
                                                h={"min"}
                                                colorScheme={"red"}
                                                onClick={() => {
                                                    OpenDeleteModel(wt);
                                                }}
                                            />
                                        </ButtonGroup>
                                    </Center>
                                </Flex>
                            </GridItem>
                        ))}
                    </Grid>
                    {selectedWorkTime && (
                        <>
                            <EditWorkTimeModel
                                isOpen={isOpen}
                                onClose={onClose}
                                workTime={selectedWorkTime}
                                activeWorkItem={activeWorkItem}
                            />
                            <RemoveWorkTimeModel
                                isOpen={isOpenDelete}
                                onClose={onCloseDelete}
                                workTime={selectedWorkTime}
                                activeWorkItem={activeWorkItem}
                            />
                        </>
                    )}
                </Stack>
            )}
        </>
    );
}

export default WorkTimeList;
