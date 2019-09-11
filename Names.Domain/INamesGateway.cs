using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Names.Domain.Entities;

namespace Names.Domain
{
	/// <summary>
	/// Contract for accessing the data store.
	/// </summary>
	public interface INamesGateway
	{
		string TestConnection();
	}
}
