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
	/// Data source.
	/// </summary>
	[Table("Source")]
	public class SourceRecord
	{
		[Key]
		public int Id { get; set; }
		/// <summary>
		/// Human-legible name of data source.
		/// </summary>
		[Required]
		public string Name { get; set; }
		public string Url { get; set; }
	}
}
