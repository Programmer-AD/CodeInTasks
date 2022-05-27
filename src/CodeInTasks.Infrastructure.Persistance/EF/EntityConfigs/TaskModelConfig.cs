using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeInTasks.Infrastructure.Persistance.EF.EntityConfigs
{
    internal class TaskModelConfig : IEntityTypeConfiguration<TaskModel>
    {
        public void Configure(EntityTypeBuilder<TaskModel> builder)
        {
            builder.Property(x => x.Title)
                .HasMaxLength(DomainConstants.TaskModel_Title_MaxLength)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(DomainConstants.TaskModel_Description_MaxLength)
                .IsRequired();

            builder.Property(x => x.BaseRepositoryUrl)
                .HasMaxLength(DomainConstants.RepositoryUrl_MaxLength)
                .IsRequired();

            builder.Property(x => x.TestRepositoryUrl)
                .HasMaxLength(DomainConstants.RepositoryUrl_MaxLength)
                .IsRequired();

            builder.Property(x => x.TestRepositoryAccessToken)
                .HasMaxLength(DomainConstants.RepositoryAccessToken_MaxLength)
                .IsRequired();

            builder.HasOne(x => x.Creator).WithMany();

            builder.HasIndex(x => x.CreateDate).IsClustered(false);
        }
    }
}
