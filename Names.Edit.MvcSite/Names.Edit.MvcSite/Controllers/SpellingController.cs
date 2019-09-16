using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Names.Edit.MvcSite.ServiceClients;
using Names.Edit.MvcSite.Models.Spelling;

namespace Names.Edit.MvcSite.Controllers
{
    public class SpellingController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult All()
		{
			NameClient client = new NameClient();
			SpellingModel[] models = client.GetSpellings().Select(response => new SpellingModel() {
				CommonNameId = response.CommonNameId,
				CommonName = response.CommonName,
				VariationNameId = response.VariationNameId,
				VariationName = response.VariationName
			}).ToArray();
			client.Close();

			return View("All", models);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Save(AddAlternateSpelling alternateSpelling)
		{
			NameClient client = new NameClient();
			client.AddSpelling(alternateSpelling.CommonName, alternateSpelling.VariationName);
			client.Close();

			return RedirectToAction("All");
		}
    }
}