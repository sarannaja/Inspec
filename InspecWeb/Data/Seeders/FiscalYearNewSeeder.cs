using InspecWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InspecWeb.Data.Seeders
{
    public class FiscalYearNewSeeder : IEntityTypeConfiguration<FiscalYearNew>
    {

        public void Configure(EntityTypeBuilder<FiscalYearNew> builder)
        {
            builder.HasData(
                   new FiscalYearNew { Id = 1, Year = 2563, StartDate = new DateTime(2019 - 10 - 01), EndDate = new DateTime(2020 - 09 - 30) }
            );
        }
    }
}