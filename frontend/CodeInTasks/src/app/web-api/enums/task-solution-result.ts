enum TaskSolutionResult {
    unknown,

    downloadError,
    buildError,
    runError,

    failed,
    completed
}

export default TaskSolutionResult;
