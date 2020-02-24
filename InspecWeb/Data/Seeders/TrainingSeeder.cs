using InspecWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InspecWeb.Data.Seeders
{
    public class TrainingSeeder : IEntityTypeConfiguration<Training>
    {

        public void Configure(EntityTypeBuilder<Training> builder)
        {
            builder.HasData(
                new Training {}
            );
        }
    }
}