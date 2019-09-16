using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Names.Edit.MvcSite.ServiceClients;
using Names.Edit.SoapService.Contracts;
using Names.Edit.MvcSite.Models.Home;
using Names.Edit.MvcSite.Models.ModelBinders;

namespace Names.Edit.MvcSite.Controllers
{
	public class HomeController : Controller
	{
		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Index(int pageIndex = 0, int rowsPerPage = 50)
		{
			IndexModel model = new IndexModel() { PageIndex = pageIndex, RowsPerPage = rowsPerPage };

			NameClient client = new NameClient();
			model.NameDetails = client.GetPagedNames(pageIndex, rowsPerPage).Select(response => new NameDetail() {
				NameId = response.NameId,
				Name = response.Name,
				NameDetailId = response.NameDetailId,
				Origin = response.Origin,
				Meaning = response.Meaning
			}).ToArray();
			client.Close();

			return View("Index", model);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Save([ModelBinder(typeof(MultipleNameDetailsBinder))]EditNameDetail[] edits, int pageIndex, int rowsPerPage)
		{
			NameClient client = new NameClient();
			client.EditNameDetails(edits.Select(edit => new EditNameDetailRequest() {
				NameDetailId = edit.NameDetailId,
				Origin = edit.Origin,
				Meaning = edit.Meaning,
				IsDeleted = edit.IsDeleted
			}).ToArray());
			client.Close();

			return RedirectToAction("Index", new { pageIndex = pageIndex, rowsPerPage = rowsPerPage });
		}
	}
}