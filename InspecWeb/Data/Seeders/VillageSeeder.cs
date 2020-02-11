using InspecWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InspecWeb.Data.Seeders
{
    public class SubdistrictSeeder : IEntityTypeConfiguration<Subdistrict>
    {

        public void Configure(EntityTypeBuilder<Subdistrict> builder)
        {
            builder.HasData(
                new Village { Id = 1, SubdistrictId = 1, Name = "พระบรมมหาราชวัง" }
            );
        }
    }
}