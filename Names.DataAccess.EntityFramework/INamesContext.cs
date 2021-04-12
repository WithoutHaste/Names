using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Names.Domain.Entities;

namespace Names.DataAccess.EntityFramework
{
    public interface INamesContext
    {
        DbSet<NameRecord> Names { get; set; }
        DbSet<NameDetailRecord> NameDetails { get; set; }
        DbSet<SpellingRecord> Spellings { get; set; }
        DbSet<NickNameRecord> NickNames { get; set; }
        DbSet<CategoryRecord> Categories { get; set; }
        DbSet<SourceRecord> Sources { get; set; }

        List<NameWithDetailResult> GetNamesByOrigin(string origin);

        NameWithDetailResult[] GetPagedNames(int pageIndex, int rowsPerPage);

        int SaveChanges();
    }
}
