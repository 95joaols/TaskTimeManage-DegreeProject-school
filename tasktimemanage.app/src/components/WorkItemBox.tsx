import { Box, Text } from "@chakra-ui/react";
import React, { useEffect, useState } from "react";
import { WorkItem } from "../Types/WorkItem";

type Props = {
    onPress: (id: string) => void;
    workItem: WorkItem;
    activeWorkItem: string | undefined;
};

function WorkItemBox({ workItem, onPress, activeWorkItem }: Props) {
    const [Color, setColor] = useState("green");

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
                onPress(workItem.publicId!);
            }}
        >
            <Text color={"white"}>{workItem.name}</Text>
        </Box>
    );
}

export default WorkItemBox;
