using InspecWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InspecWeb.Data.Seeders
{
    public class RelationSeeder : IEntityTypeConfiguration<FiscalYearRelation>
    {

        public void Configure(EntityTypeBuilder<FiscalYearRelation> builder)

        {
            builder.HasData(
                new FiscalYearRelation { Id = 1, FiscalYearId = 1, RegionId = 1, ProvinceId = 1 },
                new FiscalYearRelation { Id = 2, FiscalYearId = 1, RegionId = 2, ProvinceId = 10 },
                new FiscalYearRelation { Id = 3, FiscalYearId = 1, RegionId = 2, ProvinceId = 33 },
                new FiscalYearRelation { Id = 4, FiscalYearId = 1, RegionId = 2, ProvinceId = 52 },
                new FiscalYearRelation { Id = 5, FiscalYearId = 1, RegionId = 2, ProvinceId = 64 },
                new FiscalYearRelation { Id = 6, FiscalYearId = 1, RegionId = 2, ProvinceId = 65 },
                new FiscalYearRelation { Id = 7, FiscalYearId = 1, RegionId = 2, ProvinceId = 72 },
                new FiscalYearRelation { Id = 8, FiscalYearId = 1, RegionId = 3, ProvinceId = 19 },
                new FiscalYearRelation { Id = 9, FiscalYearId = 1, RegionId = 3, ProvinceId = 24 },
                new FiscalYearRelation { Id = 10, FiscalYearId = 1, RegionId = 3, ProvinceId = 29 },
                new FiscalYearRelation { Id = 11, FiscalYearId = 1, RegionId = 3, ProvinceId = 60 },
                new FiscalYearRelation { Id = 12, FiscalYearId = 1, RegionId = 4, ProvinceId = 3 },
                new FiscalYearRelation { Id = 13, FiscalYearId = 1, RegionId = 4, ProvinceId = 51 },
                new FiscalYearRelation { Id = 14, FiscalYearId = 1, RegionId = 4, ProvinceId = 67 },
                new FiscalYearRelation { Id = 15, FiscalYearId = 1, RegionId = 5, ProvinceId = 30 },
                new FiscalYearRelation { Id = 16, FiscalYearId = 1, RegionId = 5, ProvinceId = 39 },
                new FiscalYearRelation { Id = 17, FiscalYearId = 1, RegionId = 5, ProvinceId = 61 },
                new FiscalYearRelation { Id = 18, FiscalYearId = 1, RegionId = 5, ProvinceId = 62 },
                new FiscalYearRelation { Id = 19, FiscalYearId = 1, RegionId = 6, ProvinceId = 12 },
                new FiscalYearRelation { Id = 20, FiscalYearId = 1, RegionId = 6, ProvinceId = 22 },
                new FiscalYearRelation { Id = 21, FiscalYearId = 1, RegionId = 6, ProvinceId = 36 },
                new FiscalYearRelation { Id = 22, FiscalYearId = 1, RegionId = 6, ProvinceId = 58 },
                new FiscalYearRelation { Id = 23, FiscalYearId = 1, RegionId = 6, ProvinceId = 68 },
                new FiscalYearRelation { Id = 24, FiscalYearId = 1, RegionId = 7, ProvinceId = 2 },
                new FiscalYearRelation { Id = 25, FiscalYearId = 1, RegionId = 7, ProvinceId = 15 },
                new FiscalYearRelation { Id = 26, FiscalYearId = 1, RegionId = 7, ProvinceId = 35 },
                new FiscalYearRelation { Id = 27, FiscalYearId = 1, RegionId = 7, ProvinceId = 42 },
                new FiscalYearRelation { Id = 28, FiscalYearId = 1, RegionId = 7, ProvinceId = 49 },
                new FiscalYearRelation { Id = 29, FiscalYearId = 1, RegionId = 7, ProvinceId = 59 },
                new FiscalYearRelation { Id = 30, FiscalYearId = 1, RegionId = 8, ProvinceId = 25 },
                new FiscalYearRelation { Id = 31, FiscalYearId = 1, RegionId = 8, ProvinceId = 32 },
                new FiscalYearRelation { Id = 32, FiscalYearId = 1, RegionId = 8, ProvinceId = 47 },
                new FiscalYearRelation { Id = 33, FiscalYearId = 1, RegionId = 9, ProvinceId = 8 },
                new FiscalYearRelation { Id = 34, FiscalYearId = 1, RegionId = 9, ProvinceId = 9 },
                new FiscalYearRelation { Id = 35, FiscalYearId = 1, RegionId = 9, ProvinceId = 50 },
                new FiscalYearRelation { Id = 36, FiscalYearId = 1, RegionId = 10, ProvinceId = 7 },
                new FiscalYearRelation { Id = 37, FiscalYearId = 1, RegionId = 10, ProvinceId = 16 },
                new FiscalYearRelation { Id = 38, FiscalYearId = 1, RegionId = 10, ProvinceId = 18 },
                new FiscalYearRelation { Id = 39, FiscalYearId = 1, RegionId = 10, ProvinceId = 31 },
                new FiscalYearRelation { Id = 40, FiscalYearId = 1, RegionId = 10, ProvinceId = 63 },
                new FiscalYearRelation { Id = 41, FiscalYearId = 1, RegionId = 11, ProvinceId = 27 },
                new FiscalYearRelation { Id = 42, FiscalYearId = 1, RegionId = 11, ProvinceId = 55 },
                new FiscalYearRelation { Id = 43, FiscalYearId = 1, RegionId = 11, ProvinceId = 70 },
                new FiscalYearRelation { Id = 44, FiscalYearId = 1, RegionId = 11, ProvinceId = 71 },
                new FiscalYearRelation { Id = 45, FiscalYearId = 1, RegionId = 11, ProvinceId = 74 },
                new FiscalYearRelation { Id = 46, FiscalYearId = 1, RegionId = 12, ProvinceId = 20 },
                new FiscalYearRelation { Id = 47, FiscalYearId = 1, RegionId = 12, ProvinceId = 44 },
                new FiscalYearRelation { Id = 48, FiscalYearId = 1, RegionId = 12, ProvinceId = 57 },
                new FiscalYearRelation { Id = 49, FiscalYearId = 1, RegionId = 13, ProvinceId = 4 },
                new FiscalYearRelation { Id = 50, FiscalYearId = 1, RegionId = 13, ProvinceId = 6 },
                new FiscalYearRelation { Id = 51, FiscalYearId = 1, RegionId = 13, ProvinceId = 43 },
                new FiscalYearRelation { Id = 52, FiscalYearId = 1, RegionId = 13, ProvinceId = 48 },
                new FiscalYearRelation { Id = 53, FiscalYearId = 1, RegionId = 14, ProvinceId = 11 },
                new FiscalYearRelation { Id = 54, FiscalYearId = 1, RegionId = 14, ProvinceId = 21 },
                new FiscalYearRelation { Id = 55, FiscalYearId = 1, RegionId = 14, ProvinceId = 28 },
                new FiscalYearRelation { Id = 56, FiscalYearId = 1, RegionId = 14, ProvinceId = 69 },
                new FiscalYearRelation { Id = 57, FiscalYearId = 1, RegionId = 15, ProvinceId = 46 },
                new FiscalYearRelation { Id = 58, FiscalYearId = 1, RegionId = 15, ProvinceId = 56 },
                new FiscalYearRelation { Id = 59, FiscalYearId = 1, RegionId = 15, ProvinceId = 73 },
                new FiscalYearRelation { Id = 60, FiscalYearId = 1, RegionId = 15, ProvinceId = 77 },
                new FiscalYearRelation { Id = 61, FiscalYearId = 1, RegionId = 16, ProvinceId = 14 },
                new FiscalYearRelation { Id = 62, FiscalYearId = 1, RegionId = 16, ProvinceId = 45 },
                new FiscalYearRelation { Id = 63, FiscalYearId = 1, RegionId = 16, ProvinceId = 53 },
                new FiscalYearRelation { Id = 64, FiscalYearId = 1, RegionId = 16, ProvinceId = 54 },
                new FiscalYearRelation { Id = 65, FiscalYearId = 1, RegionId = 17, ProvinceId = 13 },
                new FiscalYearRelation { Id = 66, FiscalYearId = 1, RegionId = 17, ProvinceId = 26 },
                new FiscalYearRelation { Id = 67, FiscalYearId = 1, RegionId = 17, ProvinceId = 34 },
                new FiscalYearRelation { Id = 68, FiscalYearId = 1, RegionId = 17, ProvinceId = 41 },
                new FiscalYearRelation { Id = 69, FiscalYearId = 1, RegionId = 18, ProvinceId = 17 },
                new FiscalYearRelation { Id = 70, FiscalYearId = 1, RegionId = 18, ProvinceId = 38 },
                new FiscalYearRelation { Id = 71, FiscalYearId = 1, RegionId = 18, ProvinceId = 40 },
                new FiscalYearRelation { Id = 72, FiscalYearId = 1, RegionId = 18, ProvinceId = 66 },
                new FiscalYearRelation { Id = 73, FiscalYearId = 1, RegionId = 18, ProvinceId = 75 },
                new FiscalYearRelation { Id = 74, FiscalYearId = 1, RegionId = 19, ProvinceId = 5 },
                new FiscalYearRelation { Id = 75, FiscalYearId = 1, RegionId = 19, ProvinceId = 23 },
                new FiscalYearRelation { Id = 76, FiscalYearId = 1, RegionId = 19, ProvinceId = 37 },
                new FiscalYearRelation { Id = 77, FiscalYearId = 1, RegionId = 19, ProvinceId = 76 }
            );
        }
    }
}