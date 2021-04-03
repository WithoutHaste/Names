using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Names.Domain.Entities;

namespace Names.DataAccess.EntityFramework.Configurations
{
	public class NickNameConfiguration : IEntityTypeConfiguration<NickNameRecord>
	{
		public void Configure(EntityTypeBuilder<NickNameRecord> builder)
		{
			builder.HasKey(o => new { o.NickNameId, o.FullNameId });
		}
	}
}
