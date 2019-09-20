using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Names.Read.SoapService.Contracts
{
	[DataContract]
	public enum GenderOption : int
	{
		[EnumMember]
		Any = 0,
		[EnumMember]
		OnlyBoys = 1,
		[EnumMember]
		OnlyGirls = 2
	};
}
