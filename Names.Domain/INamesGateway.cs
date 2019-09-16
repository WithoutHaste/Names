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

		/// <summary>
		/// Returns all categories.
		/// </summary>
		ICollection<CategoryRecord> GetCategories();

		/// <summary>
		/// Returns 1 page of data, alphabetized, first names, with details.
		/// </summary>
		/// <param name="pageIndex">0-based index.</param>
		/// <param name="rowsPerPage"></param>
		/// <returns></returns>
		NameWithDetailResult[] GetAlphabetizedPagedNamesWithDetails(int pageIndex, int rowsPerPage);

		/// <summary>
		/// Batch edit of NameDetailRecords.
		/// </summary>
		/// <param name="editRequests"></param>
		void EditNameDetails(EditNameDetailRequest[] editRequests);

		/// <summary>
		/// Returns all spellings, ordered by CommonName, with Names attached.
		/// </summary>
		/// <returns></returns>
		SpellingRecord[] GetSpellings();

		/// <summary>
		/// Add a new spelling record.
		/// </summary>
		void AddSpelling(string commonName, string variationName);

		/// <summary>
		/// Returns all nicknames, order by FullName then NickName, with Names attached.
		/// </summary>
		/// <returns></returns>
		NickNameRecord[] GetNickNames();

		/// <summary>
		/// Add a new nickname record.
		/// </summary>
		void AddNickName(string fullName, string nickName);
	}
}
