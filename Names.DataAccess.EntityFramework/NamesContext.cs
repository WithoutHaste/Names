﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

		public NamesContext() : base("name=NameDatabase")
		{
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations
				.Add(new NameConfiguration())
				.Add(new SourceConfiguration())
				;
			base.OnModelCreating(modelBuilder);
		}
	}
}