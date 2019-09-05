using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameEntities
{
	/// <summary>
	/// These two names are spelling variations of each other.
	/// </summary>
	public class SpellingRecord
	{
		public int CommonNameId { get; set; }
		public int VariationNameId { get; set; }

		public virtual NameRecord CommonName { get; set; }
		public virtual NameRecord VariationName { get; set; }
	}
}
