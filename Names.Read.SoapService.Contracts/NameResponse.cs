using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Names.Read.SoapService.Contracts
{
	[DataContract]
	public class NameResponse
	{
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public bool? IsBoy { get; set; }
		[DataMember]
		public bool? IsGirl { get; set; }
		[DataMember]
		public ICollection<string> Origins { get; set; }
	}
}
