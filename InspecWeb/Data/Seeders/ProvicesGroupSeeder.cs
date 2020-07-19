using InspecWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InspecWeb.Data.Seeders
{
    public class ProvincesGroupSeeder : IEntityTypeConfiguration<ProvincesGroup>
    {
        public void Configure(EntityTypeBuilder<ProvincesGroup> builder)
        {
            builder.HasData(
                new ProvincesGroup { Id = 1, Name = "ไม่มีกลุ่มจังหวัด" },
                new ProvincesGroup { Id = 2, Name = "ภาคกลางตอนบน" },
                new ProvincesGroup { Id = 3, Name = "ภาคกลางปริมณฑล" },
                new ProvincesGroup { Id = 4, Name = "ภาคกลางตอนล่าง 1" },
                new ProvincesGroup { Id = 5, Name = "ภาคกลางตอนล่าง 2" },
                new ProvincesGroup { Id = 6, Name = "ภาคใต้ฝั่งอ่าวไทย" },
                new ProvincesGroup { Id = 7, Name = "ภาคใต้ฝั่งอันดามัน" },
                new ProvincesGroup { Id = 8, Name = "ภาคใต้ชายแดน" },
                new ProvincesGroup { Id = 9, Name = "ภาคตะวันออก 1" },
                new ProvincesGroup { Id = 10, Name = "ภาคตะวันออก 2" },
                new ProvincesGroup { Id = 11, Name = "ภาคตะวันออกเฉียงเหนือตอนบน 1" },
                new ProvincesGroup { Id = 12, Name = "ภาคตะวันออกเฉียงเหนือตอนบน 2" },
                new ProvincesGroup { Id = 13, Name = "ภาคตะวันออกเฉียงเหนือตอนกลาง" },
                new ProvincesGroup { Id = 14, Name = "ภาคตะวันออกเฉียงเหนือตอนล่าง 1" },
                new ProvincesGroup { Id = 15, Name = "ภาคตะวันออกเฉียงเหนือตอนล่าง 2" },
                new ProvincesGroup { Id = 16, Name = "ภาคเหนือตอนบน 1" },
                new ProvincesGroup { Id = 17, Name = "ภาคเหนือตอนบน 2" },
                new ProvincesGroup { Id = 18, Name = "ภาคเหนือตอนล่าง 1" },
                new ProvincesGroup { Id = 19, Name = "ภาคเหนือตอนล่าง 2" }

            );
        }
    }
}