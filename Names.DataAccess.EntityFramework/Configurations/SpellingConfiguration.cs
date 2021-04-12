using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Names.Domain.Entities;

namespace Names.DataAccess.EntityFramework.Configurations
{
	public class SpellingConfiguration : IEntityTypeConfiguration<SpellingRecord>
	{
		public void Configure(EntityTypeBuilder<SpellingRecord> builder)
		{
			builder.HasKey(o => new { o.CommonNameId, o.VariationNameId });
		}
	}
}
