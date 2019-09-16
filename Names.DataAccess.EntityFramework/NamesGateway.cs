﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Threading;
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

		public SpellingRecord[] GetSpellings()
		{
			using(NamesContext context = new NamesContext())
			{
				return context.Spellings.Include("CommonName").Include("VariationName")
					.OrderBy(spelling => spelling.CommonName.Name)
					.ThenBy(spelling => spelling.VariationName.Name)
					.ToArray();
			}
		}

		public void AddSpelling(string commonName, string variationName)
		{
			CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
			TextInfo textInfo = cultureInfo.TextInfo;
			commonName = textInfo.ToTitleCase(commonName);
			variationName = textInfo.ToTitleCase(variationName);

			using(NamesContext context = new NamesContext())
			{
				NameRecord commonNameRecord = context.Names.FirstOrDefault(name => name.Name == commonName);
				NameRecord variationNameRecord = context.Names.FirstOrDefault(name => name.Name == variationName);
				if(commonNameRecord == null)
					throw new Exception("Common name not found.");
				if(variationNameRecord == null)
					throw new Exception("Variation name not found.");

				SpellingRecord record = context.Spellings.FirstOrDefault(spelling => spelling.CommonNameId == commonNameRecord.Id && spelling.VariationNameId == variationNameRecord.Id);
				if(record != null)
					return;
				record = new SpellingRecord() { CommonNameId = commonNameRecord.Id, VariationNameId = variationNameRecord.Id };
				context.Spellings.Add(record);
				context.SaveChanges();
			}
		}
	}
}
