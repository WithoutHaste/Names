﻿using System;
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

		public string GetData(int value)
		{
			return Channel.GetData(value);
		}
	}
}