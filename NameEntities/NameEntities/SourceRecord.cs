using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameEntities
{
	/// <summary>
	/// Data source.
	/// </summary>
	public class SourceRecord
	{
		public int Id { get; set; }
		/// <summary>
		/// Human-legible name of data source.
		/// </summary>
		public string Name { get; set; }
		public string Url { get; set; }
	}
}
