import { RunnerType, TaskCategory } from "../../enums";

export class TaskFilterModel {
    public categories: TaskCategory[] = [];
    public runners: RunnerType[] = [];

    public creatorId: string | null = null;

    public takeOffset: number = null!;
    public takeCount: number = null!;
}
