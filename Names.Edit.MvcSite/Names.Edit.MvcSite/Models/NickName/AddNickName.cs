using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Names.Edit.MvcSite.Models.NickName
{
	public class AddNickName
	{
		[Required]
		[Display(Name = "Full Name")]
		public string FullName { get; set; }
		[Required]
		[Display(Name = "Nickname")]
		public string NickName { get; set; }
	}
}