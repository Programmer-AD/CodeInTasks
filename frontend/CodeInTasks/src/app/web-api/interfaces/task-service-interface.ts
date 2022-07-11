import { Observable } from "rxjs";
import TaskCreateModel from "../models/task/task-create-model";
import TaskCreateResultModel from "../models/task/task-create-result-model";
import TaskFilterModel from "../models/task/task-filter-model";
import TaskUpdateModel from "../models/task/task-update-model";
import TaskViewModel from "../models/task/task-view-model";

export default interface TaskServiceInterface {
    getAsync(taskId: string): Observable<TaskViewModel>;

    getFilteredAsync(filterModel: TaskFilterModel): Observable<TaskViewModel[]>;

    addAsync(createModel: TaskCreateModel): Observable<TaskCreateResultModel>;

    updateAsync(updateModel: TaskUpdateModel): Observable<void>;

    deleteAsync(taskId: string): Observable<void>;
}
