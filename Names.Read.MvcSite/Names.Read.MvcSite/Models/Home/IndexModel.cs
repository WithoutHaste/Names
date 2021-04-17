using System;
using System.Collections.Generic;
using System.Linq;

namespace Names.Read.MvcSite.Models.Home
{
	public class IndexModel
	{
		public SearchModel Search { get; set; }
		public CategoryModel[] Categories {
			set {
				origins = new List<OriginModel>();
				origins.Add(new OriginModel("All", "All Regions"));
				if(String.IsNullOrEmpty(selectedOrigin) || selectedOrigin == "All")
					origins[0].Checked = true;
				ConvertCategoriesToOrigins(value);
			}
		}

		private List<OriginModel> origins;
		public OriginModel[] Origins { get { return origins.ToArray(); } }

		private string selectedOrigin;
		private string selectedGender;

		public IndexModel(string selectedOrigin, string selectedGender)
		{
			this.selectedOrigin = selectedOrigin;
			this.selectedGender = selectedGender;
		}

		public bool GenderSelected(string gender)
		{
			return (selectedGender == gender);
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