import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import SolutionServiceInterface from '../interfaces/solution-service-interface';
import SolutionCreateModel from '../models/solution/solution-create-model';
import SolutionCreateResultModel from '../models/solution/solution-create-result-model';
import SolutionFilterModel from '../models/solution/solution-filter-model';
import SolutionViewModel from '../models/solution/solution-view-model';

@Injectable()
export default class SolutionService implements SolutionServiceInterface {
  private static readonly basePath = "/api/solution";

  constructor(private httpClient: HttpClient) { }

  public addAsync(createModel: SolutionCreateModel): Observable<SolutionCreateResultModel> {
    return this.httpClient.post<SolutionCreateResultModel>(SolutionService.basePath, createModel);
  }

  public getAsync(solutionId: string): Observable<SolutionViewModel> {
    return this.httpClient.get<SolutionViewModel>(`${SolutionService.basePath}/${solutionId}`)
  }

  public getFilteredAsync(filterModel: SolutionFilterModel): Observable<SolutionViewModel[]> {
    return this.httpClient.get<SolutionViewModel[]>(SolutionService.basePath, { params: <any>filterModel });
  }
}
