using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Names.Read.MvcSite.ServiceClients;
using Names.Read.SoapService.Contracts;
using Names.Read.MvcSite.Models.Home;

namespace Names.Read.MvcSite.Controllers
{
	public class HomeController : Controller
	{
		[HttpGet]
		public ActionResult Index(string origin=null, string gender="Any")
		{
			IndexModel model = new IndexModel(origin, gender);

			NameClient nameClient = new NameClient();
			model.Categories = nameClient.GetCategories().Select(response => ConvertCategoryResponseToModel(response)).ToArray();
			if(!String.IsNullOrEmpty(origin))
			{
				model.Search = new SearchModel();
				model.Search.Names = nameClient.GetDetailedNames(origin, gender).Select(response => ConvertNameResponseToModel(response)).ToArray();
			}
			nameClient.Close();

			return View("Index", model);
		}

		[HttpPost]
		public ActionResult Search(string origin = null, string gender="Any")
		{
			SearchModel model = new SearchModel();

			NameClient nameClient = new NameClient();
			model.Names = nameClient.GetDetailedNames(origin, gender).Select(response => ConvertNameResponseToModel(response)).ToArray();
			nameClient.Close();

			return View("_Search", model);
		}

		private NameModel ConvertNameResponseToModel(NameResponse response)
		{
			return new NameModel() {
				Name = response.Name,
				FirstLetter = response.FirstLetter,
				IsBoy = response.IsBoy,
				IsGirl = response.IsGirl,
				OriginText = String.Join(", ", response.Origins)
			};
		}

		private CategoryModel ConvertCategoryResponseToModel(CategoryResponse response)
		{
			return new CategoryModel() {
				Category = response.Category,
				SubCategories = response.SubCategories.Select(sub => ConvertCategoryResponseToModel(sub)).ToArray()
			};
		}
	}
}