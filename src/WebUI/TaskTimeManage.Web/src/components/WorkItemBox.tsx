import { Box, Text } from "@chakra-ui/react";
import React, { useEffect, useState } from "react";
import { useAppDispatch, useAppSelector } from "../store/hook";
import { selectActiveWorkItemId, setSelectedWorkItemId } from "../store/state/workItemSlice";
import { WorkItem } from "../Types/WorkItem";

type Props = {
    workItem: WorkItem;
};

function WorkItemBox({ workItem }: Props) {
    const [Color, setColor] = useState("green");
    const dispatch = useAppDispatch();
    const activeWorkItem = useAppSelector(selectActiveWorkItemId);

    useEffect(() => {
        const isActive = workItem.publicId === activeWorkItem;
        if (isActive) {
            setColor("purple");
        } else {
            setColor("blueviolet");
        }
    }, [activeWorkItem]);

    return (
        <Box
            key={workItem.publicId}
            bg={Color}
            p={5}
            onClick={() => {
                if (workItem.publicId) {
                    dispatch(setSelectedWorkItemId({ newActiveWorkItemId: workItem.publicId }));
                }
            }}
        >
            <Text color={"white"}>{workItem.name}</Text>
        </Box>
    );
}

export default WorkItemBox;
