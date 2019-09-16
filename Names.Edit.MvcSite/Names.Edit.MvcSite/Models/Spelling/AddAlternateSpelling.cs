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
		public string NormalName { get; set; }
		[Required]
		public string AlternateName { get; set; }
	}
}