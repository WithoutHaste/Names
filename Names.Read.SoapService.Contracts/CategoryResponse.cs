namespace Names.Read.SoapService.Contracts
{
	public class CategoryResponse
	{
		public string Category { get; set; }
		public CategoryResponse[] SubCategories { get; set; }
	}
}
