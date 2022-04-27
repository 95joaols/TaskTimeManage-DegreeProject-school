import type { WorkTime } from "WorkTime";
export interface WorkItem {
    publicId?: string;
    name: string;
    userId: string;
    WorkTimes?: WorkTime[];
}
