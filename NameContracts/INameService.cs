using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NameContracts
{
	[ServiceContract]
	public interface INameService
	{
		[OperationContract]
		string GetData(int value);
	}
}
