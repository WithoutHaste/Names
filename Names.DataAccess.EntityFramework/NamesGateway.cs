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
		public string TestDataStoreConnection()
		{
			using(NamesContext context = new NamesContext())
			{
				List<NameRecord> names = context.Names.ToList();
				return "Found " + names.Count + " names.";
			}
		}

		public ICollection<NameWithDetailResult> GetNamesWithDetails(string origin)
		{
			using(NamesContext context = new NamesContext())
			{
				return context.GetNamesByOrigin(origin).ToList();
			}
		}

		public ICollection<CategoryRecord> GetCategories()
		{
			using(NamesContext context = new NamesContext())
			{
				return context.Categories.ToList();
			}
		}

		public NameWithDetailResult[] GetAlphabetizedPagedNamesWithDetails(int pageIndex, int rowsPerPage)
		{
			using(NamesContext context = new NamesContext())
			{
				return context.GetPagedNames(pageIndex, rowsPerPage);
			}
		}

		public void EditNameDetails(EditNameDetailRequest[] editRequests)
		{
			int[] ids = editRequests.Select(request => request.NameDetailId).ToArray();
			using(NamesContext context = new NamesContext())
			{
				NameDetailRecord[] records = context.NameDetails.Where(record => ids.Contains(record.Id)).ToArray();
				foreach(NameDetailRecord record in records)
				{
					EditNameDetailRequest request = editRequests.First(r => r.NameDetailId == record.Id);
					record.Origin = request.Origin;
					record.Meaning = request.Meaning;
				}
				context.SaveChanges();
			}
		}
	}
}
