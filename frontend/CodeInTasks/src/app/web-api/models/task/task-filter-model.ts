import RunnerType from "../../enums/runner-type";
import TaskCategory from "../../enums/task-category";

export default class TaskFilterModel {
    public categories: TaskCategory[] = [];
    public runners: RunnerType[] = [];

    public creatorId: string | null = null;

    public takeOffset: number = null!;
    public takeCount: number = null!;
}
