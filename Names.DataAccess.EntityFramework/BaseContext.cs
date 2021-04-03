using Microsoft.EntityFrameworkCore;

namespace Names.DataAccess.EntityFramework
{
	internal abstract class BaseContext<T> : DbContext where T:DbContext
	{
		protected BaseContext(string connectionStringToName) : base()
		{
		}

		public override int SaveChanges()
		{
			return base.SaveChanges();
		}
	}
}
