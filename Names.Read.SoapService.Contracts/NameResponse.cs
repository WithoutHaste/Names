using System.Collections.Generic;

namespace Names.Read.SoapService.Contracts
{
	public class NameResponse
	{
		public string Name { get; set; }
		public string FirstLetter { get; set; }
		public bool? IsBoy { get; set; }
		public bool? IsGirl { get; set; }
		public ICollection<string> Origins { get; set; }
	}
}
