using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameEntities
{
	/// <summary>
	/// Category or Origin of names. Recursive structure.
	/// </summary>
	public class Category
	{
		/// <summary>
		/// Would be called Category, but that's the encloding type name.
		/// </summary>
		public string Text { get; set; }
		/// <summary>
		/// The category that this record is nested under.
		/// </summary>
		public string SuperCategory { get; set; }
	}
}
