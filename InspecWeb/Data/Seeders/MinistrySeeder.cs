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
                new Ministry
                {
                    Id = 1,
                    Name = "มหาดไทย",
                },
                new Ministry
                {
                    Id = 2,
                    Name = "สาธาระณะสุข",

                }
            );
        }
    }
}