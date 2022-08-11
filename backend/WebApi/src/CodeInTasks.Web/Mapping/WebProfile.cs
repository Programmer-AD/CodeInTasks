using AutoMapper;
using CodeInTasks.Application.Abstractions.Models.Solution;
using CodeInTasks.Application.Abstractions.Models.Task;
using CodeInTasks.Application.Abstractions.Models.User;
using CodeInTasks.Domain.Models;
using CodeInTasks.WebApi.Models.Solution;
using CodeInTasks.WebApi.Models.Task;
using CodeInTasks.WebApi.Models.User;

namespace CodeInTasks.Web.Mapping
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<Solution, SolutionViewModel>();
            CreateMap<TaskModel, TaskViewModel>();
            CreateMap<UserData, UserViewModel>();
        }
    }
}
