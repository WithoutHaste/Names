using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Names.Edit.MvcSite.ServiceClients;
using Names.Edit.SoapService.Contracts;

namespace Names.Edit.MvcSite.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			NameClient client = new NameClient();
			NameDetailResponse[] result = client.GetPagedNames(0, 50);
			client.Close();

			return View("Index", result);
		}
	}
}