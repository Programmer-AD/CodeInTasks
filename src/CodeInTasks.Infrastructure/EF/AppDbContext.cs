using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeInTasks.Infrastructure.EF
{
    internal class AppDbContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<TaskSolution> TaskSolutions { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
