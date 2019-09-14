using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Names.Read.MvcSite.ServiceClients;
using Names.Read.SoapService.Contracts;
using Names.Read.MvcSite.Models.Home;

namespace Names.Read.MvcSite.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			IndexModel model = new IndexModel();

			NameClient nameClient = new NameClient();
			model.Categories = nameClient.GetCategories().Select(response => ConvertCategoryResponseToModel(response)).ToArray();
			model.Names = nameClient.GetDetailedNames("English").Select(response => ConvertNameResponseToModel(response)).ToArray();
			nameClient.Close();

			return View("Index", model);
		}

		private NameModel ConvertNameResponseToModel(NameResponse response)
		{
			return new NameModel() {
				Name = response.Name,
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