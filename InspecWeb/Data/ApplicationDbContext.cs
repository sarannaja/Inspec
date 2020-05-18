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
        public DbSet<Region> Regions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Subdistrict> Subdistricts { get; set; }
        public DbSet<Village> Villages { get; set; }
        public DbSet<CentralPolicy> CentralPolicies { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<CentralPolicyFile> CentralPolicyFiles { get; set; }
        public DbSet<FiscalYear> FiscalYears { get; set; }
        public DbSet<FiscalYearRelation> FiscalYearRelations { get; set; }
        public DbSet<Governmentinspectionplan> Governmentinspectionplans { get; set; }
        public DbSet<InspectionOrder> InspectionOrders { get; set; }
        public DbSet<InstructionOrder> InstructionOrders { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Subquestion> Subquestions { get; set; }
        public DbSet<InspectionPlanEvent> InspectionPlanEvents { get; set; }
        public DbSet<Cabine> Cabines { get; set; }
        public DbSet<Ministermonitoring> Ministermonitorings { get; set; }
        public DbSet<MinistermonitoringRegion> MinistermonitoringRegions { get; set; }
        public DbSet<Inspector> Inspectors { get; set; }
        public DbSet<InspectorRegion> InspectorRegions { get; set; }
        public DbSet<CentralPolicyEvent> CentralPolicyEvents { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; } //เพิ่มคอลัม user
        public DbSet<UserRegion> UserRegions { get; set; } //เชื่อม user กับ เขตตรวจ
        public DbSet<UserProvince> UserProvinces { get; set; } //เชื่อม user กับ จังหวัด
        public DbSet<ExecutiveOrder> ExecutiveOrders { get; set; }
        public DbSet<CentralPolicyProvince> CentralPolicyProvinces { get; set; }
        //public DbSet<InspectionPlanEventProvince> InspectionPlanEventProvinces { get; set; } 
        public DbSet<ProvincialDepartment> ProvincialDepartment { get; set; } //หน่วยงานส่วนภูมิถาค
        public DbSet<ProvincialDepartmentProvince> ProvincialDepartmentProvince { get; set; } //เชื่อม หน่วยงานส่วนภูมิถาค กับ จังหวัด
        public DbSet<CentralDepartment> CentralDepartment { get; set; } //หน่วยงานราชการส่วนกลางภูมิภาค 
        public DbSet<CentralDepartmentProvince> CentralDepartmentProvince { get; set; } //เชื่อม หน่วยงานราชการส่วนกลางภูมิภาค กับ จังหวัด

        //public DbSet<InspectionPlanEventProvince> InspectionPlanEventProvinces { get; set; }
       
        public DbSet<CentralPolicyUser> CentralPolicyUsers { get; set; }
        public DbSet<CentralPolicyDate> CentralPolicyDates { get; set; }
        public DbSet<SubjectDate> SubjectDates { get; set; }
        public DbSet<SubquestionChoice> SubquestionChoices { get; set; }

        public DbSet<SubjectCentralPolicyProvince> SubjectCentralPolicyProvinces { get; set; }
        public DbSet<SubquestionCentralPolicyProvince> SubquestionCentralPolicyProvinces { get; set; }
        public DbSet<SubquestionChoiceCentralPolicyProvince> SubquestionChoiceCentralPolicyProvinces { get; set; }
        public DbSet<SubjectDateCentralPolicyProvince> SubjectDateCentralPolicyProvinces { get; set; }
        public DbSet<CentralPolicyDateProvince> CentralPolicyDateProvinces { get; set; }

        public DbSet<CentralPolicyGroup> CentralPolicyGroups { get; set; }
        public DbSet<CentralPolicyUserFile> CentralPolicyUserFiles { get; set; }

        public DbSet<ElectronicBook> ElectronicBooks { get; set; }
        public DbSet<ElectronicBookGroup> ElectronicBookGroups { get; set; }
        public DbSet<ElectronicBookFile> ElectronicBookFiles { get; set; }

        public DbSet<SubjectCentralPolicyProvinceGroup> SubjectCentralPolicyProvinceGroups { get; set; }
        //public DbSet<SubquestionGroup> SubquestionGroups { get; set; }
        public DbSet<SubjectCentralPolicyProvinceFile> SubjectCentralPolicyProvinceFiles { get; set; }
        public DbSet<ElectronicBookSuggestGroup> ElectronicBookSuggestGroups { get; set; }
        
        //method 
        protected override void OnModelCreating(ModelBuilder builder)
        {
           // ส่วนที่สำหรับเชื่อ model
            builder.Entity<UserRegion>()
            .HasKey(m => new { m.UserID, m.RegionId });

            builder.Entity<UserProvince>()
            .HasKey(m => new { m.UserID, m.ProvinceId });

            builder.Entity<ProvincialDepartmentProvince>() //หน่วยงานส่วนภูมิถาค
            .HasKey(m => new { m.ProvincialDepartmentID, m.ProvinceId });

            builder.Entity<CentralDepartmentProvince>() //หน่วยงานราชการส่วนกลางภูมิภาค
           .HasKey(m => new { m.CentralDepartmentID, m.ProvinceId });

            builder.Entity<CentralPolicyUser>()
            .HasKey(m => new { m.CentralPolicyId, m.UserId });

            builder.Entity<SubjectDate>()
            .HasKey(m => new { m.SubjectId, m.CentralPolicyDateId });

            //builder.Entity<SubquestionGroup>()
            //.HasKey(m => new { m.SubquestionId, m.ProvincialDepartmentId });

            //builder.Entity<SubjectDateCentralPolicyProvince>()
            //.HasKey(m => new { m.SubjectCentralPolicyProvinceId, m.CentralPolicyDateId });

            base.OnModelCreating(builder);

            // seed data
            builder.ApplyConfiguration(new MinistrySeeder());
            builder.ApplyConfiguration(new DepartmentSeeder());
            builder.ApplyConfiguration(new ProvinceSeeder());
            builder.ApplyConfiguration(new RegionSeeder());
            builder.ApplyConfiguration(new FiscalYearSeeder());
            builder.ApplyConfiguration(new GovernmentinspectionplanSeeder());
            builder.ApplyConfiguration(new InspectionOrderSeeder());
            builder.ApplyConfiguration(new InstructionOrderSeeder());
            builder.ApplyConfiguration(new DistrictSeeder());
            builder.ApplyConfiguration(new SubdistrictSeeder());
            builder.ApplyConfiguration(new RelationSeeder());
            builder.ApplyConfiguration(new ProvincialDepartmentSeeder());//หน่วยงานส่วนภูมิถาค
            builder.ApplyConfiguration(new ProvincialDepartmentProvinceSeeder());//หน่วยงานส่วนภูมิถาค เชื่อมจังหวัด
            builder.ApplyConfiguration(new CabineSeeder());//คณะรัฐมนตรี
            builder.ApplyConfiguration(new VillageSeeder());//หมู่บ้าน
        }

     
    }
}
