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
		public string GetData(int value)
		{
			//return string.Format("You entered: {0}", value);

			INamesGateway repository = new NamesGateway();
			return repository.TestConnection();
		}
	}
}
