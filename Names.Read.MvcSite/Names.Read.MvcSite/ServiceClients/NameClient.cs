using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using Names.Read.SoapService.Contracts;

namespace Names.Read.MvcSite.ServiceClients
{
	public class NameClient : ClientBase<INamesReadService>, INamesReadService
	{
		public NameClient() : base("NameEndpoint")
		{
		}

		public bool TestServiceConnection()
		{
			return Channel.TestServiceConnection();
		}

		public string TestDataStoreConnection()
		{
			return Channel.TestDataStoreConnection();
		}

		public ICollection<NameResponse> GetDetailedNames(string origin, string gender)
		{
			GenderOption genderOption = GenderOption.Any;
			switch(gender)
			{
				case "OnlyBoys": genderOption = GenderOption.OnlyBoys; break;
				case "OnlyGirls": genderOption = GenderOption.OnlyGirls; break;
			}
			return GetDetailedNames(origin, genderOption);
		}

		public ICollection<NameResponse> GetDetailedNames(string origin, GenderOption gender)
		{
			return Channel.GetDetailedNames(origin, gender);
		}

		public ICollection<CategoryResponse> GetCategories()
		{
			return Channel.GetCategories();
		}
	}
}