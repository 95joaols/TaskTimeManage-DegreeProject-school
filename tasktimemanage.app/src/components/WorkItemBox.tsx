import { Box, Text } from "@chakra-ui/react";
import React, { useEffect, useState } from "react";
import { WorkItem } from "../Types/WorkItem";

type Props = {
    onPress: (id: string) => void;
    workItem: WorkItem;
    activeWorkItem: string | undefined;
};

function WorkItemBox({ workItem, onPress, activeWorkItem }: Props) {
    const [borderColor, setBorderColor] = useState("green");

    useEffect(() => {
        const isActive = workItem.publicId === activeWorkItem;
        if (isActive) {
            setBorderColor("red");
        } else {
            setBorderColor("green");
        }
    }, [activeWorkItem]);

    return (
        <Box
            key={workItem.publicId}
            bg={"blue"}
            p={5}
            onClick={() => {
                onPress(workItem.publicId!);
            }}
            border="5px solid"
            borderColor={borderColor}
        >
            <Text>{workItem.name}</Text>
        </Box>
    );
}

export default WorkItemBox;
