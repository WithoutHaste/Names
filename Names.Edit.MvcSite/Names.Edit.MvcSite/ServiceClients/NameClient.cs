using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using Names.Edit.SoapService.Contracts;

namespace Names.Edit.MvcSite.ServiceClients
{
	public class NameClient : ClientBase<INamesEditService>, INamesEditService
	{
		public NameClient() : base("NameEndpoint")
		{
		}

		public NameDetailResponse[] GetPagedNames(int pageIndex, int rowsPerPage)
		{
			return Channel.GetPagedNames(pageIndex, rowsPerPage);
		}

		public void EditNameDetails(EditNameDetailRequest[] editRequests)
		{
			Channel.EditNameDetails(editRequests);
		}
	}
}