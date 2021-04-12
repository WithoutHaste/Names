using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Names.Domain.Entities;

namespace Names.DataAccess.EntityFramework.Configurations
{
	public class NameConfiguration : IEntityTypeConfiguration<NameRecord>
	{
		public void Configure(EntityTypeBuilder<NameRecord> builder)
		{
			builder.HasMany(name => name.Details).WithOne(detail => detail.Name).OnDelete(DeleteBehavior.SetNull);
			builder.HasMany(name => name.AsCommonSpellings).WithOne(spelling => spelling.CommonName).OnDelete(DeleteBehavior.SetNull);
			builder.HasMany(name => name.AsVariationSpellings).WithOne(spelling => spelling.VariationName).OnDelete(DeleteBehavior.SetNull);
			builder.HasMany(name => name.AsNickNames).WithOne(nickname => nickname.NickName).OnDelete(DeleteBehavior.SetNull);
			builder.HasMany(name => name.AsFullNames).WithOne(nickname => nickname.FullName).OnDelete(DeleteBehavior.SetNull);
		}
	}
}
