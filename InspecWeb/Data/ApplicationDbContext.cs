using InspecWeb.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data.Seeders; //เรียกไฟล์ โฟเดอ

namespace InspecWeb.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        //ถ้าเพิ่มโมเดลใหม่
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Ministry> Ministries { get; set; }
        public DbSet<Department> Departments { get; set; }

        //method 
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // seed data
            builder.ApplyConfiguration(new MinistrySeeder());
            builder.ApplyConfiguration(new DepartmentSeeder());
        }
    }
}
