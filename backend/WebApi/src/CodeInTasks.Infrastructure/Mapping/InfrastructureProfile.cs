using AutoMapper;
using CodeInTasks.WebApi.Models.User;

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
            CreateMap<UserCreateModel, UserData>();
        }
    }
}
