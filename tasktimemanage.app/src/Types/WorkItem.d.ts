import type { WorkTime } from "WorkTime";
export declare interface WorkItem {
    publicId?: string;
    name: string;
    userId: string;
    workTimes?: WorkTime[];
}
