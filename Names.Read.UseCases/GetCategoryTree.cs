using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Names.Domain;
using Names.Domain.Entities;
using Names.Read.UseCases.Data;

namespace Names.Read.UseCases
{
	public static class GetCategoryTree
	{
		public static ICollection<CategoryOutput> Execute(INamesGateway gateway)
		{
			return gateway.GetCategories().ConvertToTree();
		}

		/// <summary>
		/// Convert linear list of categories into tree structure.
		/// Cannot fall into a loop, even if the data contains a loop.
		/// </summary>
		private static ICollection<CategoryOutput> ConvertToTree(this ICollection<CategoryRecord> records)
		{
			ICollection<CategoryOutput> result = GetTopLevelCategories(records);
			AddChildren(result, records, new string[] { });
			return result;
		}

		private static ICollection<CategoryOutput> GetTopLevelCategories(ICollection<CategoryRecord> records)
		{
			return records.Where(record => String.IsNullOrEmpty(record.SuperCategory)).Select(record => new CategoryOutput() { Category = record.Category }).ToList();
		}

		/// <summary>
		/// Builds out the rest of the tree.
		/// </summary>
		/// <param name="roots"></param>
		/// <param name="inputs"></param>
		/// <param name="path">The path of categories that led to this point; to avoid loops.</param>
		private static void AddChildren(ICollection<CategoryOutput> roots, ICollection<CategoryRecord> inputs, string[] path)
		{
			foreach(CategoryOutput root in roots)
			{
				foreach(CategoryRecord input in inputs)
				{
					if(path.Contains(input.Category))
						continue;
					if(input.SuperCategory == root.Category)
					{
						root.SubCategories.Add(new CategoryOutput() { Category = input.Category });
					}
				}
				string[] nextPath = new string[path.Length + 1];
				Array.Copy(path, nextPath, path.Length);
				nextPath[path.Length] = root.Category;
				AddChildren(root.SubCategories, inputs, nextPath);
			}
		}
	}
}
