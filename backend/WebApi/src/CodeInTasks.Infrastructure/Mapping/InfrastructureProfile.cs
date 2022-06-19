using AutoMapper;
using CodeInTasks.Application.Abstractions.Dtos.User;

namespace CodeInTasks.Infrastructure.Mapping
{
    public class InfrastructureProfile : Profile
    {
        public InfrastructureProfile()
        {
            CreateUserMaps();
        }

        private void CreateUserMaps()
        {
            CreateMap<UserCreateDto, UserData>();
        }
    }
}
