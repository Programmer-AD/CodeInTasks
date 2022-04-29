using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CodeInTasks.Infrastructure.EF
{
    internal class AppDbContext : IdentityDbContext<User, Role, Guid>
    {
        private readonly EfDbOptions dbOptions;

        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<TaskSolution> TaskSolutions { get; set; }

        public AppDbContext(IOptions<EfDbOptions> dbOptions)
        {
            this.dbOptions = dbOptions.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(
                dbOptions.ConnectionString,
                options => options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
