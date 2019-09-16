using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Names.Domain.Entities
{
	public class EditNameDetailRequest
	{
		public int NameDetailId { get; set; }
		public string Origin { get; set; }
		public string Meaning { get; set; }
		public bool IsDeleted { get; set; }
	}
}
