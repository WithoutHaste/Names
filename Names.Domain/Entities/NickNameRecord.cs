using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Names.Domain.Entities
{
	/// <summary>
	/// A nickname associated with the full version of the name.
	/// </summary>
	[Table("NickName")]
	public class NickNameRecord
	{
		[ForeignKey("NickName")]
		public int NickNameId { get; set; }
		[ForeignKey("FullName")]
		public int FullNameId { get; set; }

		public virtual NameRecord NickName { get; set; }
		public virtual NameRecord FullName { get; set; }
	}
}
