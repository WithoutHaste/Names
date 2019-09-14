using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Names.Edit.SoapService.Contracts
{
	[DataContract]
	public class NameDetailResponse
	{
		[DataMember]
		public int NameId { get; set; }
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public string FirstLetter { get; set; }
		[DataMember]
		public bool? IsFamiliar { get; set; }

		[DataMember]
		public int NameDetailId { get; set; }
		[DataMember]
		public string Origin { get; set; }
		[DataMember]
		public string Meaning { get; set; }
	}
}
