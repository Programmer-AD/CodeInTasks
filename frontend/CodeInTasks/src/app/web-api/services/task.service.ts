import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import TaskServiceInterface from '../interfaces/task-service-interface';
import TaskCreateModel from '../models/task/task-create-model';
import TaskCreateResultModel from '../models/task/task-create-result-model';
import TaskFilterModel from '../models/task/task-filter-model';
import TaskUpdateModel from '../models/task/task-update-model';
import TaskViewModel from '../models/task/task-view-model';

@Injectable()
export default class TaskService implements TaskServiceInterface {
  private static readonly basePath = "/api/task";

  constructor(private httpClient: HttpClient) { }

  public getAsync(taskId: string): Observable<TaskViewModel> {
    return this.httpClient.get<TaskViewModel>(`${TaskService.basePath}/${taskId}`);
  }

  public getFilteredAsync(filterModel: TaskFilterModel): Observable<TaskViewModel[]> {
    return this.httpClient.get<TaskViewModel[]>(TaskService.basePath, { params: <any>filterModel });
  }

  public addAsync(createModel: TaskCreateModel): Observable<TaskCreateResultModel> {
    return this.httpClient.post<TaskCreateResultModel>(TaskService.basePath, createModel);
  }

  public updateAsync(taskId: string, updateModel: TaskUpdateModel): Observable<unknown> {
    return this.httpClient.put(`${TaskService.basePath}/${taskId}`, updateModel);
  }

  public deleteAsync(taskId: string): Observable<unknown> {
    return this.httpClient.delete(`${TaskService.basePath}/${taskId}`);
  }
}
