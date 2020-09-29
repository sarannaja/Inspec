using InspecWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InspecWeb.Data.Seeders
{
    public class GovernmentinspectionplanSeeder : IEntityTypeConfiguration<Governmentinspectionplan>
    {

        public void Configure(EntityTypeBuilder<Governmentinspectionplan> builder)
        {
            builder.HasData(
                new Governmentinspectionplan
                {
                    Id = 1,
                    Year = "2563",
                    Title = "แผนการตรวจราชการแบบบบูรณาการของผู้ตรวจราชการ ประจำปีงบประมาณ พ.ศ. 2563",
                    File = "inps_plan63.pdf"
                },
                new Governmentinspectionplan
                {
                    Id = 2,
                    Year = "2561",
                    Title = "แผนการตรวจราชการแบบบบูรณาการของผู้ตรวจราชการ ประจำปีงบประมาณ พ.ศ. 2561",
                    File = "plan_61.pdf"
                },
                new Governmentinspectionplan
                {
                    Id = 3,
                    Year = "2560",
                    Title = "แผนการตรวจราชการแบบบูรณาการ ของผู้ตรวจราชการ ประจำปีงบประมาณ พ.ศ. 2560",
                    File = "plan60.pdf"
                },
                new Governmentinspectionplan
                {
                    Id = 4,
                    Year = "2559",
                    Title = "แผนการตรวจราชการแบบบูรณาการประจำปีงบประมาณ พ.ศ. 2559",
                    File = "plan59.pdf"
                },
                new Governmentinspectionplan
                {
                    Id = 5,
                    Year = "2558",
                    Title = "แผนการตรวจราชการแบบบูรณาการประจำปีงบประมาณ พ.ศ. 2558",
                    File = "insp_plan58.zip"
                },
                new Governmentinspectionplan
                {
                    Id = 6,
                    Year = "2557",
                    Title = "แผนการตรวจราชการแบบบูรรณาการ ของผู้ตรวจราชการ ประจำปีงบประมาณ พ.ศ. 2557",
                    File = "plan57.zip"
                },
                new Governmentinspectionplan
                {
                    Id = 7,
                    Year = "2556",
                    Title = "แผนการตรวจราชการแบบบูรรณาการ ของผู้ตรวจราชการ ประจำปีงบประมาณ พ.ศ. 2556",
                    File = "plan56.zip"
                },
                new Governmentinspectionplan
                {
                    Id = 8,
                    Year = "2555",
                    Title = "แผนการตรวจราชการแบบบูรณาการ ของผู้ตรวจราชการ ประจำปีงบประมาณ พ.ศ. 2555",
                    File = "plan55.zip"
                },
                new Governmentinspectionplan
                {
                    Id = 9,
                    Year = "2554",
                    Title = "แผนการตรวจราชการแบบบูรณาการ ประจำปีงบประมาณ พ.ศ.2554",
                    File = "plan54.zip"
                },
                new Governmentinspectionplan
                {
                    Id = 10,
                    Year = "2553",
                    Title = "แผนการตรวจราชการแบบบูรณาการเพื่อมุ่งผลสัมฤทธิ์ตามนโยบายรัฐบาลประจำปีงบประมาณ พ.ศ.2553ของผู้ตรวจราชการ",
                    File = "NULL"
                },
                new Governmentinspectionplan
                {
                    Id = 11,
                    Year = "2552",
                    Title = "แผนการตรวจราชการแบบบูรณาการเพื่อมุ่งผลสัมฤทธิ์ตามนโยบายรัฐบาลประจำปีงบประมาณ พ.ศ. 2552",
                    File = "plan52.pdf"
                },
                new Governmentinspectionplan
                {
                    Id = 12,
                    Year = "2551",
                    Title = "แผนยุทธศาสตร์การตรวจราชการแบบบูรณาการเพื่อมุ่งผลสัมฤทธิ์ตามนโยบายรัฐบาลประจำปีงบประมาณ พ.ศ. 2551",
                    File = "NULL"
                }

            );
        }
    }
}