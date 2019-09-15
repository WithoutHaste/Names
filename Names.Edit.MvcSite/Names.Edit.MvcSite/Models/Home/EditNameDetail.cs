using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Names.Edit.MvcSite.Models.Home
{
	public class EditNameDetail
	{
		public int NameDetailId { get; set; }
		public string Origin { get; set; }
		public string Meaning { get; set; }
//		public string IsEdited { private get; set; }
//		public bool IsEditedBool { get { return (IsEdited == "on"); } }
	}
}