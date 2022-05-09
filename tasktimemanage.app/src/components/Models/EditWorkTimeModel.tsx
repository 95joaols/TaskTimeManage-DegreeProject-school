import { DeleteIcon, EditIcon } from "@chakra-ui/icons";
import {
    Modal,
    ModalBody,
    ModalCloseButton,
    ModalContent,
    ModalFooter,
    ModalHeader,
    ModalOverlay,
} from "@chakra-ui/modal";
import { Button, Flex, FormControl, FormErrorMessage, FormLabel, IconButton, Input, useToast } from "@chakra-ui/react";
import React, { useEffect, useState } from "react";
import { useDeleteWorkTimeMutation } from "../../store/api/WorkApi";
import { WorkTime } from "../../Types/WorkTime";

type Props = {
    onClose: () => void;
    isOpen: boolean;
    workTime: WorkTime;
    activeWorkItem: string;
};

function EditWorkTimeModel({ onClose, isOpen, workTime, activeWorkItem }: Props) {
    const [Delete, { data, isLoading, error, isError: createUserError }] = useDeleteWorkTimeMutation();
    const [date, setDate] = useState(workTime.time);
    const handleInputChange = (e: any) => {
        console.log("target", e.target.value);
        console.log("target date", new Date(e.target.value).toISOString());
        console.log("toDateString", toDateString(new Date(e.target.value)));

        setDate(e.target.value);
    };
    const [isError, setIsError] = useState(false);
    const toast = useToast();

    useEffect(() => {
        setDate(workTime.time);
    }, [workTime]);

    useEffect(() => {
        if (createUserError) {
            toast({
                title: (error as any).data.title,
                status: "error",
                duration: 5000,
            });
        }
    }, [createUserError, error]);

    useEffect(() => {
        if (data) {
            onClose();
        }
    }, [data]);

    const DeleteWorkTime = () => {
        Delete({ workTime, TimeItemPublicId: activeWorkItem });
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
                <ModalOverlay />
                <ModalContent>
                    <ModalHeader>Edit</ModalHeader>
                    <ModalCloseButton />
                    <ModalBody>
                        <FormControl isInvalid={isError} isRequired>
                            <FormLabel htmlFor="Date">Date</FormLabel>
                            <Flex>
                                <Input
                                    id="Date"
                                    type="datetime-local"
                                    value={toDateString(new Date(date))}
                                    onChange={handleInputChange}
                                />
                                {isError && <FormErrorMessage>Name is required.</FormErrorMessage>}

                                <IconButton
                                    aria-label="Edit"
                                    icon={<EditIcon />}
                                    colorScheme={"purple"}
                                    onClick={() => {
                                        console.log();
                                    }}
                                />
                            </Flex>
                        </FormControl>
                    </ModalBody>
                    <ModalFooter>
                        <Button colorScheme="gray" mr={3} onClick={onClose}>
                            Close
                        </Button>
                        <IconButton
                            aria-label="Edit"
                            icon={<DeleteIcon />}
                            colorScheme={"red"}
                            isLoading={isLoading}
                            onClick={DeleteWorkTime}
                        />
                    </ModalFooter>
                </ModalContent>
            </Modal>
        </>
    );
}

export default EditWorkTimeModel;
