using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeInTasks.Infrastructure.Persistance.EF.EntityConfigs
{
    internal class SolutionConfig : IEntityTypeConfiguration<Solution>
    {
        public void Configure(EntityTypeBuilder<Solution> builder)
        {
            builder.Property(x => x.RepositoryUrl)
                .HasMaxLength(DomainConstants.RepositoryUrl_MaxLength)
                .IsRequired();

            builder.Property(x => x.RepositoryAuthPassword)
                .HasMaxLength(DomainConstants.RepositoryAuthPassword_MaxLength);

            builder.Property(x => x.ErrorCode)
                .HasMaxLength(DomainConstants.Solution_ErrorCode_MaxLength);

            builder.Property(x => x.ResultAdditionalInfo)
                .HasMaxLength(DomainConstants.Solution_ResultAdditionalInfo_MaxLength);

            builder.HasOne(x => x.Task).WithMany().OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Sender).WithMany().OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(x => x.SendTime).IsClustered(false);
        }
    }
}
