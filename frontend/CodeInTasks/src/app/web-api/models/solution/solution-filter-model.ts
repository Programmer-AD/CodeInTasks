import { TaskSolutionResult } from "../../enums";

export class SolutionFilterModel {
    public results: (TaskSolutionResult | null)[] = [];
    public taskId: string | null = null;

    public takeOffset: number = null!;
    public takeCount: number = null!;
}
