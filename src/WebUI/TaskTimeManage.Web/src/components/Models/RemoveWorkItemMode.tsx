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
import { useDeleteWorkItemMutation } from "../../store/api/WorkApi";

type Props = {
    onClose: () => void;
    onDeleted: () => void;

    isOpen: boolean;
    activeWorkItem: string;
};

function RemoveWorkItemMode({ onClose, onDeleted, isOpen, activeWorkItem }: Props) {
    const [Delete, { data, isLoading, error, isError: createUserError }] = useDeleteWorkItemMutation();
    const toast = useToast();

    useEffect(() => {
        if (createUserError) {
            console.log("error", error);

            toast({
                title: (error as any).data.title,
                status: "error",
                duration: 5000,
            });
        }
    }, [createUserError, error]);

    useEffect(() => {
        console.log("data", data);

        if (data) {
            onClose();
            onDeleted();
        }
    }, [data]);

    const DeleteWorkTime = () => {
        Delete(activeWorkItem);
    };

    return (
        <>
            <Modal isCentered onClose={onClose} isOpen={isOpen} motionPreset="slideInBottom">
                <ModalOverlay />
                <ModalContent>
                    <ModalHeader>confirm</ModalHeader>
                    <ModalCloseButton />
                    <ModalBody>
                        <Text>confirm delete Work Item</Text>
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

export default RemoveWorkItemMode;
