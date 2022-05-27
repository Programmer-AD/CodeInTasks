﻿using CodeInTasks.Application.Abstractions.Dtos.Solution;
using CodeInTasks.Application.Abstractions.Interfaces.Infrastructure.Persistance;
using CodeInTasks.Infrastructure.Queues.Shared.Models;
using StackExchange.Redis;

namespace CodeInTasks.Infrastructure.Queues
{
    internal class SolutionCheckQueue : ISolutionCheckQueue
    {
        private readonly IDatabase redisDatabase;
        private readonly IRepository<TaskModel> taskRepository;
        private readonly IJsonSerializer jsonSerializer;

        public SolutionCheckQueue(
            IDatabase redisDatabase,
            IRepository<TaskModel> taskRepository,
            IJsonSerializer jsonSerializer)
        {
            this.redisDatabase = redisDatabase;
            this.taskRepository = taskRepository;
            this.jsonSerializer = jsonSerializer;
        }

        public async Task EnqueueSolutionCheck(SolutionQueueDto solution)
        {
            var message = await MakeMessageAsync(solution);
            var messageJson = jsonSerializer.Serialize(message);

            await redisDatabase.StreamAddAsync(
                QueueConstants.SolutionStreamName,
                QueueConstants.DataFieldName,
                messageJson,
                maxLength: QueueConstants.SolutionStreamMaxLength,
                useApproximateMaxLength: true,
                flags: CommandFlags.FireAndForget);
        }

        internal async Task<SolutionCheckQueueMessage> MakeMessageAsync(SolutionQueueDto solution)
        {
            var taskId = solution.TaskId;
            var task = await GetTaskAsync(taskId);

            var message = new SolutionCheckQueueMessage()
            {
                SolutionId = solution.Id,
                Runner = task.Runner,

                TestRepositoryUrl = task.TestRepositoryUrl,
                TestRepositoryAccessToken = task.TestRepositoryAccessToken,

                UserRepositoryUrl = solution.RepositoryUrl,
                UserRepositoryAccessToken = solution.RepositoryAccessToken,
            };

            return message;
        }

        private async Task<TaskModel> GetTaskAsync(Guid taskId)
        {
            var taskModel = await taskRepository.GetAsync(taskId);

            return taskModel ?? throw new EntityNotFoundException(nameof(TaskModel), taskId);
        }
    }
}