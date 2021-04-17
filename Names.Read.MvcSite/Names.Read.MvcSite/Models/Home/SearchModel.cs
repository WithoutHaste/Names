using System;
using System.Collections.Generic;
using System.Linq;

namespace Names.Read.MvcSite.Models.Home
{
	public class SearchModel
	{
		public NameModel[] Names { get; set; }

		public string NameCountMessage {
			get {
				return String.Format("{0:n0} Name{1} Found", Names.Length, Names.Length == 1 ? "" : "s");
			}
		}
	}
}