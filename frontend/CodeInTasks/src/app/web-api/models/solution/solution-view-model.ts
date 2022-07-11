import TaskSolutionResult from "../../enums/task-solution-result";
import TaskSolutionStatus from "../../enums/task-solution-status";

export default class SolutionViewModel {
    public id: string = null!;
    public repositoryUrl: string = null!;

    public status: TaskSolutionStatus = null!;
    public result: TaskSolutionResult | null = null;
    public errorCode: string | null = null;
    public resultAdditionalInfo: string | null = null;

    public sendTime: Date = null!;
    public finishTime: Date | null = null;
    public runTimeMs: number | null = null;

    public taskId: string = null!;
    public senderId: string = null!;
}
