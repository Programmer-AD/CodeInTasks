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
                .HasMaxLength(DomainConstants.TaskModel_Description_MaxLength);

            builder.Property(x => x.BaseRepositoryName)
                .HasMaxLength(DomainConstants.RepositoryName_MaxLength)
                .IsRequired();

            builder.Property(x => x.TestRepositoryName)
                .HasMaxLength(DomainConstants.RepositoryName_MaxLength)
                .IsRequired();

            builder.HasOne(x => x.Creator).WithMany();

            builder.HasIndex(x => x.CreateDate).IsClustered(false);
        }
    }
}
