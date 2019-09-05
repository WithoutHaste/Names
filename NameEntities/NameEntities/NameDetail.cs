using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameEntities
{
	/// <summary>
	/// </summary>
	public class NameDetail
	{
		public int Id { get; set; }
		public int NameId { get; set; }
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
		public string Origin { get; set; }
		public string Meaning { get; set; }
		public DateTime CreateDateTime { get; set; }

		public virtual Name Name { get; set; }
		public virtual Source Source { get; set; }
	}
}
