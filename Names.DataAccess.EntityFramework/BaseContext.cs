using Microsoft.EntityFrameworkCore;

namespace Names.DataAccess.EntityFramework
{
	public abstract class BaseContext<T> : DbContext where T:DbContext
	{
		protected BaseContext(DbContextOptions<NamesContext> options) : base(options)
		{
		}

		public override int SaveChanges()
		{
			return base.SaveChanges();
		}
	}
}
