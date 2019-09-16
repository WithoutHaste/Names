using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Names.Edit.MvcSite.Models.Spelling
{
	public class SpellingModel
	{
		public int CommonNameId { get; set; }
		public string CommonName { get; set; }
		public int VariationNameId { get; set; }
		public string VariationName { get; set; }
	}
}