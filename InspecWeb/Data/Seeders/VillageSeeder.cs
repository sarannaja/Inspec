using InspecWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InspecWeb.Data.Seeders
{
    public class VillageSeeder : IEntityTypeConfiguration<Village>
    {

        public void Configure(EntityTypeBuilder<Village> builder)
        {
            builder.HasData(
                new Village { Id = 1, SubdistrictId = 1, Name = "พระบรมมหาราชวัง" }
            );
        }
    }
}