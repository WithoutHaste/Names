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
		public static ICollection<NameOutput> Execute(INamesGateway gateway, string origin)
		{
			return gateway.GetNamesWithDetails(origin).GroupBy(record => record.Name).Select(group => new NameOutput() {
				Name = group.Key,
				IsBoy = group.Any(detail => detail.IsBoy == true),
				IsGirl = group.Any(detail => detail.IsGirl == true),
				Origins = group.Select(detail => detail.Origin).Distinct().ToList()
			}).OrderBy(output => output.Name).ToList();
		}
    }
}
