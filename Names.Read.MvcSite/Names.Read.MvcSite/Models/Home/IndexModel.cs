﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Names.Read.MvcSite.Models.Home
{
	public class IndexModel
	{
		public NameModel[] Names { get; set; }
		public CategoryModel[] Categories { get; set; }
	}
}