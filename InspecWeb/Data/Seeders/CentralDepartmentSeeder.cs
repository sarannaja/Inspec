using InspecWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InspecWeb.Data.Seeders
{
    public class CentralDepartmentSeeder : IEntityTypeConfiguration<CentralDepartment>
    {
        //https://stackoverflow.com/questions/29841503/json-serialization-deserialization-in-asp-net-core
        public void Configure(EntityTypeBuilder<CentralDepartment> builder)
        {
            builder.HasData(
                new CentralDepartment { Id = 1, DepartmentId = 1, Name = "สำนักงานประชาสัมพันธ์" }
            );
        }
    }
}