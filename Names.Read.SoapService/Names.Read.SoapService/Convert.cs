using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Names.Read.UseCases.Data;
using Names.Read.SoapService.Contracts;

namespace Names.Read.SoapService
{
	/// <summary>
	/// Conversions from Use Case data types to Service data types.
	/// </summary>
	internal static class Convert
	{
		internal static NameResponse NameOutputToResponse(this NameOutput output)
		{
			return new NameResponse() {
				Name = output.Name,
				IsBoy = output.IsBoy,
				IsGirl = output.IsGirl,
				Origins = output.Origins
			};
		}

		internal static CategoryResponse CategoryOutputToResponse(this CategoryOutput output)
		{
			return new CategoryResponse() {
				Category = output.Category,
				SubCategories = output.SubCategories.Select(sub => sub.CategoryOutputToResponse()).ToArray()
			};
		}
	}
}