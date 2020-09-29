using InspecWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InspecWeb.Data.Seeders
{
    public class FiscalYearSeeder : IEntityTypeConfiguration<FiscalYear>
    {

        public void Configure(EntityTypeBuilder<FiscalYear> builder)
        {
            builder.HasData(
                   new FiscalYear { Id = 1, Year = "2563" }
            );
        }
    }
}