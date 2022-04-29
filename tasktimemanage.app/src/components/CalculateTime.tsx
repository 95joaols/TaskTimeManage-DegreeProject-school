import { Box } from "@chakra-ui/layout";
import { Text } from "@chakra-ui/react";
import React, { useEffect, useState } from "react";
import { WorkTime } from "../Types/WorkTime";

type Props = {
    WorkTimes: WorkTime[] | undefined;
};
function CalculateTime({ WorkTimes }: Props) {
    const [time, setTime] = useState(0);
    const [timeText, setTimeText] = useState("0:00");
    const [domby, setDomby] = useState(false);
    const pad = (n: number) => {
        return n < 10 ? "0" + n : n;
    };

    useEffect(() => {
        setTimeText(`${Math.floor(time / (1000 * 60 * 60))}:${pad(Math.floor(((time / (1000 * 60)) % 60) * 1.66))}`);
    }, [time]);

    useEffect(() => {
        let totalTime = 0;
        if (WorkTimes) {
            if (WorkTimes.length > 1) {
                for (let i = 0; i < WorkTimes.length; i += 2) {
                    if (WorkTimes[i + 1]) {
                        totalTime += new Date(WorkTimes[i + 1].time).getTime() - new Date(WorkTimes[i].time).getTime();
                    }
                }
            }
            if (WorkTimes.length % 2 === 1) {
                totalTime += new Date().getTime() - new Date(WorkTimes[WorkTimes.length - 1].time).getTime();
            }
        }

        setTime(totalTime);
    }, [domby, WorkTimes]);

    useEffect(() => {
        if (WorkTimes && WorkTimes.length % 2 === 1) {
            const intervalId = setInterval(() => {
                setDomby((data) => (data = !data));
            }, 1000);
            return () => clearInterval(intervalId); //This is important
        }
    }, [WorkTimes]);

    return <Box>{WorkTimes && <Text>{timeText}</Text>}</Box>;
}

export default CalculateTime;
