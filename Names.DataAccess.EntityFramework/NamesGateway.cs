using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Names.Domain;
using Names.Domain.Entities;

namespace Names.DataAccess.EntityFramework
{
	/// <summary>
	/// Used by other projects to access the data store.
	/// </summary>
	public class NamesGateway : INamesGateway
	{
		public string TestConnection()
		{
			using(NameContext context = new NameContext())
			{
				List<NameRecord> names = context.Names.ToList();
				return "Found " + names.Count + " names.";
			}
		}
	}
}
