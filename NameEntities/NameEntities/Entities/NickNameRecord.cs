using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameEntities.Entities
{
	/// <summary>
	/// A nickname associated with the full version of the name.
	/// </summary>
	public class NickNameRecord
	{
		public int NickNameId { get; set; }
		public int FullNameId { get; set; }

		public virtual NameRecord NickName { get; set; }
		public virtual NameRecord FullName { get; set; }
	}
}
