using DEVinHouse.SolarEnergy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DEVinHouse.SolarEnergy.Data.Mappings
{
  public class GenerationMap : IEntityTypeConfiguration<Generation>
  {
    public void Configure(EntityTypeBuilder<Generation> builder)
    {
      builder.Property(g => g.Date)
        .HasColumnType("date")
        .IsRequired();

      builder.Property(g => g.MonthlyConsumption)
        .HasColumnType("decimal(18,2)")
        .IsRequired();

      builder.Property(g => g.DailyAverage)
        .HasColumnType("decimal(18,2)")
        .IsRequired();

      builder.Property(g => g.PlantId)
        .IsRequired();
    }
  }
}