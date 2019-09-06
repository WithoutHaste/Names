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
	/// A first or last (or both) name.
	/// </summary>
	[Table("Name")]
    public class NameRecord
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		[MaxLength(128)]
		public string Name { get; set; }
		/// <summary>
		/// First letter of the name, for quick lookup.
		/// </summary>
		[Required]
		[MaxLength(1)]
		public string FirstLetter { get; set; }
		/// <summary>
		/// Arbitray value - is this name familiar to me personally?
		/// </summary>
		public bool? IsFamiliar { get; set; }

		public virtual ICollection<NameDetailRecord> Details { get; set; }
		/// <summary>
		/// Spelling records where this name is the common name.
		/// </summary>
		[InverseProperty("CommonName")]
		public virtual ICollection<SpellingRecord> AsCommonSpellings { get; set; }
		/// <summary>
		/// Spelling records where this name is the variation name.
		/// </summary>
		[InverseProperty("VariationName")]
		public virtual ICollection<SpellingRecord> AsVariationSpellings { get; set; }
		/// <summary>
		/// Nickname records where this name is the nickname.
		/// </summary>
		[InverseProperty("NickName")]
		public virtual ICollection<NickNameRecord> AsNickNames { get; set; }
		/// <summary>
		/// Nickname records where this name is the full name.
		/// </summary>
		[InverseProperty("FullName")]
		public virtual ICollection<NickNameRecord> AsFullNames { get; set; }
	}
}
