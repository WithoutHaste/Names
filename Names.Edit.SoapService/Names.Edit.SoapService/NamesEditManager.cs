using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Names.DataAccess.EntityFramework;
using Names.Domain;
using Names.Edit.SoapService.Contracts;
using Names.Edit.UseCases;

namespace Names.Edit.SoapService
{
	[ServiceBehavior(IncludeExceptionDetailInFaults = true)]
	public class NamesEditManager : INamesEditService
	{
		private INamesGateway _gateway = null;
		private INamesGateway NamesGateway {
			get {
				if(_gateway == null)
					_gateway = new NamesGateway();
				return _gateway;
			}
		}

		public NameDetailResponse[] GetPagedNames(int pageIndex, int rowsPerPage)
		{
			return GetPagedDetailedNamesAlphabetized.Execute(NamesGateway, pageIndex, rowsPerPage).Select(output => new NameDetailResponse() {
				NameId = output.NameId,
				Name = output.Name,
				FirstLetter = output.FirstLetter,
				IsFamiliar = output.IsFamiliar,
				NameDetailId = output.NameDetailId,
				Origin = output.Origin,
				Meaning = output.Meaning
			}).ToArray();
		}
	}
}
