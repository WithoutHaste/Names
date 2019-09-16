using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Names.Edit.SoapService.Contracts
{
	[DataContract]
	public class NickNameResponse
	{
		[DataMember]
		public int FullNameId { get; set; }
		[DataMember]
		public string FullName { get; set; }
		[DataMember]
		public int NickNameId { get; set; }
		[DataMember]
		public string NickName { get; set; }
	}
}
