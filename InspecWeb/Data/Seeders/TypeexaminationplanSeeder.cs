using InspecWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InspecWeb.Data.Seeders
{
    public class TypeexaminationplanSeeder : IEntityTypeConfiguration<Typeexaminationplan>
    {
        public void Configure(EntityTypeBuilder<Typeexaminationplan> builder)
        {
            builder.HasData(
                new Typeexaminationplan { Id = 1, Name = "ตรวจราชการแบบบูรณาการ" },
                new Typeexaminationplan { Id = 2, Name = "การตรวจราชการตามภารกิจปกติของหน่วยงาน" },
                new Typeexaminationplan { Id = 3, Name = "การตรวจราชการกรณีพิเศษ" }

            );
        }
    }
}