using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Names.Domain;
using Names.Domain.Entities;

namespace Names.DataAccess.EntityFramework
{
	/// <summary>
	/// Used by other projects to access the data store.
	/// </summary>
	public class NamesGateway : INamesGateway
	{
		private readonly INamesContext _context;

		public NamesGateway(INamesContext context)
		{
			_context = context;
		}

		public string TestDataStoreConnection()
		{
			List<NameRecord> names = _context.Names.ToList();
			return "Found " + names.Count + " names.";
		}

		public ICollection<NameWithDetailResult> GetNamesWithDetails(string origin)
		{
			return _context.GetNamesByOrigin(origin).ToList();
		}

		public ICollection<CategoryRecord> GetCategories()
		{
			return _context.Categories.ToList();
		}

		public NameWithDetailResult[] GetAlphabetizedPagedNamesWithDetails(int pageIndex, int rowsPerPage)
		{
			return _context.GetPagedNames(pageIndex, rowsPerPage);
		}

		public void EditNameDetails(EditNameDetailRequest[] editRequests)
		{
			int[] ids = editRequests.Select(request => request.NameDetailId).ToArray();
			NameDetailRecord[] records = _context.NameDetails.Where(record => ids.Contains(record.Id)).ToArray();
			foreach(NameDetailRecord record in records)
			{
				EditNameDetailRequest request = editRequests.First(r => r.NameDetailId == record.Id);
				if(request.IsDeleted)
				{
					_context.NameDetails.Remove(record);
				}
				else
				{
					if(request.Origin.Contains(","))
					{
						string[] origins = request.Origin.Split(',');
						request.Origin = origins[0].Trim();
						for(int i = 1; i < origins.Length; i++)
						{
							NameDetailRecord newRecord = new NameDetailRecord() {
								NameId = record.NameId,
								SourceId = record.SourceId,
								IsBoy = record.IsBoy,
								IsGirl = record.IsGirl,
								IsFirstName = record.IsFirstName,
								IsLastName = record.IsLastName,
								Origin = origins[i].Trim(),
								Meaning = request.Meaning,
								CreateDateTime = record.CreateDateTime
							};
							_context.NameDetails.Add(newRecord);
						}
					}
					record.Origin = request.Origin;
					record.Meaning = request.Meaning;
				}
			}
			_context.SaveChanges();
		}

		public SpellingRecord[] GetSpellings()
		{
			return _context.Spellings.Include(spelling => spelling.CommonName).Include(spelling => spelling.VariationName)
				.OrderBy(spelling => spelling.CommonName.Name)
				.ThenBy(spelling => spelling.VariationName.Name)
				.ToArray();
		}

		public void AddSpelling(string commonName, string variationName)
		{
			CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
			TextInfo textInfo = cultureInfo.TextInfo;
			commonName = textInfo.ToTitleCase(commonName);
			variationName = textInfo.ToTitleCase(variationName);

			NameRecord commonNameRecord = _context.Names.FirstOrDefault(name => name.Name == commonName);
			NameRecord variationNameRecord = _context.Names.FirstOrDefault(name => name.Name == variationName);
			if(commonNameRecord == null)
				throw new Exception("Common name not found.");
			if(variationNameRecord == null)
				throw new Exception("Variation name not found.");

			SpellingRecord record = _context.Spellings.FirstOrDefault(spelling => spelling.CommonNameId == commonNameRecord.Id && spelling.VariationNameId == variationNameRecord.Id);
			if(record != null)
				return;
			record = new SpellingRecord() { CommonNameId = commonNameRecord.Id, VariationNameId = variationNameRecord.Id };
			_context.Spellings.Add(record);
			_context.SaveChanges();
		}

		public NickNameRecord[] GetNickNames()
		{
			return _context.NickNames.Include(nickname => nickname.FullName).Include(nickname => nickname.NickName)
				.OrderBy(nickname => nickname.FullName.Name)
				.ThenBy(nickname => nickname.NickName.Name)
				.ToArray();
		}

		public void AddNickName(string fullName, string nickName)
		{
			CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
			TextInfo textInfo = cultureInfo.TextInfo;
			fullName = textInfo.ToTitleCase(fullName);
			nickName = textInfo.ToTitleCase(nickName);

			NameRecord fullNameRecord = _context.Names.FirstOrDefault(name => name.Name == fullName);
			NameRecord nickNameRecord = _context.Names.FirstOrDefault(name => name.Name == nickName);
			if(fullNameRecord == null)
				throw new Exception("Full name not found.");
			if(nickNameRecord == null)
				throw new Exception("Nickname not found.");

			NickNameRecord record = _context.NickNames.FirstOrDefault(nickname => nickname.FullNameId == fullNameRecord.Id && nickname.NickNameId == nickNameRecord.Id);
			if(record != null)
				return;
			record = new NickNameRecord() { FullNameId = fullNameRecord.Id, NickNameId = nickNameRecord.Id };
			_context.NickNames.Add(record);
			_context.SaveChanges();
		}
	}
}
