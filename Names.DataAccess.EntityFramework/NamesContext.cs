using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Names.Domain.Entities;
using Names.DataAccess.EntityFramework.Configurations;

namespace Names.DataAccess.EntityFramework
{
	internal class NamesContext : BaseContext<NamesContext>
	{
		public DbSet<NameRecord> Names { get; set; }
		public DbSet<NameDetailRecord> NameDetails { get; set; }
		public DbSet<SpellingRecord> Spellings { get; set; }
		public DbSet<NickNameRecord> NickNames { get; set; }
		public DbSet<CategoryRecord> Categories { get; set; }
		public DbSet<SourceRecord> Sources { get; set; }

		//just for stored proc results
		public DbSet<NameWithDetailResult> NameWithDetailResults { get; set; }

		public NamesContext() : base("name=NameDatabase")
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration<NameRecord>(new NameConfiguration());
			modelBuilder.ApplyConfiguration<SourceRecord>(new SourceConfiguration());
			base.OnModelCreating(modelBuilder);
		}

		public List<NameWithDetailResult> GetNamesByOrigin(string origin)
		{
			SqlParameter originParameter = new SqlParameter("Origin", (object)origin ?? DBNull.Value);

			return NameWithDetailResults.FromSql("exec GetNamesByOrigin @Origin", originParameter).ToList();
		}

		public NameWithDetailResult[] GetPagedNames(int pageIndex, int rowsPerPage)
		{
			SqlParameter pageIndexParameter = new SqlParameter("pageIndex", pageIndex);
			SqlParameter rowsPerPageParameter = new SqlParameter("rowsPerPage", rowsPerPage);

			return NameWithDetailResults.FromSql("exec GetPagedNames @pageIndex, @rowsPerPage", pageIndexParameter, rowsPerPageParameter).ToArray();
		}
	}
}
