using InspecWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InspecWeb.Data.Seeders
{
    public class FiscalYearNewSeeder : IEntityTypeConfiguration<FiscalYearNew>
    {

        public void Configure(EntityTypeBuilder<FiscalYearNew> builder)
        {
            builder.HasData(
                   new FiscalYearNew { Id = 1, Year = 2563 }
            );
        }
    }
}