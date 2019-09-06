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
	/// </summary>
	[Table("NameDetail")]
	public class NameDetailRecord
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[ForeignKey("Name")]
		public int NameId { get; set; }
		[ForeignKey("Source")]
		public int SourceId { get; set; }
		/// <summary>
		/// Is a traditional or common boy's name.
		/// </summary>
		public bool? IsBoy { get; set; }
		/// <summary>
		/// Is a traditional or common girl's name.
		/// </summary>
		public bool? IsGirl { get; set; }
		/// <summary>
		/// Is a traditional or common first name.
		/// </summary>
		public bool? IsFirstName { get; set; }
		/// <summary>
		/// Is a traditional or common last name.
		/// </summary>
		public bool? IsLastName { get; set; }
		/// <summary>
		/// Region or country of origin.
		/// </summary>
		[MaxLength(128)]
		public string Origin { get; set; }
		[MaxLength(256)]
		public string Meaning { get; set; }
		[Required]
		public DateTime CreateDateTime { get; set; }

		public virtual NameRecord Name { get; set; }
		public virtual SourceRecord Source { get; set; }
	}
}
