using InspecWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InspecWeb.Data.Seeders
{
    public class CentralDepartmentProvinceSeeder : IEntityTypeConfiguration<CentralDepartmentProvince>
    {
        //https://stackoverflow.com/questions/29841503/json-serialization-deserialization-in-asp-net-core
        public void Configure(EntityTypeBuilder<CentralDepartmentProvince> builder)
        {
            builder.HasData(
                  new CentralDepartmentProvince { Id = 1, CentralDepartmentID = 1, ProvinceId = 2 }
            );
        }
    }
}