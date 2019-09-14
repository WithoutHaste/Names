using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Names.Domain;
using Names.Edit.UseCases.Data;

namespace Names.Edit.UseCases
{
    public static class GetPagedDetailedNamesAlphabetized
    {
		public static DetailedNameOutput[] Execute(INamesGateway gateway, int pageIndex, int rowsPerPage)
		{
			return gateway.GetAlphabetizedPagedNamesWithDetails(pageIndex, rowsPerPage).Select(record => new DetailedNameOutput() {
				NameId = record.NameId,
				Name = record.Name,
				FirstLetter = record.FirstLetter,
				IsFamiliar = record.IsFamiliar,
				NameDetailId = record.NameDetailId,
				Origin = record.Origin,
				Meaning = record.Meaning
			}).ToArray();
		}
    }
}
