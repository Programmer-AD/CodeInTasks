using CodeInTasks.Infrastructure.Persistance.IdentityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeInTasks.Infrastructure.Persistance.EF.EntityConfigs
{
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(x => x.UserData).WithOne()
                .HasForeignKey((User user) => user.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(x => x.UserData).AutoInclude();
        }
    }
}
