using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Names.Domain;
using Names.Domain.Entities;
using Names.Read.UseCases.Data;

namespace Names.Read.UseCases
{
    public static class GetDetailedNamesAlphabetized
    {
		public static ICollection<NameOutput> Execute(INamesGateway gateway, string origin, bool includeBoys, bool includeGirls)
		{
			return gateway.GetNamesWithDetails(origin).GroupBy(record => record.Name).Select(group => new NameOutput() {
				Name = group.Key,
				FirstLetter = group.First().FirstLetter,
				IsBoy = ConvertIsBoy(group),
				IsGirl = ConvertIsGirl(group),
				Origins = group.Select(detail => detail.Origin).Distinct().OrderBy(text => text).ToList()
			}).Where(output =>
				(includeBoys == true && output.IsBoy != false) ||
				(includeGirls == true && output.IsGirl != false)
			).OrderBy(output => output.Name).ToList();
		}

		private static bool? ConvertIsBoy(IGrouping<string, NameWithDetailResult> group)
		{
			if(group.Any(detail => detail.IsBoy == true))
				return true;
			if(group.Any(detail => detail.IsBoy == false))
				return false;
			return null;
		}

		private static bool? ConvertIsGirl(IGrouping<string, NameWithDetailResult> group)
		{
			if(group.Any(detail => detail.IsGirl == true))
				return true;
			if(group.Any(detail => detail.IsGirl == false))
				return false;
			return null;
		}
	}
}
