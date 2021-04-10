using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Names.Domain.Entities;
using Names.DataAccess.EntityFramework.Configurations;

namespace Names.DataAccess.EntityFramework
{
	public class NamesContext : BaseContext<NamesContext>, INamesContext
	{
		public DbSet<NameRecord> Names { get; set; }
		public DbSet<NameDetailRecord> NameDetails { get; set; }
		public DbSet<SpellingRecord> Spellings { get; set; }
		public DbSet<NickNameRecord> NickNames { get; set; }
		public DbSet<CategoryRecord> Categories { get; set; }
		public DbSet<SourceRecord> Sources { get; set; }

		//just for stored proc results
		public DbSet<NameWithDetailResult> NameWithDetailResults { get; set; }

		public NamesContext(DbContextOptions<NamesContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new NameConfiguration());
			modelBuilder.ApplyConfiguration(new NickNameConfiguration());
			modelBuilder.ApplyConfiguration(new SpellingConfiguration());
			modelBuilder.ApplyConfiguration(new SourceConfiguration());
			modelBuilder.ApplyConfiguration(new NameWithDetailConfiguration());
			base.OnModelCreating(modelBuilder);
		}

		public List<NameWithDetailResult> GetNamesByOrigin(string origin)
		{
			SqlParameter originParameter = new SqlParameter("Origin", (object)origin ?? DBNull.Value);

			return NameWithDetailResults.FromSqlRaw("exec GetNamesByOrigin @Origin", originParameter).ToList();
		}

		public NameWithDetailResult[] GetPagedNames(int pageIndex, int rowsPerPage)
		{
			SqlParameter pageIndexParameter = new SqlParameter("pageIndex", pageIndex);
			SqlParameter rowsPerPageParameter = new SqlParameter("rowsPerPage", rowsPerPage);

			return NameWithDetailResults.FromSqlRaw("exec GetPagedNames @pageIndex, @rowsPerPage", pageIndexParameter, rowsPerPageParameter).ToArray();
		}
	}
}
