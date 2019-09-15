using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Names.Edit.MvcSite.Models.Home
{
	public class IndexModel
	{
		public int PageIndex { get; set; }
		public int RowsPerPage { get; set; }
		public NameDetail[] NameDetails { get; set; }
	}
}