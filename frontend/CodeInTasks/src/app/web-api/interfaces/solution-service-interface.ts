import { Observable } from "rxjs";
import { SolutionCreateModel, SolutionCreateResultModel, SolutionFilterModel, SolutionViewModel } from "../models/solution";

export abstract class SolutionServiceInterface {
    public abstract addAsync(createModel: SolutionCreateModel): Observable<SolutionCreateResultModel>;

    public abstract getAsync(solutionId: string): Observable<SolutionViewModel>;

    public abstract getFilteredAsync(filterModel: SolutionFilterModel): Observable<SolutionViewModel[]>;
}
