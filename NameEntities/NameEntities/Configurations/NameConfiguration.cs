//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.ModelConfiguration;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using NameEntities.Entities;

//namespace NameEntities.Configurations
//{
//	public class NameConfiguration : EntityTypeConfiguration<NameRecord>
//	{
//		public NameConfiguration()
//		{
//			ToTable("Name", "dbo");
//			HasKey(x => x.Id).Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
//			Property(x => x.Name).HasMaxLength(200).IsRequired();
//			//Property(x => x.FirstLetter).HasColumnType("char").IsRequired();
//			Property(x => x.IsFamiliar).IsOptional();
//		}
//	}
//}
