using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Names.Read.MvcSite.Models.Home
{
	public class CategoryModel
	{
		public string Category { get; set; }
		public CategoryModel[] SubCategories { get; set; }
	}
}