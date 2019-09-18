using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Names.Domain.Entities
{
	public class NameWithDetailResult
	{
		public int NameId { get; set; }
		public string Name { get; set; }
		public string FirstLetter { get; set; }
		public bool? IsFamiliar { get; set; }

		public int NameDetailId { get; set; }
		public bool? IsBoy { get; set; }
		public bool? IsGirl { get; set; }
		public string Origin { get; set; }
		public string Meaning { get; set; }
	}
}
