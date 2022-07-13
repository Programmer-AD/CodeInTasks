import { Observable } from "rxjs";
import TaskCreateModel from "../models/task/task-create-model";
import TaskCreateResultModel from "../models/task/task-create-result-model";
import TaskFilterModel from "../models/task/task-filter-model";
import TaskUpdateModel from "../models/task/task-update-model";
import TaskViewModel from "../models/task/task-view-model";

export default abstract class TaskServiceInterface {
    public abstract getAsync(taskId: string): Observable<TaskViewModel>;

    public abstract getFilteredAsync(filterModel: TaskFilterModel): Observable<TaskViewModel[]>;

    public abstract addAsync(createModel: TaskCreateModel): Observable<TaskCreateResultModel>;

    public abstract updateAsync(taskId: string, updateModel: TaskUpdateModel): Observable<unknown>;

    public abstract deleteAsync(taskId: string): Observable<unknown>;
}
