import { Box, Center, Heading } from "@chakra-ui/layout";
import { Text } from "@chakra-ui/react";
import React, { useEffect, useState } from "react";
import { useLazyGetWorkItemQuery } from "../store/api/WorkApi";

type Props = {
    activeWorkItem: string | undefined;
};

function WorkItemControl({ activeWorkItem }: Props) {
    const [LastActive, setLastActive] = useState<string>();
    const [trigger, result, lastPromiseInfo] = useLazyGetWorkItemQuery();

    console.log("result", result, "activeWorkItem", activeWorkItem);

    useEffect(() => {
        if (activeWorkItem) {
            if (!LastActive || activeWorkItem !== LastActive) {
                setLastActive(activeWorkItem);
                trigger(activeWorkItem);
            }
        }
    }, [activeWorkItem]);

    return (
        <Box>
            <Center>
                <Heading as="h1" size="lg">
                    {result.isUninitialized && !activeWorkItem && <Text>No Selected</Text>}
                    {result.data && result.data.name}
                </Heading>
            </Center>
        </Box>
    );
}

export default WorkItemControl;
