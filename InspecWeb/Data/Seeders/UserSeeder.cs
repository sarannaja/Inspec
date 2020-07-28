using InspecWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InspecWeb.Data.Seeders
{   
    public class UserSeeder : IEntityTypeConfiguration<ApplicationUser>
    {    
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
                 
        }
    }
}