import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TaskServiceInterface } from '../interfaces';
import { TaskCreateModel, TaskCreateResultModel, TaskFilterModel, TaskUpdateModel, TaskViewModel } from '../models';

@Injectable()
export class TaskService implements TaskServiceInterface {
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
