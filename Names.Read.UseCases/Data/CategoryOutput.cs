using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Names.Read.UseCases.Data
{
	public class CategoryOutput
	{
		public string Category { get; set; }
		public List<CategoryOutput> SubCategories { get; set; }

		public CategoryOutput()
		{
			SubCategories = new List<CategoryOutput>();
		}
	}
}
