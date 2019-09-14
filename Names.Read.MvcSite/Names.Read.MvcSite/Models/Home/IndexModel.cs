using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Names.Read.MvcSite.Models.Home
{
	public class IndexModel
	{
		public NameModel[] Names { get; set; }
		public CategoryModel[] Categories {
			set {
				origins = new List<OriginModel>();
				origins.Add(new OriginModel(null, "All"));
				if(String.IsNullOrEmpty(selectedOrigin))
					origins[0].Checked = true;
				ConvertCategoriesToOrigins(value);
			}
		}

		private List<OriginModel> origins;
		public OriginModel[] Origins { get { return origins.ToArray(); } }

		private string selectedOrigin;

		public IndexModel(string selectedOrigin)
		{
			this.selectedOrigin = selectedOrigin;
		}

		private void ConvertCategoriesToOrigins(ICollection<CategoryModel> categories, int depth = 0)
		{
			if(categories == null)
				return;
			foreach(CategoryModel category in categories)
			{
				OriginModel origin = new OriginModel(category.Category, depth);
				if(origin.Value == selectedOrigin)
					origin.Checked = true;
				origins.Add(origin);
				ConvertCategoriesToOrigins(category.SubCategories, depth + 1);
			}
		}
	}
}