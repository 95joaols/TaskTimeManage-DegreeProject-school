import {
    Modal,
    ModalBody,
    ModalCloseButton,
    ModalContent,
    ModalFooter,
    ModalHeader,
    ModalOverlay,
} from "@chakra-ui/modal";
import { Button, Text, useToast } from "@chakra-ui/react";
import React, { useEffect } from "react";
import { useDeleteWorkTimeMutation } from "../../store/api/WorkApi";
import { WorkTime } from "../../Types/WorkTime";

type Props = {
    onClose: () => void;
    isOpen: boolean;
    workTime: WorkTime;
};

function RemoveWorkTimeModel({ onClose, isOpen, workTime }: Props) {
    const [Delete, { data, isLoading, error, isError: createUserError }] = useDeleteWorkTimeMutation();
    const toast = useToast();

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
        Delete(workTime.publicId);
    };

    return (
        <>
            <Modal isCentered onClose={onClose} isOpen={isOpen} motionPreset="slideInBottom">
                <ModalOverlay />
                <ModalContent>
                    <ModalHeader>confirm</ModalHeader>
                    <ModalCloseButton />
                    <ModalBody>
                        <Text>confirm delete Work Time</Text>
                    </ModalBody>
                    <ModalFooter>
                        <Button colorScheme="red" mr={3} isLoading={isLoading} onClick={DeleteWorkTime}>
                            Delete
                        </Button>
                        <Button colorScheme="gray" mr={3} onClick={onClose}>
                            Close
                        </Button>
                    </ModalFooter>
                </ModalContent>
            </Modal>
        </>
    );
}

export default RemoveWorkTimeModel;