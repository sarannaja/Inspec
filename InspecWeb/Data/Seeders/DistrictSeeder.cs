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
                new District { Id = 3, ProvinceId = 1, Name = "เขตหนองจอก" },
                new District { Id = 4, ProvinceId = 1, Name = "เขตบางรัก" },
                new District { Id = 5, ProvinceId = 1, Name = "เขตบางเขน" },
                new District { Id = 6, ProvinceId = 1, Name = "เขตบางกะปิ" },
                new District { Id = 7, ProvinceId = 1, Name = "เขตปทุมวัน" },
                new District { Id = 8, ProvinceId = 1, Name = "เขตป้อมปราบศัตรูพ่าย" },
                new District { Id = 9, ProvinceId = 1, Name = "เขตพระโขนง" },
                new District { Id = 10, ProvinceId = 1, Name = "เขตมีนบุรี" },
                new District { Id = 11, ProvinceId = 1, Name = "เขตลาดกระบัง" },
                new District { Id = 12, ProvinceId = 1, Name = "เขตยานนาวา" },
                new District { Id = 13, ProvinceId = 1, Name = "เขตสัมพันธวงศ์" },
                new District { Id = 14, ProvinceId = 1, Name = "เขตพญาไท" },
                new District { Id = 15, ProvinceId = 1, Name = "เขตธนบุรี" },
                new District { Id = 16, ProvinceId = 1, Name = "เขตบางกอกใหญ่" },
                new District { Id = 17, ProvinceId = 1, Name = "เขตห้วยขวาง" },
                new District { Id = 18, ProvinceId = 1, Name = "เขตคลองสาน" },
                new District { Id = 19, ProvinceId = 1, Name = "เขตตลิ่งชัน" },
                new District { Id = 20, ProvinceId = 1, Name = "เขตบางกอกน้อย" },
                new District { Id = 21, ProvinceId = 1, Name = "เขตบางขุนเทียน" },
                new District { Id = 22, ProvinceId = 1, Name = "เขตภาษีเจริญ" },
                new District { Id = 23, ProvinceId = 1, Name = "เขตหนองแขม" },
                new District { Id = 24, ProvinceId = 1, Name = "เขตราษฎร์บูรณะ" },
                new District { Id = 25, ProvinceId = 1, Name = "เขตบางพลัด" },
                new District { Id = 26, ProvinceId = 1, Name = "เขตดินแดง" },
                new District { Id = 27, ProvinceId = 1, Name = "เขตบึงกุ่ม" },
                new District { Id = 28, ProvinceId = 1, Name = "เขตสาทร" },
                new District { Id = 29, ProvinceId = 1, Name = "เขตบางซื่อ" },
                new District { Id = 30, ProvinceId = 1, Name = "เขตจตุจักร" },
                new District { Id = 31, ProvinceId = 1, Name = "เขตบางคอแหลม" },
                new District { Id = 32, ProvinceId = 1, Name = "เขตประเวศ" },
                new District { Id = 33, ProvinceId = 1, Name = "เขตคลองเตย" },
                new District { Id = 34, ProvinceId = 1, Name = "เขตสวนหลวง" },
                new District { Id = 35, ProvinceId = 1, Name = "เขตจอมทอง" },
                new District { Id = 36, ProvinceId = 1, Name = "เขตดอนเมือง" },
                new District { Id = 37, ProvinceId = 1, Name = "เขตราชเทวี" },
                new District { Id = 38, ProvinceId = 1, Name = "เขตลาดพร้าว" },
                new District { Id = 39, ProvinceId = 1, Name = "เขตวัฒนา" },
                new District { Id = 40, ProvinceId = 1, Name = "เขตบางแค" },
                new District { Id = 41, ProvinceId = 1, Name = "เขตหลักสี่" },
                new District { Id = 42, ProvinceId = 1, Name = "เขตสายไหม" },
                new District { Id = 43, ProvinceId = 1, Name = "เขตคันนายาว" },
                new District { Id = 44, ProvinceId = 1, Name = "เขตสะพานสูง" },
                new District { Id = 45, ProvinceId = 1, Name = "เขตวังทองหลาง" },
                new District { Id = 46, ProvinceId = 1, Name = "เขตคลองสามวา" },
                new District { Id = 47, ProvinceId = 1, Name = "เขตบางนา" },
                new District { Id = 48, ProvinceId = 1, Name = "เขตทวีวัฒนา" },
                new District { Id = 49, ProvinceId = 1, Name = "เขตทุ่งครุ" },
                new District { Id = 50, ProvinceId = 1, Name = "เขตบางบอน" }
            );
        }
    }
}