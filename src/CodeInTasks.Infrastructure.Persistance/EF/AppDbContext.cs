using CodeInTasks.Infrastructure.Persistance.IdentityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeInTasks.Infrastructure.Persistance.EF
{
    public class AppDbContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<Solution> Solutions { get; set; }
        public DbSet<UserData> UserDatas { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
