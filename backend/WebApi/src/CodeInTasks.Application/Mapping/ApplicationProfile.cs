using AutoMapper;
using CodeInTasks.WebApi.Models.Solution;
using CodeInTasks.WebApi.Models.Task;
using CodeInTasks.WebApi.Models.User;

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
            CreateMap<SolutionCreateModel, Solution>();

            CreateMap<SolutionStatusUpdateModel, Solution>();
            CreateMap<Solution, SolutionQueueModel>();
        }

        private void CreateTaskMaps()
        {
            CreateMap<TaskCreateModel, TaskModel>();

            CreateMap<TaskUpdateModel, TaskModel>();
        }

        private void CreateUserMaps()
        {
            CreateMap<UserCreateModel, UserData>();
        }
    }
}
