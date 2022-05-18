using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeInTasks.Infrastructure.Persistance.EF.EntityConfigs
{
    internal class UserDataConfig : IEntityTypeConfiguration<UserData>
    {
        public void Configure(EntityTypeBuilder<UserData> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(DomainConstants.UserData_Name_MaxLength)
                .IsRequired();
        }
    }
}
