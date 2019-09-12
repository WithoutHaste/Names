using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Names.Domain;
using Names.Read.UseCases.Data;

namespace Names.Read.UseCases
{
    public static class GetDetailedNamesAlphabetized
    {
		public static ICollection<NameOutput> Execute(INamesGateway gateway)
		{
			return gateway.GetNamesWithDetails().Select(record => new NameOutput() {
				Name = record.Name,
				Origins = record.Details.Select(detail => detail.Origin).Distinct().ToList()
			}).OrderBy(output => output.Name).ToList();
		}
    }
}
