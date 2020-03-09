using InspecWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InspecWeb.Data.Seeders
{
    public class RegionSeeder : IEntityTypeConfiguration<Region>
    {

        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.HasData(
                new Region { Id = 1, Name = "เขตตรวจราชส่วนกลาง" },
                new Region { Id = 2, Name = "เขตตรวจราชการที่ 1" },
                new Region { Id = 3, Name = "เขตตรวจราชการที่ 2" },
                new Region { Id = 4, Name = "เขตตรวจราชการที่ 3" },
                new Region { Id = 5, Name = "เขตตรวจราชการที่ 4" },
                new Region { Id = 6, Name = "เขตตรวจราชการที่ 5" },
                new Region { Id = 7, Name = "เขตตรวจราชการที่ 6" },
                new Region { Id = 8, Name = "เขตตรวจราชการที่ 7" },
                new Region { Id = 9, Name = "เขตตรวจราชการที่ 8" },
                new Region { Id = 10, Name = "เขตตรวจราชการที่ 9" },
                new Region { Id = 11, Name = "เขตตรวจราชการที่ 10" },
                new Region { Id = 12, Name = "เขตตรวจราชการที่ 11" },
                new Region { Id = 13, Name = "เขตตรวจราชการที่ 12" },
                new Region { Id = 14, Name = "เขตตรวจราชการที่ 13" },
                new Region { Id = 15, Name = "เขตตรวจราชการที่ 14" },
                new Region { Id = 16, Name = "เขตตรวจราชการที่ 15" },
                new Region { Id = 17, Name = "เขตตรวจราชการที่ 16" },
                new Region { Id = 18, Name = "เขตตรวจราชการที่ 17" },
                new Region { Id = 19, Name = "เขตตรวจราชการที่ 18" }
            );
        }
    }
}