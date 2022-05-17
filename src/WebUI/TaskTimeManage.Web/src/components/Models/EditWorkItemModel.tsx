import {DeleteIcon} from "@chakra-ui/icons";
import {
    Modal,
    ModalBody,
    ModalCloseButton,
    ModalContent,
    ModalFooter,
    ModalHeader,
    ModalOverlay,
} from "@chakra-ui/modal";
import {
    Box,
    Button,
    Flex,
    FormControl,
    FormLabel,
    IconButton,
    Input,
    Spacer,
    useDisclosure,
    useToast,
} from "@chakra-ui/react";
import React, {useEffect, useState} from "react";
import {useEditWorkItemMutation} from "../../store/api/WorkApi";
import {WorkItem} from "../../Types/WorkItem";
import {WorkTime} from "../../Types/WorkTime";
import RemoveWorkItemMode from "./RemoveWorkItemMode";
import RemoveWorkTimeModel from "./RemoveWorkTimeModel";

type Props = {
    onClose: () => void;
    onReset: () => void;
    isOpen: boolean;
    workItem: WorkItem;
};

function EditWorkItemModel({onClose, isOpen, workItem, onReset}: Props) {
    const [Edit, {data: dataEdit, isLoading: isLoadingEdit, error, isError}] = useEditWorkItemMutation();

    const {isOpen: isDeleteOpen, onOpen: onDeleteOpen, onClose: onDeleteClose} = useDisclosure();
    const {
        isOpen: isOpenDeleteWorkItem,
        onOpen: onOpenDeleteWorkItem,
        onClose: onCloseDeleteWorkItem,
    } = useDisclosure();

    const [workTimeToDelete, SetWorkTimeToDelete] = useState<WorkTime>();

    const [name, SetName] = useState(workItem.name);
    const [workTimes, SetWorkTimes] = useState(workItem.workTimes);

    const toast = useToast();

    useEffect(() => {
        SetName(workItem.name);
        SetWorkTimes(workItem.workTimes);
    }, [workItem]);

    useEffect(() => {
        if (dataEdit && !isError) {
            toast({
                title: "Save",
                status: "success",
                duration: 5000,
            });
        } else if (isError && error) {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            if ((error as any)?.data) {
                toast({
                    // eslint-disable-next-line @typescript-eslint/no-explicit-any
                    title: (error as any).data.title,
                    status: "error",
                    duration: 5000,
                });
            } else {
                toast({
                    title: "error",
                    status: "error",
                    duration: 5000,
                });
            }
        } else if (isError) {
            toast({
                title: "error",
                status: "error",
                duration: 5000,
            });
        }
    }, [error, isError, dataEdit]);

    const handleInputChange = (workTime: WorkTime, value: string) => {
        const newWorkTime: WorkTime = {...workTime, time: new Date(value)};
        if (workTimes) {
            SetWorkTimes((old) => {
                if (old) {
                    return old.map((o) => {
                        return o.publicId === workTime.publicId ? newWorkTime : o;
                    });
                } else {
                    return [workTime];
                }
            });
        }
    };

    const OnSaveWorkItem = () => {
        const workItemToSave: WorkItem = {...workItem, name, workTimes};
        Edit(workItemToSave);
    };

    const OpenDeleteModel = (workTime: WorkTime) => {
        SetWorkTimeToDelete(workTime);
        onDeleteOpen();
    };

    const toDateString = (date: Date) => {
        return (
            date.getFullYear().toString() +
            "-" +
            ("0" + (date.getMonth() + 1)).slice(-2) +
            "-" +
            ("0" + date.getDate()).slice(-2) +
            "T" +
            date.toTimeString().slice(0, 5)
        );
    };

    return (
        <>
            <Modal isCentered onClose={onClose} isOpen={isOpen} motionPreset="slideInBottom">
                <ModalOverlay/>
                <ModalContent>
                    <ModalHeader>Edit</ModalHeader>
                    <ModalCloseButton/>
                    <ModalBody>
                        <FormControl>
                            <FormLabel htmlFor="name">Name</FormLabel>
                            <Flex>
                                <Input id="name" type="text" value={name} onChange={(e) => SetName(e.target.value)}/>
                            </Flex>
                            {workTimes?.map((Wt, index) => (
                                <Box key={Wt.publicId}>
                                    <FormLabel htmlFor={Wt.publicId}>{index % 2 === 1 ? "Stop" : "Start"}</FormLabel>
                                    <Flex>
                                        <Input
                                            id={Wt.publicId}
                                            type="datetime-local"
                                            value={toDateString(new Date(Wt.time))}
                                            onChange={(e) => {
                                                handleInputChange(Wt, e.target.value);
                                            }}
                                        />
                                        <IconButton
                                            aria-label="Delete"
                                            icon={<DeleteIcon/>}
                                            colorScheme={"red"}
                                            onClick={() => {
                                                OpenDeleteModel(Wt);
                                            }}
                                        />
                                    </Flex>
                                </Box>
                            ))}
                        </FormControl>
                    </ModalBody>
                    <ModalFooter>
                        <IconButton
                            aria-label="Delete"
                            icon={<DeleteIcon/>}
                            onClick={onOpenDeleteWorkItem}
                            colorScheme={"red"}
                            ml="2"
                        />
                        <Spacer/>
                        <Button colorScheme="purple" onClick={OnSaveWorkItem} isLoading={isLoadingEdit}>
                            Save
                        </Button>
                        <Button colorScheme="gray" onClick={onClose}>
                            Close
                        </Button>
                    </ModalFooter>
                </ModalContent>
            </Modal>
            {workTimeToDelete && workItem.publicId && (
                <RemoveWorkTimeModel isOpen={isDeleteOpen} onClose={onDeleteClose} workTime={workTimeToDelete}/>
            )}
            {workItem.publicId && (
                <RemoveWorkItemMode
                    onDeleted={() => {
                        onCloseDeleteWorkItem();
                        onReset();
                    }}
                    isOpen={isOpenDeleteWorkItem}
                    onClose={onCloseDeleteWorkItem}
                    activeWorkItem={workItem.publicId}
                />
            )}
        </>
    );
}

export default EditWorkItemModel;
