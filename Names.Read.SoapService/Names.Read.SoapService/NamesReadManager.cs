using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Names.Read.SoapService.Contracts;
using Names.Domain;

namespace Names.Read.SoapService
{
	[ServiceBehavior(IncludeExceptionDetailInFaults=true)]
	public class NamesReadManager : INamesReadService
	{
		public string GetData(int value)
		{
			//return string.Format("You entered: {0}", value);

			NameRepository repository = new NameRepository();
			return repository.TestConnection();
		}
	}
}
