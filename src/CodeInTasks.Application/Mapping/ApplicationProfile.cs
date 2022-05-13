using AutoMapper;
using CodeInTasks.Application.Dtos.Solution;
using CodeInTasks.Application.Dtos.Task;
using CodeInTasks.Application.Dtos.User;

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
            CreateBaseDtoMaps<SolutionCreateDto, Solution, SolutionViewDto>();

            CreateMap<SolutionStatusUpdateDto, Solution>();
            CreateMap<Solution, SolutionQueueDto>();
        }

        private void CreateTaskMaps()
        {
            CreateBaseDtoMaps<TaskCreateDto, TaskModel, TaskViewDto>();

            CreateMap<TaskUpdateDto, TaskModel>();
        }

        private void CreateUserMaps()
        {
            CreateBaseDtoMaps<UserCreateDto, User, UserViewDto>();
        }

        private void CreateBaseDtoMaps<TCreateDto, TEntity, TViewDto>(
            Action<IMappingExpression<TCreateDto, TEntity>> configureCreate = null,
            Action<IMappingExpression<TEntity, TViewDto>> configureView = null)
        {
            var createMapping = CreateMap<TCreateDto, TEntity>();
            configureCreate?.Invoke(createMapping);

            var viewMapping = CreateMap<TEntity, TViewDto>();
            configureView?.Invoke(viewMapping);
        }
    }
}
