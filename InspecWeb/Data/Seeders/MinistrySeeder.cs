using InspecWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InspecWeb.Data.Seeders
{
    public class MinistrySeeder : IEntityTypeConfiguration<Ministry>
    {

        public void Configure(EntityTypeBuilder<Ministry> builder)
        {
            builder.HasData(
                new Ministry { Id = 1,Name = "สำนักนายกรัฐมนตรี" },
                new Ministry { Id = 2,Name = "กระทรวงกลาโหม" },
                new Ministry { Id = 3, Name = "กระทรวงการคลัง" },
                new Ministry { Id = 4, Name = "กระทรวงการต่างประเทศ" },
                new Ministry { Id = 5, Name = "กระทรวงการท่องเที่ยวและกีฬา" },
                new Ministry { Id =6, Name = "กระทรวงการพัฒนาสังคมและความมั่นคงของมนุษย์" },
                new Ministry { Id = 7, Name = "กระทรวงเกษตรและสหกรณ์" },
                new Ministry { Id = 8, Name = "กระทรวงคมนาคม" },
                new Ministry { Id = 9, Name = "กระทรวงทรัพยากรธรรมชาติและสิ่งแวดล้อม" },
                new Ministry { Id = 10, Name = "กระทรวงดิจิทัลเพื่อเศรษฐกิจและสังคม" },
                new Ministry { Id = 11, Name = "กระทรวงพลังงาน" },
                new Ministry { Id = 12, Name = "กระทรวงพาณิชย์" },
                new Ministry { Id = 13, Name = "กระทรวงมหาดไทย" },
                new Ministry { Id = 14, Name = "กระทรวงยุติธรรม" },
                new Ministry { Id = 15, Name = "กระทรวงแรงงาน" },
                new Ministry { Id = 16, Name = "กระทรวงวัฒนธรรม" },
                new Ministry { Id = 17, Name = "กระทรวงการอุดมศึกษา วิทยาศาสตร์ วิจัยและนวัตกรรม" },
                new Ministry { Id = 18, Name = "กระทรวงสาธารณสุข" },
                new Ministry { Id = 19, Name = "กระทรวงอุตสาหกรรม" }
            );
        }
    }
}