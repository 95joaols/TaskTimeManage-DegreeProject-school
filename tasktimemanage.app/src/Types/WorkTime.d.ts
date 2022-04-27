export interface WorkTime {
    id: number;
    time: string;
    type: WorkTimeType;
}

export enum WorkTimeType {
    Start,
    End,
}
