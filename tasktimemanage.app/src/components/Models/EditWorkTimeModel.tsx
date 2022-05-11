import { EditIcon } from "@chakra-ui/icons";
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
import { useEditWorkTimeMutation } from "../../store/api/WorkApi";
import { WorkTime } from "../../Types/WorkTime";

type Props = {
    onClose: () => void;
    isOpen: boolean;
    workTime: WorkTime;
    activeWorkItem: string;
};

function EditWorkTimeModel({ onClose, isOpen, workTime, activeWorkItem }: Props) {
    const [Edit, { data: dataEdit, isLoading: isLoadingEdit, error, isError }] = useEditWorkTimeMutation();
    const [date, setDate] = useState(workTime.time);
    const handleInputChange = (e: any) => {
        console.log("target", e.target.value);
        console.log("target date", new Date(e.target.value).toISOString());
        console.log("toDateString", toDateString(new Date(e.target.value)));

        setDate(new Date(e.target.value));
    };
    const toast = useToast();

    useEffect(() => {
        setDate(workTime.time);
    }, [workTime]);

    useEffect(() => {
        if (isError) {
            toast({
                title: (error as any).data.title,
                status: "error",
                duration: 5000,
            });
        }
    }, [isError, error]);

    useEffect(() => {
        if (dataEdit) {
            onClose();
        }
    }, [dataEdit]);

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

    const onEditWorkTime = () => {
        const editWorkTime: WorkTime = { publicId: workTime.publicId, time: date };
        Edit({ workTime: editWorkTime, TimeItemPublicId: activeWorkItem });
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
                                    isLoading={isLoadingEdit}
                                    aria-label="Edit"
                                    icon={<EditIcon />}
                                    colorScheme={"purple"}
                                    onClick={onEditWorkTime}
                                />
                            </Flex>
                        </FormControl>
                    </ModalBody>
                    <ModalFooter>
                        <Button colorScheme="gray" mr={3} onClick={onClose}>
                            Close
                        </Button>
                    </ModalFooter>
                </ModalContent>
            </Modal>
        </>
    );
}

export default EditWorkTimeModel;
