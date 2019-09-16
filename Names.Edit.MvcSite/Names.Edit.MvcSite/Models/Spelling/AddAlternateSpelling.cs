using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Names.Edit.MvcSite.Models.Spelling
{
	public class AddAlternateSpelling
	{
		[Required]
		[Display(Name="Common Name")]
		public string CommonName { get; set; }
		[Required]
		[Display(Name="Variation Name")]
		public string VariationName { get; set; }
	}
}