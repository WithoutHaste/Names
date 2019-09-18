using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Names.Read.UseCases.Data
{
	public class NameOutput
	{
		public string Name { get; set; }
		public bool? IsBoy { get; set; }
		public bool? IsGirl { get; set; }
		public ICollection<string> Origins { get; set; }
	}
}
