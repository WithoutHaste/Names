using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Names.Domain.Entities;

namespace Names.Domain
{
	/// <summary>
	/// Contract for accessing the data store.
	/// </summary>
	public interface INamesGateway
	{
		/// <summary>
		/// Minumum test that data store can be queried and data returned.
		/// </summary>
		string TestDataStoreConnection();

		/// <summary>
		/// Returns first names, with details.
		/// </summary>
		/// <param name="origin">Set to NULL for "all", or to one Category. Each Category includes all subcategories.</param>
		ICollection<NameWithDetailResult> GetNamesWithDetails(string origin);
	}
}
