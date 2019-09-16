using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Names.Domain;

namespace Names.Edit.UseCases
{
	public static class AddSpelling
	{
		public static void Execute(INamesGateway gateway, string commonName, string variationName)
		{
			gateway.AddSpelling(commonName, variationName);
		}
	}
}
