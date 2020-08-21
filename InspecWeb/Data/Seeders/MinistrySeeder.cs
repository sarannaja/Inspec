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
            new Ministry { Id = 1, Name = "สำนักนายกรัฐมนตรี", NameEN = "Office of the Prime Minister", ShortnameEN = "" },
            new Ministry { Id = 2, Name = "กระทรวงศึกษาธิการ", NameEN = "Ministry of Education", ShortnameEN = "" },
            new Ministry { Id = 3, Name = "กระทรวงการคลัง", NameEN = "Ministry of Finance", ShortnameEN = "" },
            new Ministry { Id = 4, Name = "กระทรวงการต่างประเทศ", NameEN = "Ministry of Foreign Affairs", ShortnameEN = "" },
            new Ministry { Id = 5, Name = "กระทรวงการท่องเที่ยวและกีฬา", NameEN = "Ministry of Tourism and Sports", ShortnameEN = "" },
            new Ministry { Id = 6, Name = "กระทรวงการพัฒนาสังคมและความมั่นคงของมนุษย์", NameEN = "Ministry of Social Development and Human Security", ShortnameEN = "" },
            new Ministry { Id = 7, Name = "กระทรวงเกษตรและสหกรณ์", NameEN = "Ministry of Agriculture and Cooperatives", ShortnameEN = "" },
            new Ministry { Id = 8, Name = "กระทรวงคมนาคม", NameEN = "Ministry of Transport", ShortnameEN = "" },
            new Ministry { Id = 9, Name = "กระทรวงทรัพยากรธรรมชาติและสิ่งแวดล้อม", NameEN = "Ministry of Natural Resources and Environment", ShortnameEN = "" },
            new Ministry { Id = 10, Name = "กระทรวงดิจิทัลเพื่อเศรษฐกิจและสังคม", NameEN = "Ministry of Digital Economy and Society", ShortnameEN = "" },
            new Ministry { Id = 11, Name = "กระทรวงพลังงาน", NameEN = "Ministry of Energy", ShortnameEN = "" },
            new Ministry { Id = 12, Name = "กระทรวงพาณิชย์", NameEN = "Ministry of Commerce", ShortnameEN = "" },
            new Ministry { Id = 13, Name = "กระทรวงมหาดไทย", NameEN = "Ministry of Interior", ShortnameEN = "" },
            new Ministry { Id = 14, Name = "กระทรวงยุติธรรม", NameEN = "Ministry of Justice", ShortnameEN = "" },
            new Ministry { Id = 15, Name = "กระทรวงแรงงาน", NameEN = "Ministry of Labor", ShortnameEN = "" },
            new Ministry { Id = 16, Name = "กระทรวงวัฒนธรรม", NameEN = "Ministry of Culture", ShortnameEN = "" },
            new Ministry { Id = 17, Name = "กระทรวงการอุดมศึกษา วิทยาศาสตร์ วิจัยและนวัตกรรม", NameEN = "Ministry of Higher Education, Science, Research and Innovation", ShortnameEN = "" },
            new Ministry { Id = 18, Name = "กระทรวงสาธารณสุข", NameEN = "Ministry of Public Health", ShortnameEN = "" },
            new Ministry { Id = 19, Name = "กระทรวงอุตสาหกรรม", NameEN = "Ministry of Industry", ShortnameEN = "" }

            );
        }
    }
}