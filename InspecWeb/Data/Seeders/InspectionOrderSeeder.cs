using InspecWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InspecWeb.Data.Seeders
{
    public class InspectionOrderSeeder : IEntityTypeConfiguration<InspectionOrder>
    {

        public void Configure(EntityTypeBuilder<InspectionOrder> builder)
        {
            builder.HasData(
                new InspectionOrder
                {
                    Id = 1,
                    Year = "2563",
                    Order = "คำสั่งสำนักนายกรัฐมนตรี ที่ 361/2562",
                    Name = "การตรวจราชการประจำปีงบประมาณ พ.ศ. 2563",
                    CreateBy = "ผู้ตรวจราชการสำนักนายกรัฐมนตรี",
                    File = "insp_com63.pdf",
                    
                },
                new InspectionOrder
                {
                    Id = 2,
                    Year = "2562",
                    Order = "คำสั่งสำนักนายกรัฐมนตรี ที่ 343/2561",
                    Name = "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2562",
                    CreateBy = "ผู้ตรวจราชการสำนักนายกรัฐมนตรี",
                    File = "insp_com62.pdf",
                  
                },
                new InspectionOrder
                {
                    Id = 3,
                    Year = "2561",
                    Order = "คำสั่งสำนักนายกรัฐมนตรี ที่ 15/2561",
                    Name = "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2561",
                    CreateBy = "ผู้ตรวจราชการสำนักนายกรัฐมนตรี",
                    File = "insp_com61.pdf",
                   
                },
                new InspectionOrder
                {
                    Id = 4,
                    Year = "2560",
                    Order = "คำสั่งสำนักนายกรัฐมนตรี ที่ 264/2559",
                    Name = "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2560",
                    CreateBy = "ผู้ตรวจราชการสำนักนายกรัฐมนตรี",
                    File = "insp_com60.pdf",
                    
                },
                new InspectionOrder
                {
                    Id = 5,
                    Year = "2559",
                    Order = "คำสั่งสำนักนายกรัฐมนตรี ที่ 396/2558",
                    Name = "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2559",
                    CreateBy = "ผู้ตรวจราชการสำนักนายกรัฐมนตรี",
                    File = "insp_com59.pdf",
                   
                },
                new InspectionOrder
                {
                    Id = 6,
                    Year = "2558",
                    Order = "คำสั่งสำนักนายกรัฐมนตรี ที่ 256/2557",
                    Name = "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2558",
                    CreateBy = "ผู้ตรวจราชการสำนักนายกรัฐมนตรี",
                    File = "insp_com58.pdf",
                  
                },
                new InspectionOrder
                {
                    Id = 7,
                    Year = "2557",
                    Order = "คำสั่งสำนักนายกรัฐมนตรี ที่ 328/2556 ",
                    Name = "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2557",
                    CreateBy = "ผู้ตรวจราชการสำนักนายกรัฐมนตรี",
                    File = "",
                   
                },
                new InspectionOrder
                {
                    Id = 8,
                    Year = "2556",
                    Order = "คำสั่งสำนักนายกรัฐมนตรี ที่ 302/2555",
                    Name = "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2556",
                    CreateBy = "ผู้ตรวจราชการสำนักนายกรัฐมนตรี",
                    File = "insp_com56.pdf",
                   
                },
                new InspectionOrder
                {
                    Id = 9,
                    Year = "2555",
                    Order = "คำสั่งสำนักนายกรัฐมนตรี ที่ 19/2555",
                    Name = "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2555",
                    CreateBy = "ผู้ตรวจราชการสำนักนายกรัฐมนตรี",
                    File = "insp_com55.pdf",
                    
                },
                new InspectionOrder
                {
                    Id = 10,
                    Year = "2554",
                    Order = "คำสั่งสำนักนายกรัฐมนตรี ที่ 8/2554",
                    Name = "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2554",
                    CreateBy = "ผู้ตรวจราชการสำนักนายกรัฐมนตรี",
                    File = "insp_com54.pdf",
                    
                },
                new InspectionOrder
                {
                    Id = 11,
                    Year = "2553",
                    Order = "คำสั่งสำนักนายกรัฐมนตรี ที่ 8/2553",
                    Name = "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2553",
                    CreateBy = "ผู้ตรวจราชการสำนักนายกรัฐมนตรี",
                    File = "insp_com53.pdf",
                    
                },
                new InspectionOrder
                {
                    Id = 12,
                    Year = "2552",
                    Order = "คำสั่งสำนักนายกรัฐมนตรี ที่ 21/2552",
                    Name = "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2552",
                    CreateBy = "ผู้ตรวจราชการสำนักนายกรัฐมนตรี",
                    File = "insp_com52.pdf",
                    
                },
                new InspectionOrder
                {
                    Id = 13,
                    Year = "2551",
                    Order = "คำสั่งสำนักนายกรัฐมนตรี ที่ 226/2550",
                    Name = "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2551",
                    CreateBy = "ผู้ตรวจราชการสำนักนายกรัฐมนตรี",
                    File = "insp_com52.tif",
                }
            );
        }
    }
}