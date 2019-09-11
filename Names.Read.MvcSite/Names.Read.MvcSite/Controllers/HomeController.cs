using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Names.Read.MvcSite.ServiceClients;

namespace Names.Read.MvcSite.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			NameClient nameClient = new NameClient();
			ICollection<string> names = nameClient.GetNames();
			nameClient.Close();

			return View("Index", names);
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}