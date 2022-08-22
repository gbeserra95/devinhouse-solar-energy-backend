using DEVinHouse.SolarEnergy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DEVinHouse.SolarEnergy.Data.Mappings
{
	public class GenerationMap : IEntityTypeConfiguration<Generation>
	{
		public void Configure(EntityTypeBuilder<Generation> builder)
		{
			builder.HasKey(g => g.Id);

			builder.HasIndex(g => new {g.Date, g.PlantId})
				.IsUnique();

			builder.Property(g => g.Date)
				.HasColumnType("date")
				.IsRequired();

			builder.Property(g => g.MonthlyConsumption)
				.HasColumnType("decimal(18,2)")
				.IsRequired();

			builder.Property(g => g.DailyAverage)
				.HasColumnType("decimal(18,2)")
				.IsRequired();

			builder.HasOne(g => g.Plant)
				.WithMany(p => p.Generations)
				.HasForeignKey(g => g.PlantId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(g => g.User)
				.WithMany()
				.HasForeignKey(g => g.UserId)
				.OnDelete(DeleteBehavior.NoAction);
   		}
  	}
}