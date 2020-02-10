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