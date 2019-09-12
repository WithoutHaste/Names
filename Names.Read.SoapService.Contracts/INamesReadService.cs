﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Names.Read.SoapService.Contracts
{
	[ServiceContract]
	public interface INamesReadService
	{
		[OperationContract]
		string TestDataStoreConnection();

		[OperationContract]
		ICollection<NameResponse> GetDetailedNames();
	}
}
