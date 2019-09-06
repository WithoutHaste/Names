using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NameEntities.Entities;

namespace NameEntities.Configurations
{
	public class SpellingConfiguration : EntityTypeConfiguration<SpellingRecord>
	{
		public SpellingConfiguration()
		{
			//HasOne(spelling => spelling.CommonName).WithMany(name => name.AsCommonSpellings).OnDelete(DeleteBehavior.Cascade);
			//Property(spelling => spelling.CommonName).WillCascadeOnDelete(false);
			//Property(spelling => spelling.VariationName).WillCascadeOnDelete(false);
		}
	}
}
