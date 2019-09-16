using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Names.Edit.SoapService.Contracts
{
	[ServiceContract]
    public interface INamesEditService
    {
		/// <summary>
		/// Returns 1 page of data, alphabetized, first names, with details.
		/// </summary>
		/// <param name="pageIndex">0-based index.</param>
		/// <param name="rowsPerPage"></param>
		/// <returns></returns>
		[OperationContract]
		NameDetailResponse[] GetPagedNames(int pageIndex, int rowsPerPage);

		/// <summary>
		/// Batch update of NameDetailRecords. Transaction based.
		/// </summary>
		/// <param name="editRequests"></param>
		[OperationContract]
		[TransactionFlow(TransactionFlowOption.Allowed)]
		void EditNameDetails(EditNameDetailRequest[] editRequests);

		/// <summary>
		/// Returns all spellings, ordered by CommonName then VariationName.
		/// </summary>
		/// <returns></returns>
		[OperationContract]
		SpellingResponse[] GetSpellings();

		/// <summary>
		/// Add a spelling association between these names.
		/// </summary>
		/// <param name="commonName"></param>
		/// <param name="variationName"></param>
		[OperationContract]
		void AddSpelling(string commonName, string variationName);
    }
}
