using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Names.DataAccess.EntityFramework;
using Names.Domain;
using Names.Read.SoapService.Contracts;
using Names.Read.UseCases;

namespace Names.Read.SoapService
{
	[ServiceBehavior(IncludeExceptionDetailInFaults=true)]
	public class NamesReadManager : INamesReadService
	{
		private INamesGateway _gateway = null;
		private INamesGateway NamesGateway {
			get {
				if(_gateway == null)
					_gateway = new NamesGateway();
				return _gateway;
			}
		}

		public string TestDataStoreConnection()
		{
			return NamesGateway.TestDataStoreConnection();
		}

		public ICollection<NameResponse> GetDetailedNames(string origin, GenderOption gender)
		{
			if(origin == "All")
				origin = null;
			bool includeBoys = (gender != GenderOption.OnlyGirls);
			bool includeGirls = (gender != GenderOption.OnlyBoys);
			return GetDetailedNamesAlphabetized.Execute(NamesGateway, origin, includeBoys, includeGirls).Select(output => Convert.NameOutputToResponse(output)).ToList();
		}

		public ICollection<CategoryResponse> GetCategories()
		{
			return GetCategoryTree.Execute(NamesGateway).Select(output => Convert.CategoryOutputToResponse(output)).ToList();
		}
	}
}
