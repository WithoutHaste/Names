using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Names.Read.MvcSite.Models.Home
{
	public class OriginModel
	{
		public string Value { get; set; }
		public string Text { get; set; }
		public int Depth { get; set; }
		public bool Checked { get; set; }

		public OriginModel(string value) : this(value, value, 0)
		{
		}

		public OriginModel(string value, string text) : this(value, text, 0)
		{
		}

		public OriginModel(string value, int depth) : this(value, value, depth)
		{
		}

		public OriginModel(string value, string text, int depth)
		{
			Value = value;
			Text = text;
			Depth = depth;
		}
	}
}