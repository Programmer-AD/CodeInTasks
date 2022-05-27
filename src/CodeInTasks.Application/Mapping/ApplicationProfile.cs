using AutoMapper;
using CodeInTasks.Application.Abstractions.Dtos.Solution;
using CodeInTasks.Application.Abstractions.Dtos.Task;
using CodeInTasks.Application.Abstractions.Dtos.User;

namespace CodeInTasks.Application.Mapping
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateSolutionMaps();
            CreateTaskMaps();
            CreateUserMaps();
        }

        private void CreateSolutionMaps()
        {
            CreateMap<SolutionCreateDto, Solution>();

            CreateMap<SolutionStatusUpdateDto, Solution>();
            CreateMap<Solution, SolutionQueueDto>();
        }

        private void CreateTaskMaps()
        {
            CreateMap<TaskCreateDto, TaskModel>();

            CreateMap<TaskUpdateDto, TaskModel>();
        }

        private void CreateUserMaps()
        {
            CreateMap<UserCreateDto, UserData>();
        }
    }
}
