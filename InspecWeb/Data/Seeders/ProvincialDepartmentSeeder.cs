using InspecWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InspecWeb.Data.Seeders
{
    public class ProvincialDepartmentSeeder : IEntityTypeConfiguration<ProvincialDepartment>
    {
        public void Configure(EntityTypeBuilder<ProvincialDepartment> builder)
        {
            builder.HasData(
                 new ProvincialDepartment { Id = 1, DepartmentId = 1, Name = "สำนักงานประชาสัมพันธ์" },
                 new ProvincialDepartment { Id = 2, DepartmentId = 24, Name = "สำนักงานคลังจังหวัด" },
                 new ProvincialDepartment { Id = 3, DepartmentId = 46, Name = "สำนักงานท่องเที่ยวและกีฬาจังหวัด" },
                 new ProvincialDepartment { Id = 4, DepartmentId = 50, Name = "สำนักงานพัฒนาสังคมและความมั่นคงของมนุษย์จังหวัด " },
                 new ProvincialDepartment { Id = 5, DepartmentId = 56, Name = "สำนักงานเกษตรและสหกรณ์จังหวัด" },
                 new ProvincialDepartment { Id = 6, DepartmentId = 59, Name = "สำนักงานประมงจังหวัด" },
                 new ProvincialDepartment { Id = 7, DepartmentId = 60, Name = "สำนักงานปศุสัตว์จังหวัด " },
                 new ProvincialDepartment { Id = 8, DepartmentId = 73, Name = "สำนักงานขนส่งจังหวัด" },
                 new ProvincialDepartment { Id = 9, DepartmentId = 90, Name = "สำนักงานสถิติจังหวัด" },
                 new ProvincialDepartment { Id = 10, DepartmentId = 91, Name = "สถานีอุตุนิยมวิทยา" },
                 new ProvincialDepartment { Id = 11, DepartmentId = 79, Name = "สำนักงานทรัพยากรธรรมชาติและสิ่งแวดล้อมจังหวัด" },
                 new ProvincialDepartment { Id = 12, DepartmentId = 102, Name = "สำนักงานจังหวัด" },
                 new ProvincialDepartment { Id = 13, DepartmentId = 103, Name = "ที่ทำการปกครองจังหวัด" },
                 new ProvincialDepartment { Id = 14, DepartmentId = 104, Name = "สำนักงานพัฒนาชุมชนจังหวัด" },
                 new ProvincialDepartment { Id = 15, DepartmentId = 105, Name = "สำนักงานส่งเสริมการปกครองท้องถิ่นจังหวัด" },
                 new ProvincialDepartment { Id = 16, DepartmentId = 106, Name = "สำนักงานที่ดินจังหวัด" },
                 new ProvincialDepartment { Id = 17, DepartmentId = 107, Name = "สำนักงานโยธาธิการและผังเมืองจังหวัด" },
                 new ProvincialDepartment { Id = 18, DepartmentId = 108, Name = "สำนักงานป้องกันและบรรเทาสาธารณภัยจังหวัด" },
                 new ProvincialDepartment { Id = 19, DepartmentId = 110, Name = "สำนักงานคุมประพฤติจังหวัด" },
                 new ProvincialDepartment { Id = 20, DepartmentId = 111, Name = "เรือนจำ" },
                 new ProvincialDepartment { Id = 21, DepartmentId = 112, Name = "สำนักงานบังคับคดีจังหวัด" },
                 new ProvincialDepartment { Id = 22, DepartmentId = 116, Name = "สำนักงานแรงงานจังหวัด" },
                 new ProvincialDepartment { Id = 23, DepartmentId = 117, Name = "สำนักงานจัดหางานจังหวัด" },
                 new ProvincialDepartment { Id = 24, DepartmentId = 118, Name = "สำนักงานสวัสดิการและคุ้มครองแรงงานจังหวัด" },
                 new ProvincialDepartment { Id = 25, DepartmentId = 119, Name = "สำนักงานประกันสังคมจังหวัด" },
                 new ProvincialDepartment { Id = 26, DepartmentId = 121, Name = "สำนักงานวัฒนธรรมจังหวัด" },
                 new ProvincialDepartment { Id = 27, DepartmentId = 133, Name = "สำนักงานสาธารณสุขจังหวัด" },
                 new ProvincialDepartment { Id = 28, DepartmentId = 142, Name = "สำนักงานอุตสาหกรรมจังหวัด" }
            );
        }
    }
}