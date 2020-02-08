using InspecWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InspecWeb.Data.Seeders
{
    public class DistrictSeeder : IEntityTypeConfiguration<District>
    {

        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.HasData(
                new District { Id = 1, ProvinceId = 1, Name = "เขตพระนคร" },
                new District { Id = 2, ProvinceId = 1, Name = "เขตดุสิต" },
                new District { Id = 3, ProvinceId = 1, Name = "เขตหนองจอก" }
            );
        }
    }
}