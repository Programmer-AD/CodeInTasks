﻿namespace CodeInTasks.Application.Abstractions.Dtos.Task
{
    public class TaskUpdateDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public TaskCategory Category { get; set; }

        public RunnerType Runner { get; set; }
        public string BaseRepositoryUrl { get; set; }
        public string TestRepositoryUrl { get; set; }
        public string TestRepositoryAccessToken { get; set; }
    }
}