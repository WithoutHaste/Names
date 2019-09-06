using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameEntities.Entities
{
	/// <summary>
	/// Origin of names, or Regions containing multiple Origins. Recursive structure.
	/// </summary>
	[Table("Category")]
	public class CategoryRecord
	{
		[Key]
		[MaxLength(128)]
		public string Category { get; set; }
		/// <summary>
		/// The category that this record is nested under.
		/// </summary>
		[MaxLength(128)]
		public string SuperCategory { get; set; }
	}
}
