using InspecWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InspecWeb.Data.Seeders
{
    public class SectorSeeder : IEntityTypeConfiguration<Sector>
    {
        public void Configure(EntityTypeBuilder<Sector> builder)
        {
            builder.HasData(
                new Sector { Id = 1, Name = "กลาง" },
                new Sector { Id = 2, Name = "ภาคใต้" },
                new Sector { Id = 3, Name = "ภาคตะวันออก" },
                new Sector { Id = 4, Name = "ภาคตะวันออกเฉียงเหนือ" },
                new Sector { Id = 5, Name = "ภาคเหนือ" }
            );
        }
    }
}