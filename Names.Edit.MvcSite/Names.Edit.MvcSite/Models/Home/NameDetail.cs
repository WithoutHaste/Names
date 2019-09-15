using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Names.Edit.MvcSite.Models.Home
{
	public class NameDetail
	{
		public int NameId { get; set; }
		public string Name { get; set; }
		public int NameDetailId { get; set; }
		public string Origin { get; set; }
		public string Meaning { get; set; }
	}
}