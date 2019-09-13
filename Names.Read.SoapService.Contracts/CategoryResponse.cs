using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Names.Read.SoapService.Contracts
{
	[DataContract]
	public class CategoryResponse
	{
		[DataMember]
		public string Category { get; set; }
		[DataMember]
		public CategoryResponse[] SubCategories { get; set; }
	}
}
