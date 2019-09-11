using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Names.Domain.Entities;

namespace Names.DataAccess.EntityFramework.Configurations
{
	public class NameConfiguration : EntityTypeConfiguration<NameRecord>
	{
		public NameConfiguration()
		{
			HasMany(name => name.Details).WithRequired(detail => detail.Name).WillCascadeOnDelete(false);
			HasMany(name => name.AsCommonSpellings).WithRequired(spelling => spelling.CommonName).WillCascadeOnDelete(false);
			HasMany(name => name.AsVariationSpellings).WithRequired(spelling => spelling.VariationName).WillCascadeOnDelete(false);
			HasMany(name => name.AsNickNames).WithRequired(nickname => nickname.NickName).WillCascadeOnDelete(false);
			HasMany(name => name.AsFullNames).WithRequired(nickname => nickname.FullName).WillCascadeOnDelete(false);
		}
	}
}
