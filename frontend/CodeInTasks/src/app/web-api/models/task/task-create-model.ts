import { RunnerType, TaskCategory } from "../../enums";


export class TaskCreateModel {
    public title: string = null!;
    public description: string = null!;

    public category: TaskCategory = null!;
    public runner: RunnerType = null!;

    public baseRepositoryUrl: string = null!;

    public testRepositoryUrl: string = null!;
    public testRepositoryAuthPassword: string = null!;

}
