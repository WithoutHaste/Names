using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameEntities.Entities
{
	/// <summary>
	/// A first or last (or both) name.
	/// </summary>
    public class NameRecord
    {
		public int Id { get; set; }
		public string Name { get; set; }
		/// <summary>
		/// First letter of the name, for quick lookup.
		/// </summary>
		public char FirstLetter { get; set; }
		/// <summary>
		/// Arbitray value - is this name familiar to me personally?
		/// </summary>
		public bool? IsFamiliar { get; set; }

		/// <summary>
		/// Spelling records where this name is the common name.
		/// </summary>
		public virtual ICollection<SpellingRecord> CommonNameSpelling { get; set; }
		/// <summary>
		/// Spelling records where this name is the variation name.
		/// </summary>
		public virtual ICollection<SpellingRecord> VariationNameSpelling { get; set; }
    }
}
