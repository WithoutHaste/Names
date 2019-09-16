using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Names.Edit.MvcSite.ServiceClients;
using Names.Edit.MvcSite.Models.NickName;

namespace Names.Edit.MvcSite.Controllers
{
    public class NickNameController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult All()
		{
			NameClient client = new NameClient();
			NickNameModel[] models = client.GetNickNames().Select(response => new NickNameModel() {
				FullNameId = response.FullNameId,
				FullName = response.FullName,
				NickNameId = response.NickNameId,
				NickName = response.NickName
			}).ToArray();
			client.Close();

			return View("All", models);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		//public ActionResult Save(AddNickName nickName)
		public ActionResult Save(string FullName, string NickName)
		{
			NameClient client = new NameClient();
			//client.AddNickName(nickName.FullName, nickName.NickName);
			client.AddNickName(FullName, NickName);
			client.Close();

			return RedirectToAction("All");
		}
	}
}