﻿using AccidentMonitoring.Domain.Entities.Accident;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccidentMonitoring.Infrastructure.Data.Configurations.Accident;
public class AccidentDetailsConfiguration : IEntityTypeConfiguration<AccidentDetails>
{
    public void Configure(EntityTypeBuilder<AccidentDetails> builder)
    {
        builder.HasKey(v => v.Guid);
        builder.Property(v => v.Guid)
            .HasColumnName("Guid")
            .ValueGeneratedOnAdd();
    }
}
