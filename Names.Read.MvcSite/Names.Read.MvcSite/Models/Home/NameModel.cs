using System;
using System.Collections.Generic;
using System.Linq;

namespace Names.Read.MvcSite.Models.Home
{
	public class NameModel
	{
		public string Name { get; set; }
		public string FirstLetter { get; set; }
		public bool? IsBoy { private get; set; }
		public bool? IsGirl { private get; set; }
		public string OriginText { get; set; }

		public string Boy {
			get {
				return (IsBoy == true) ? "Boy" : "";
			}
		}
		public string Girl {
			get {
				return (IsGirl == true) ? "Girl" : "";
			}
		}
	}
}