using AccidentMonitoring.Domain.Entities.Accident;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccidentMonitoring.Infrastructure.Data.Configurations.Accident;
public class AccidentDetailsConfiguration : IEntityTypeConfiguration<AccidentDetails>
{
    public void Configure(EntityTypeBuilder<AccidentDetails> builder)
    {
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id)
            .HasColumnName("Guid")
            .ValueGeneratedOnAdd();
    }
}
