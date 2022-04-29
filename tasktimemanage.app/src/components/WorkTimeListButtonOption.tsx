import { Button, ButtonGroup, useDisclosure } from "@chakra-ui/react";
import React from "react";
import { WorkTime } from "../Types/WorkTime";
import RemoveWorkTimeModel from "./Models/RemoveWorkTimeModel";

type Props = {
    workTime: WorkTime;
    activeWorkItem: string;
};

function WorkTimeListButtonOption({ workTime, activeWorkItem }: Props) {
    const { isOpen: isOpenRemove, onOpen: onOpenRemove, onClose: onCloseRemove } = useDisclosure();

    return (
        <>
            <ButtonGroup size="sm" ml={2} isAttached variant="solid">
                <Button colorScheme={"cyan"} mr="-px">
                    edit
                </Button>
                <Button colorScheme={"red"} onClick={onOpenRemove} mr="-px">
                    Remove
                </Button>
            </ButtonGroup>
            <RemoveWorkTimeModel
                isOpen={isOpenRemove}
                onClose={onCloseRemove}
                workTime={workTime}
                activeWorkItem={activeWorkItem}
            />
        </>
    );
}

export default WorkTimeListButtonOption;
