using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Names.Domain.Entities;

namespace Names.DataAccess.EntityFramework.Configurations
{
	public class NameWithDetailConfiguration : IEntityTypeConfiguration<NameWithDetailResult>
	{
		public void Configure(EntityTypeBuilder<NameWithDetailResult> builder)
		{
			builder.HasKey(o => new { o.NameId, o.NameDetailId });
		}
	}
}
