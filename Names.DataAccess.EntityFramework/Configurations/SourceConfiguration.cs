using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Names.Domain.Entities;

namespace Names.DataAccess.EntityFramework.Configurations
{
	public class SourceConfiguration : IEntityTypeConfiguration<SourceRecord>
	{
		public void Configure(EntityTypeBuilder<SourceRecord> builder)
		{
			builder.HasMany(source => source.NameDetails).WithOne(detail => detail.Source).OnDelete(DeleteBehavior.SetNull);
		}
	}
}
