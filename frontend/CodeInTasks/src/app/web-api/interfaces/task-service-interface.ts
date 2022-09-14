import { Observable } from "rxjs";
import { TaskCreateModel, TaskCreateResultModel, TaskFilterModel, TaskUpdateModel, TaskViewModel } from "../models/task"

export abstract class TaskServiceInterface {
    public abstract getAsync(taskId: string): Observable<TaskViewModel>;

    public abstract getFilteredAsync(filterModel: TaskFilterModel): Observable<TaskViewModel[]>;

    public abstract addAsync(createModel: TaskCreateModel): Observable<TaskCreateResultModel>;

    public abstract updateAsync(taskId: string, updateModel: TaskUpdateModel): Observable<unknown>;

    public abstract deleteAsync(taskId: string): Observable<unknown>;
}
