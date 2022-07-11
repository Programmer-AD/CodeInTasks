import { Observable } from "rxjs";
import SolutionCreateModel from "../models/solution/solution-create-model";
import SolutionCreateResultModel from "../models/solution/solution-create-result-model";
import SolutionFilterModel from "../models/solution/solution-filter-model";
import SolutionViewModel from "../models/solution/solution-view-model";

export default interface SolutionServiceInterface {
    addAsync(createModel: SolutionCreateModel): Observable<SolutionCreateResultModel>;

    getAsync(solutionId: string): Observable<SolutionViewModel>;

    getFilteredAsync(filterModel: SolutionFilterModel): Observable<SolutionViewModel[]>;
}
