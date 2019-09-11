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

		public ICollection<string> GetNames()
		{
			return NamesGateway.GetNames();
		}
	}
}
