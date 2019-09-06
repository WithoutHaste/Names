using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using NameContracts;
using NameEntities;

namespace NameService
{
	[ServiceBehavior(IncludeExceptionDetailInFaults=true)]
	public class NameManager : INameService
	{
		public string GetData(int value)
		{
			//return string.Format("You entered: {0}", value);

			NameRepository repository = new NameRepository();
			return repository.TestConnection();
		}
	}
}
