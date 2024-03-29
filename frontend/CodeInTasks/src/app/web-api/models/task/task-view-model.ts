import { RunnerType, TaskCategory } from "../../enums";

export class TaskViewModel {
    public id: string = null!;

    public title: string = null!;
    public description: string = null!;
    public category: TaskCategory = null!;
    public createDate: Date = null!;

    public runner: RunnerType = null!;

    public baseRepositoryUrl: string = null!;
    public testRepositoryUrl: string = null!;
    public testRepositoryAuthPassword: string = null!;

    public creatorId: string = null!;
}
