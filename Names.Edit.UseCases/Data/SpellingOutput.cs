using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Names.Edit.UseCases.Data
{
	public class SpellingOutput
	{
		public int CommonNameId { get; set; }
		public string CommonName { get; set; }
		public int VariationNameId { get; set; }
		public string VariationName { get; set; }
	}
}
