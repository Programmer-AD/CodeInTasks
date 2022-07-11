import TaskSolutionResult from "../../enums/task-solution-result";

export default class SolutionFilterModel {
    public results: (TaskSolutionResult | null)[] = [];
    public taskId: string | null = null;

    public takeOffset: number = null!;
    public takeCount: number = null!;
}
