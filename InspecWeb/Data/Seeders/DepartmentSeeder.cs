using InspecWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InspecWeb.Data.Seeders
{
    public class DepartmentSeeder : IEntityTypeConfiguration<Department>
    {
        //https://stackoverflow.com/questions/29841503/json-serialization-deserialization-in-asp-net-core
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasData(
                new Department
                {
                    Id = 1,
                    MinistryId = 1,
                    Name = "ทหารบก"
                },
                new Department
                {
                    Id = 2,
                    MinistryId = 2,
                    Name = "อย."
                }
            );
        }
    }
}