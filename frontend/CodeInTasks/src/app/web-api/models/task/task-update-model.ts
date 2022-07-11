import RunnerType from "../../enums/runner-type";
import TaskCategory from "../../enums/task-category";

export default class TaskUpdateModel {
    public title: string = null!;
    public description: string = null!;

    public category: TaskCategory = null!;
    public runner: RunnerType = null!;

    public baseRepositoryUrl: string = null!;

    public testRepositoryUrl: string = null!;
    public testRepositoryAuthPassword: string = null!;
}
