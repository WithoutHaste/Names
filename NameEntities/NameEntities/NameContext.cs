using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NameEntities.Entities;
//using NameEntities.Configurations;

namespace NameEntities
{
	public class NameContext : BaseContext<NameContext>
	{
		public DbSet<NameRecord> Names { get; set; }

		public NameContext() : base("NameConnectionString")
		{
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			//modelBuilder.Configurations
			//	.Add(new NameConfiguration())
			//	;
			base.OnModelCreating(modelBuilder);
		}
	}
}
