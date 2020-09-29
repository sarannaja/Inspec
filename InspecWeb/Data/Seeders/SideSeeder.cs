using InspecWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InspecWeb.Data.Seeders
{
    public class SideSeeder : IEntityTypeConfiguration<Side>
    {

        public void Configure(EntityTypeBuilder<Side> builder)
        {
            builder.HasData(
            new Ministry { Id = 1, Name = "วิชาการ", ShortnameEN = "aca" },
            new Ministry { Id = 2, Name = "สังคม", ShortnameEN = "soc" },
            new Ministry { Id = 3, Name = "สิ่งแวดล้อม", ShortnameEN = "env" },
            new Ministry { Id = 4, Name = "เศรษฐกิจ", ShortnameEN = "eco" }
            );
        }
    }
}