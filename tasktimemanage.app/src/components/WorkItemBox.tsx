import { Box, Text } from '@chakra-ui/react';
import { useEffect, useState } from 'react';
import { WorkItem } from "../Types/WorkItem";

type Props = {
    onPress: (id: string) => void;
    workItem: WorkItem
    activeWorkItem: string | undefined

};

function WorkItemBox({ workItem, onPress, activeWorkItem }: Props) {
    const [borderColor, setborderColor] = useState("green")
        
    useEffect(() => {
        const isactive = workItem.publicId === activeWorkItem
        if (isactive)
        {
            setborderColor("red");
        }
        else
        {
            setborderColor("green");
            }

    }, [activeWorkItem])
    

    return (
        <Box key={workItem.publicId} bg={"blue"} p={5} onClick={() => { console.log("Click", workItem.publicId!); onPress(workItem.publicId!)}} border="5px solid" borderColor={borderColor} >
            <Text>{workItem.name}</Text>
        </Box>
    )
}

export default WorkItemBox;