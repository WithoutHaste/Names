using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Names.DataAccess.EntityFramework;
using Names.Domain;
using Names.Read.SoapService.Contracts;
using Names.Read.UseCases;
using Names.Read.UseCases.Data;

namespace Names.Read.WebApi.Controllers
{
	[Route("api")]
	[ApiController]
	public class NamesController : Controller
    {
		private readonly INamesGateway _gateway;

		public NamesController(INamesGateway gateway)
		{
			_gateway = gateway;
		}

		[HttpGet("names")]
		public ActionResult<ICollection<NameResponse>> GetDetailedNames(string origin, GenderOption gender)
		{
			if (origin == "All")
				origin = null;
			bool includeBoys = (gender != GenderOption.OnlyGirls);
			bool includeGirls = (gender != GenderOption.OnlyBoys);
			return GetDetailedNamesAlphabetized
				.Execute(_gateway, origin, includeBoys, includeGirls)
				.Select(output => NameOutputToResponse(output))
				.ToList();
		}

		[HttpGet("categories")]
		public ICollection<CategoryResponse> GetCategories()
		{
			return GetCategoryTree.Execute(_gateway).Select(output => CategoryOutputToResponse(output)).ToList();
		}

		private NameResponse NameOutputToResponse(NameOutput output)
		{
			return new NameResponse()
			{
				Name = output.Name,
				FirstLetter = output.FirstLetter,
				IsBoy = output.IsBoy,
				IsGirl = output.IsGirl,
				Origins = output.Origins
			};
		}

		private CategoryResponse CategoryOutputToResponse(CategoryOutput output)
		{
			return new CategoryResponse()
			{
				Category = output.Category,
				SubCategories = output.SubCategories.Select(sub => CategoryOutputToResponse(sub)).ToArray()
			};
		}
	}
}
