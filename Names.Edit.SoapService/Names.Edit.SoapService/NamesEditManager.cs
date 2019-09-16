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
using Names.Edit.UseCases.Data;

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

		[OperationBehavior(TransactionScopeRequired=true)]
		public void EditNameDetails(EditNameDetailRequest[] editRequests)
		{
			UseCases.EditNameDetails.Execute(NamesGateway, editRequests.Select(request => new EditNameDetailInput() {
				NameDetailId = request.NameDetailId,
				Origin = request.Origin,
				Meaning = request.Meaning
			}).ToArray());
		}

		public SpellingResponse[] GetSpellings()
		{
			return UseCases.GetSpellings.Execute(NamesGateway).Select(output => new SpellingResponse() {
				CommonNameId = output.CommonNameId,
				CommonName = output.CommonName,
				VariationNameId = output.VariationNameId,
				VariationName = output.VariationName
			}).ToArray();
		}
	}
}
