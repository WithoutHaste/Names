using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using NameContracts;

namespace NameWebsite.NameService
{
	public class NameClient : ClientBase<INameService>, INameService
	{
		public NameClient() : base("NameEndpoint")
		{
		}

		public string GetData(int value)
		{
			return Channel.GetData(value);
		}
	}
}