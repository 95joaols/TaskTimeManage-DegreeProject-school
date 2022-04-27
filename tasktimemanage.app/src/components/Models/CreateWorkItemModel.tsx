import { FormControl, FormErrorMessage, FormLabel } from "@chakra-ui/form-control";
import {
    Modal,
    ModalBody,
    ModalCloseButton,
    ModalContent,
    ModalFooter,
    ModalHeader,
    ModalOverlay,
} from "@chakra-ui/modal";
import { Button, Input, useToast } from "@chakra-ui/react";
import React, { useEffect, useRef, useState } from "react";
import { useCreateWorkItemMutation } from "../../store/api/WorkApi";
import { useAppSelector } from "../../store/hook";
import { selectLoginUser } from "../../store/state/authSlice";

type Props = {
    onClose: () => void;
    isOpen: boolean;
};

function CreateWorkItemModel({ onClose, isOpen }: Props) {
    const user = useAppSelector(selectLoginUser);
    const firstUpdate = useRef(true);
    const [name, setName] = useState("");
    const [isError, setIsError] = useState(false);
    const handleInputChange = (e: any) => setName(e.target.value);
    const [createWorkItemApi, { data, isLoading, error, isError: createUserError }] = useCreateWorkItemMutation();
    const toast = useToast();

    useEffect(() => {
        if (firstUpdate.current) {
            firstUpdate.current = false;
            return;
        }
        setIsError(name === "");
    }, [name]);

    useEffect(() => {
        if (data) {
            CustomOnClose();
        }
    }, [data]);

    useEffect(() => {
        if (createUserError) {
            toast({
                title: (error as any).data.title,
                status: "error",
                duration: 5000,
            });
        }
    }, [createUserError, error]);

    const CustomOnClose = () => {
        setName("");
        setIsError(false);
        firstUpdate.current = true;
        onClose();
    };

    const CreateWorkItem = () => {
        if (name && user.id) {
            createWorkItemApi({ name, userId: user.id! });
        } else {
            setIsError(true);
        }
    };

    return (
        <Modal isCentered onClose={CustomOnClose} isOpen={isOpen} motionPreset="slideInBottom">
            <ModalOverlay />
            <ModalContent>
                <ModalHeader>Create WorkItem</ModalHeader>
                <ModalCloseButton />
                <ModalBody>
                    <FormControl isInvalid={isError} isRequired>
                        <FormLabel htmlFor="Name">Name</FormLabel>
                        <Input id="Name" type="text" value={name} onChange={handleInputChange} />
                        {isError && <FormErrorMessage>Name is required.</FormErrorMessage>}
                    </FormControl>
                </ModalBody>
                <ModalFooter>
                    <Button colorScheme="purple" mr={3} isLoading={isLoading} onClick={CreateWorkItem}>
                        Create
                    </Button>
                    <Button colorScheme="red" mr={3} onClick={CustomOnClose}>
                        Close
                    </Button>
                </ModalFooter>
            </ModalContent>
        </Modal>
    );
}

export default CreateWorkItemModel;
