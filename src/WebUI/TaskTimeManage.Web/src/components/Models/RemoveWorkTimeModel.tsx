import {
    Modal,
    ModalBody,
    ModalCloseButton,
    ModalContent,
    ModalFooter,
    ModalHeader,
    ModalOverlay,
} from "@chakra-ui/modal";
import { Button, Text } from "@chakra-ui/react";
import React, { useEffect } from "react";
import UseMessage from "../../Hooks/UseMessage";
import { useDeleteWorkTimeMutation } from "../../store/api/WorkApi";
import { WorkTime } from "../../Types/WorkTime";

type Props = {
    onClose: () => void;
    isOpen: boolean;
    workTime: WorkTime;
};

function RemoveWorkTimeModel({ onClose, isOpen, workTime }: Props) {
    const [Delete, { data, isLoading, error, isError: createUserError }] = useDeleteWorkTimeMutation();
    const message = UseMessage();

    useEffect(() => {
        if (createUserError) {
            message({ errorOrMessage: error, type: "error", objectType: "object" });
        }
    }, [createUserError, error]);

    useEffect(() => {
        if (data) {
            message({ errorOrMessage: "Deleted", type: "success", objectType: "text" });
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
