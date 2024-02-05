using Domain.Entities.Ships;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal class ShipConfiguration : IEntityTypeConfiguration<Ship>
{
    public void Configure(EntityTypeBuilder<Ship> builder)
    {
        builder
            .HasKey(s => s.Code);

        builder
            .Property(s => s.Code)
            .IsRequired(true)
            .HasMaxLength(12)
            .HasConversion(
                id => id.Value,
                value => Code.Create(value)
                );

        builder
            .Property(s => s.Name)
            .IsRequired(true);

        builder
            .Property(s => s.Length)
            .IsRequired(true);

        builder
            .Property(s => s.Width)
            .IsRequired(true);
    }
}
