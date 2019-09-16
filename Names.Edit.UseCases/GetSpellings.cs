using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Names.Domain;
using Names.Domain.Entities;
using Names.Edit.UseCases.Data;

namespace Names.Edit.UseCases
{
	public static class GetSpellings
	{
		public static SpellingOutput[] Execute(INamesGateway gateway)
		{
			return gateway.GetSpellings().Select(record => new SpellingOutput() {
				CommonNameId = record.CommonNameId,
				CommonName = record.CommonName.Name,
				VariationNameId = record.VariationNameId,
				VariationName = record.VariationName.Name
			}).ToArray();
		}
	}
}
