using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NameEntities.Entities;
using NameEntities.Configurations;

namespace NameEntities
{
	public class NameContext : BaseContext<NameContext>
	{
		public DbSet<NameRecord> Names { get; set; }
		public DbSet<NameDetailRecord> NameDetails { get; set; }
		public DbSet<SpellingRecord> Spellings { get; set; }
		public DbSet<NickNameRecord> NickNames { get; set; }
		public DbSet<CategoryRecord> Categories { get; set; }
		public DbSet<SourceRecord> Sources { get; set; }

		public NameContext() : base("NameDatabase")
		{
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations
				.Add(new NameConfiguration())
				.Add(new SpellingConfiguration())
				;
			base.OnModelCreating(modelBuilder);
		}
	}
}
