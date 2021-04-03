using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Names.Domain.Entities
{
	/// <summary>
	/// These two names are spelling variations of each other.
	/// </summary>
	[Table("Spelling")]
	public class SpellingRecord
	{
		[Key]
		[Column(Order=1)]
		[ForeignKey("CommonName")]
		public int CommonNameId { get; set; }
		[Key]
		[Column(Order=2)]
		[ForeignKey("VariationName")]
		public int VariationNameId { get; set; }

		public virtual NameRecord CommonName { get; set; }
		public virtual NameRecord VariationName { get; set; }
	}
}
