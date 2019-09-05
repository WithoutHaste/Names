using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameEntities
{
	/// <summary>
	/// A nickname associated with the full version of the name.
	/// </summary>
	public class NickName
	{
		public int NickNameId { get; set; }
		public int FullNameId { get; set; }

		/// <summary>
		/// Would be called NickName, but that's the enclosing type.
		/// </summary>
		public virtual Name Name { get; set; }
		public virtual Name FullName { get; set; }
	}
}
