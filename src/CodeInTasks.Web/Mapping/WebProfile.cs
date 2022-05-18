using AutoMapper;
using CodeInTasks.Application.Abstractions.Dtos.Solution;
using CodeInTasks.Application.Abstractions.Dtos.Task;
using CodeInTasks.Application.Abstractions.Dtos.User;
using CodeInTasks.Web.Models.Solution;
using CodeInTasks.Web.Models.Task;
using CodeInTasks.Web.Models.User;

namespace CodeInTasks.Web.Mapping
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateSolutionMaps();
            CreateTaskMaps();
            CreateUserMaps();
        }

        private void CreateSolutionMaps()
        {
            CreateMap<SolutionCreateModel, SolutionCreateDto>();
            CreateMap<SolutionViewDto, SolutionViewModel>();

            CreateMap<SolutionFilterModel, SolutionFilterDto>();
            CreateMap<SolutionStatusUpdateModel, SolutionStatusUpdateDto>();
        }

        private void CreateTaskMaps()
        {
            CreateMap<TaskCreateModel, TaskCreateDto>();
            CreateMap<TaskViewDto, TaskViewModel>();

            CreateMap<TaskFilterModel, TaskFilterDto>();
            CreateMap<TaskUpdateModel, TaskUpdateDto>();
        }

        private void CreateUserMaps()
        {
            CreateMap<UserCreateModel, UserCreateDto>();
            CreateMap<UserViewDto, UserViewModel>();
        }
    }
}
