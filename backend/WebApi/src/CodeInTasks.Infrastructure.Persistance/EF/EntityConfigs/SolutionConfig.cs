﻿using Microsoft.EntityFrameworkCore;
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

            builder.Property(x => x.RepositoryAccessToken)
                .HasMaxLength(DomainConstants.RepositoryAccessToken_MaxLength)
                .IsRequired();

            builder.Property(x => x.ResultAdditionalInfo)
                .HasMaxLength(DomainConstants.Solution_ResultAdditionalInfo_MaxLength);

            builder.HasOne(x => x.Task).WithMany();

            builder.HasOne(x => x.Sender).WithMany();

            builder.HasIndex(x => x.SendTime).IsClustered(false);
        }
    }
}