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
	public class SourceConfiguration : EntityTypeConfiguration<SourceRecord>
	{
		public SourceConfiguration()
		{
			HasMany(source => source.NameDetails).WithRequired(detail => detail.Source).WillCascadeOnDelete(false);
		}
	}
}
