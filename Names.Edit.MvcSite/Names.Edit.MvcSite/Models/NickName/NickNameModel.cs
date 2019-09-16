using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Names.Edit.MvcSite.Models.NickName
{
	public class NickNameModel
	{
		public int FullNameId { get; set; }
		public string FullName { get; set; }
		public int NickNameId { get; set; }
		public string NickName { get; set; }
	}
}