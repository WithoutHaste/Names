﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NameEntities.Entities;

namespace NameEntities
{
	/// <summary>
	/// Used by other projects to access the data store.
	/// </summary>
	public class NameRepository
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