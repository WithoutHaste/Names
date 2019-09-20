using System;
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
		/// <summary>
		/// Minimum end to end test that pulls some data from the database.
		/// </summary>
		[OperationContract]
		string TestDataStoreConnection();

		/// <summary>
		/// Returns alphabetized names.
		/// </summary>
		/// <param name="origin">Set to NULL or "All" for "select all". Set to one Category. Each Category includes all subcategories.</param>
		[OperationContract]
		ICollection<NameResponse> GetDetailedNames(string origin, GenderOption gender);

		/// <summary>
		/// Return tree-structure of valid categories (origins).
		/// </summary>
		[OperationContract]
		ICollection<CategoryResponse> GetCategories();
	}
}
