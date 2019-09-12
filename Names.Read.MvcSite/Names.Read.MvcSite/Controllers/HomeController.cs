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
			NameClient nameClient = new NameClient();
			List<IndexModel> model = nameClient.GetDetailedNames("English").Select(response => new IndexModel() {
				Name = response.Name,
				OriginText = String.Join(", ", response.Origins)
			}).ToList();
			nameClient.Close();

			return View("Index", model);
		}
	}
}