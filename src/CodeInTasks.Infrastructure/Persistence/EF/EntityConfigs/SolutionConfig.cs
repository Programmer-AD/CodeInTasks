using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeInTasks.Infrastructure.Persistence.EF.EntityConfigs
{
    internal class SolutionConfig : IEntityTypeConfiguration<Solution>
    {
        public void Configure(EntityTypeBuilder<Solution> builder)
        {
            builder.Property(x => x.RepositoryName)
                .HasMaxLength(DomainConstants.RepositoryName_MaxLength)
                .IsRequired();

            builder.Property(x => x.ResultAdditionalInfo)
                .HasMaxLength(DomainConstants.TaskSolution_ResultAdditionalInfo_MaxLength);

            builder.HasOne(x => x.Task).WithMany();

            builder.HasOne(x => x.Sender).WithMany();

            builder.HasIndex(x => x.SendTime).IsClustered(false);
        }
    }
}
