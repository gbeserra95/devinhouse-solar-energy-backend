using DEVinHouse.SolarEnergy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DEVinHouse.SolarEnergy.Data.Mappings
{
  public class UnitMap : IEntityTypeConfiguration<Plant>
  {
    public void Configure(EntityTypeBuilder<Plant> builder)
    {
      builder.Property(p => p.Name)
        .HasMaxLength(150)
        .IsUnicode(false)
        .IsRequired();

    builder.Property(p => p.Address)
        .HasMaxLength(255)
        .IsUnicode(false)
        .IsRequired();

    builder.Property(p => p.Brand)
        .HasMaxLength(150)
        .IsUnicode(false)
        .IsRequired();

    builder.Property(p => p.Model)
        .HasMaxLength(150)
        .IsUnicode(false)
        .IsRequired();

    builder.Property(p => p.Active)
        .HasColumnType("bit")
        .IsRequired();

    builder.HasMany(u => u.Generations)
        .WithOne()
        .HasForeignKey(g => g.PlantId)
        .OnDelete(DeleteBehavior.Cascade);
    }
  }
}