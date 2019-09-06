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
	/// A nickname associated with the full version of the name.
	/// </summary>
	[Table("NickName")]
	public class NickNameRecord
	{
		[Key]
		[Column(Order=1)]
		[ForeignKey("NickName")]
		public int NickNameId { get; set; }
		[Key]
		[Column(Order=2)]
		[ForeignKey("FullName")]
		public int FullNameId { get; set; }

		public virtual NameRecord NickName { get; set; }
		public virtual NameRecord FullName { get; set; }
	}
}
