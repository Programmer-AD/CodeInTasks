namespace CodeInTasks.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task SetBannedAsync(Guid id, bool isBanned);
    }
}
