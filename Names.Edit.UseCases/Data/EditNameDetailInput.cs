using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Names.Edit.UseCases.Data
{
	public class EditNameDetailInput
	{
		public int NameDetailId { get; set; }
		public string Origin { get; set; }
		public string Meaning { get; set; }
	}
}
