﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Names.Read.MvcSite.ServiceClients;
using Names.Read.SoapService.Contracts;
using Names.Read.MvcSite.Models.Home;

namespace Names.Read.MvcSite.Controllers
{
	public class HomeController : Controller
	{
		[HttpGet]
		public async Task<ActionResult> Index(string origin=null, string gender="Any")
		{
			IndexModel model = new IndexModel(origin, gender);
			NameClient nameClient = new NameClient();
			CategoryResponse[] categories = (await nameClient.GetCategories()).ToArray();
			model.Categories = categories.Select(response => ConvertCategoryResponseToModel(response)).ToArray();
			if(!String.IsNullOrEmpty(origin))
			{
				model.Search = new SearchModel();
				NameResponse[] names = (await nameClient.GetDetailedNames(origin, gender)).ToArray();
				model.Search.Names = names.Select(response => ConvertNameResponseToModel(response)).ToArray();
			}

			return View("Index", model);
		}

		[HttpPost]
		public async Task<ActionResult> Search(string origin = null, string gender="Any")
		{
			SearchModel model = new SearchModel();

			NameClient nameClient = new NameClient();
			NameResponse[] names = (await nameClient.GetDetailedNames(origin, gender)).ToArray();
			model.Names = names.Select(response => ConvertNameResponseToModel(response)).ToArray();

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