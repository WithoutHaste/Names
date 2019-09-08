using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameEntities
{
	public abstract class BaseContext<T> : DbContext where T:DbContext
	{
		static BaseContext()
		{
			Database.SetInitializer<T>(null);
		}

		protected BaseContext(string connectionStringToName) : base(connectionStringToName)
		{
		}

		public override int SaveChanges()
		{
			return base.SaveChanges();
		}
	}
}
