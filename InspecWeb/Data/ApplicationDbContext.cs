using InspecWeb.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data.Seeders; //เรียกไฟล์ โฟเดอx

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


        //---------Trainings-----------
        public DbSet<Training> Trainings { get; set; }
        public DbSet<TrainingRegister> TrainingRegisters { get; set; }
        public DbSet<TrainingSurvey> TrainingSurveys { get; set; }
        public DbSet<TrainingProgram> TrainingPrograms { get; set; }
        public DbSet<TrainingDocument> TrainingDocuments { get; set; }
        public DbSet<TrainingSurveyAnswer> TrainingSurveyAnswers { get; set; }
        public DbSet<TrainingLecturer> TrainingLecturers { get; set; }
        public DbSet<TrainingRegisterGroup> TrainingRegisterGroups { get; set; }
        public DbSet<TrainingPhase> TrainingPhases { get; set; }
        public DbSet<TrainingCondition> TrainingConditions { get; set; }

        //------------------------------

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
        public DbSet<Informationoperation> Informationoperations { get; set; }
        public DbSet<Nationalstrategy> Nationalstrategies { get; set; }
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

        public DbSet<ExecutiveOrderFile> ExecutiveFiles { get; set; }
        public DbSet<AnswerExecutiveOrderFile> AnswerExecutiveOrderFiles { get; set; }
        public DbSet<RequestOrder> RequestOrders { get; set; }
        public DbSet<RequestOrderFile> RequestOrderFiles { get; set; }
        public DbSet<AnswerRequestOrderFile> AnswerRequestOrderFiles { get; set; }

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<SubjectCentralPolicyProvinceFile> SubjectCentralPolicyProvinceFiles { get; set; }
        public DbSet<ElectronicBookSuggestGroup> ElectronicBookSuggestGroups { get; set; }
        public DbSet<SubjectCentralPolicyProvinceUserGroup> SubjectCentralPolicyProvinceUserGroups { get; set; }
        public DbSet<AnswerSubquestion> AnswerSubquestions { get; set; }
        public DbSet<AnswerSubquestionOutsider> AnswerSubquestionOutsiders { get; set; }
        public DbSet<AnswerSubquestionFile> AnswerSubquestionFiles { get; set; }
        public DbSet<ExportRegistration> ExportRegistrations { get; set; }
        public DbSet<ExportReportHead> ExportReportHeads { get; set; }
        public DbSet<ExportReportBody> ExportReportBodies { get; set; }
        public DbSet<SuggestionSubject> SuggestionSubjects { get; set; }
        public DbSet<AnswerCentralPolicyProvince> AnswerCentralPolicyProvinces { get; set; }
        public DbSet<StatePolicy> StatePolicys { get; set; }
        public DbSet<Documenttemplate> Documenttemplates { get; set; }
        public DbSet<Meetinginformation> Meetinginformations { get; set; }
        public DbSet<Premierorder> Premierorders { get; set; }
        public DbSet<ReportCommander> ReportCommanders { get; set; }
        public DbSet<ElectronicBookProceed> ElectronicBookProceeds { get; set; }
        public DbSet<ElectronicBookAccept> ElectronicBookAccepts { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<CentralPolicyProvinceEvent> CentralPolicyProvinceEvents { get; set; }
        public DbSet<SubjectGroup> SubjectGroups { get; set; }

        public DbSet<ElectronicBookInvite> ElectronicBookInvites { get; set; }

        public DbSet<CentralPolicyEventQuestion> CentralPolicyEventQuestions { get; set; }
        public DbSet<ElectronicBookProvinceApproveFile> ElectronicBookProvinceApproveFiles { get; set; }
        public DbSet<AnswerSubquestionStatus> AnswerSubquestionStatuses { get; set; }

        public DbSet<ExecutiveOrderAnswer> ExecutiveOrderAnswers { get; set; }
        public DbSet<ExecutiveOrderAnswerDetail> ExecutiveOrderAnswerDetails { get; set; }
        public DbSet<RequestOrderAnswer> RequestOrderAnswers { get; set; }
        public DbSet<RequestOrderAnswerDetail> RequestOrderAnswerDetails { get; set; }


        public DbSet<CalendarFile> CalendarFiles { get; set; }
        public DbSet<SubjectEventFile> SubjectEventFiles { get; set; }
        public DbSet<AnswerCentralPolicyProvinceStatus> AnswerCentralPolicyProvinceStatuses { get; set; }
        public DbSet<TrainingProgramFile> TrainingProgramFiles { get; set; }
        public DbSet<TrainingProgramLecturer> TrainingProgramLecturers { get; set; }

        public DbSet<TrainingRegisterFile> TrainingRegisterFiles { get; set; }
        public DbSet<ImportReport> ImportReports { get; set; }
        public DbSet<ImportReportGroup> ImportReportGroups { get; set; }
        public DbSet<ImportReportFile> ImportReportFiles { get; set; }
        public DbSet<ElectronicBookOtherAccept> ElectronicBookOtherAccepts { get; set; }
        public DbSet<TrainingRegisterCondition> TrainingRegisterConditions { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<ProvincesGroup> ProvincesGroups { get; set; }
        public DbSet<UserTokenMobile> UserTokenMobiles { get; set; }
        public DbSet<ElectronicBookProvincialDepartment> ElectronicBookProvincialDepartments { get; set; }
        public DbSet<Approvaldocuments> Approvaldocuments { get; set; } //<!-- แบบขออนุมัติเดินทางไปราชการ -->
        public DbSet<Informationinspection> Informationinspections { get; set; } //<!-- ข้อมูลประกอบการตรวจราชการ -->
        public DbSet<Circularletter> Circularletters { get; set; } //<!-- กฎหมาย ระเบียบ หนังสือเวียนต่าง ๆ -->

        public DbSet<SubjectGroupPeopleQuestion> SubjectGroupPeopleQuestions { get; set; }

        public DbSet<Typeexaminationplan> Typeexaminationplans { get; set; } //<!-- ประเภทแผนการตรวจ -->
        public DbSet<FiscalYearNew> FiscalYearNew { get; set; } //<!-- ปีงบประมาณที่แท้จริง -->
        public DbSet<Side> Sides { get; set; } //<!-- ด้าน -->
        public DbSet<AnswerRecommenDationInspector> AnswerRecommenDationInspectors { get; set; }

        public DbSet<TrainingLogin> TrainingLogins { get; set; }
        

        //method 
        protected override void OnModelCreating(ModelBuilder builder)
        {

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
                //relationship.
                //Cascade(relationship);
            }
            // ส่วนที่สำหรับเชื่อ model
            builder.Entity<UserRegion>()
            .HasKey(m => new { m.UserID, m.RegionId });

            builder.Entity<UserProvince>()
            .HasKey(m => new { m.UserID, m.ProvinceId });

            builder.Entity<ProvincialDepartmentProvince>() //หน่วยงานส่วนภูมิถาค
            .HasKey(m => new { m.ProvincialDepartmentID, m.ProvinceId });

            builder.Entity<CentralDepartmentProvince>() //หน่วยงานราชการส่วนกลางภูมิภาค
           .HasKey(m => new { m.CentralDepartmentID, m.ProvinceId });

            //builder.Entity<CentralPolicyUser>()
            //.HasKey(m => new { m.CentralPolicyId, m.UserId });

            builder.Entity<SubjectDate>()
            .HasKey(m => new { m.SubjectId, m.CentralPolicyDateId });

            //    public ICollection<Subject> Subjects { get; set; }
            //public ICollection<CentralPolicyFile> CentralPolicyFiles { get; set; }
            //public ICollection<CentralPolicyUser> CentralPolicyUser { get; set; }
            //public ICollection<CentralPolicyDate> CentralPolicyDates { get; set; }
            //public ICollection<CentralPolicyProvince> CentralPolicyProvinces { get; set; }

            //public ICollection<CentralPolicyEvent> CentralPolicyEvents { get; set; }

            //CentralPolicy Cascade//
            builder.Entity<CentralPolicyDate>()
            .HasOne(p => p.CentralPolicy)
            .WithMany(b => b.CentralPolicyDates)
            .HasForeignKey(p => p.CentralPolicyId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CentralPolicyFile>()
            .HasOne(p => p.CentralPolicy)
            .WithMany(b => b.CentralPolicyFiles)
            .HasForeignKey(p => p.CentralPolicyId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CentralPolicyUser>()
            .HasOne(p => p.CentralPolicy)
            .WithMany(b => b.CentralPolicyUser)
            .HasForeignKey(p => p.CentralPolicyId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CentralPolicyProvince>()
            .HasOne(p => p.CentralPolicy)
            .WithMany(b => b.CentralPolicyProvinces)
            .HasForeignKey(p => p.CentralPolicyId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CentralPolicyEvent>()
            .HasOne(p => p.CentralPolicy)
            .WithMany(b => b.CentralPolicyEvents)
            .HasForeignKey(p => p.CentralPolicyId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Subject>()
            .HasOne(p => p.CentralPolicy)
            .WithMany(b => b.Subjects)
            .HasForeignKey(p => p.CentralPolicyId)
            .OnDelete(DeleteBehavior.Cascade);

            //builder.Entity<SubjectGroup>()
            //.HasOne(p => p.CentralPolicy)
            //.WithMany(b => b.SubjectGroups)
            //.HasForeignKey(p => p.CentralPolicyId)
            //.OnDelete(DeleteBehavior.Cascade);

            //builder.Entity<SubjectGroup>()
            //.HasOne(p => p.Province)
            //.WithMany(b => b.SubjectGroups)
            //.HasForeignKey(p => p.ProvinceId)
            //.OnDelete(DeleteBehavior.Cascade);

            //builder.Entity<CentralPolicyEvent>()
            //.HasOne(p => p.CentralPolicy)
            //.WithMany(b => b.CentralPolicyEvents)
            //.HasForeignKey(p => p.CentralPolicyId)
            //.OnDelete(DeleteBehavior.Cascade);

            //CentralPolicyProvince Cascade//
            builder.Entity<SubjectCentralPolicyProvince>()
            .HasOne(p => p.CentralPolicyProvince)
            .WithMany(b => b.SubjectCentralPolicyProvinces)
            .HasForeignKey(p => p.CentralPolicyProvinceId)
            .OnDelete(DeleteBehavior.Cascade);

            //SubjectCentralPolicyProvince Cascade//
            builder.Entity<SubjectDateCentralPolicyProvince>()
            .HasOne(p => p.SubjectCentralPolicyProvince)
            .WithMany(b => b.SubjectDateCentralPolicyProvinces)
            .HasForeignKey(p => p.SubjectCentralPolicyProvinceId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SubquestionCentralPolicyProvince>()
            .HasOne(p => p.SubjectCentralPolicyProvince)
            .WithMany(b => b.SubquestionCentralPolicyProvinces)
            .HasForeignKey(p => p.SubjectCentralPolicyProvinceId)
            .OnDelete(DeleteBehavior.Cascade);

            //builder.Entity<ElectronicBookSuggestGroup>()
            //.HasOne(p => p.SubjectCentralPolicyProvince)
            //.WithMany(b => b.ElectronicBookSuggestGroups)
            //.HasForeignKey(p => p.SubjectCentralPolicyProvinceId)
            //.OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SubjectCentralPolicyProvinceFile>()
            .HasOne(p => p.SubjectCentralPolicyProvince)
            .WithMany(b => b.SubjectCentralPolicyProvinceFiles)
            .HasForeignKey(p => p.SubjectCentralPolicyProvinceId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AnswerSubquestionFile>()
            .HasOne(p => p.SubjectCentralPolicyProvince)
            .WithMany(b => b.AnswerSubquestionFiles)
            .HasForeignKey(p => p.SubjectCentralPolicyProvinceId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AnswerSubquestionStatus>()
            .HasOne(p => p.SubjectCentralPolicyProvince)
            .WithMany(b => b.AnswerSubquestionStatuses)
            .HasForeignKey(p => p.SubjectCentralPolicyProvinceId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SuggestionSubject>()
            .HasOne(p => p.SubjectCentralPolicyProvince)
            .WithMany(b => b.SuggestionSubjects)
            .HasForeignKey(p => p.SubjectCentralPolicyProvinceId)
            .OnDelete(DeleteBehavior.Cascade);

            //SubquestionCentralPolicyProvince Cascade//
            builder.Entity<SubjectCentralPolicyProvinceGroup>()
            .HasOne(p => p.SubquestionCentralPolicyProvince)
            .WithMany(b => b.SubjectCentralPolicyProvinceGroups)
            .HasForeignKey(p => p.SubquestionCentralPolicyProvinceId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SubquestionChoiceCentralPolicyProvince>()
            .HasOne(p => p.SubquestionCentralPolicyProvince)
            .WithMany(b => b.SubquestionChoiceCentralPolicyProvinces)
            .HasForeignKey(p => p.SubquestionCentralPolicyProvinceId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AnswerSubquestion>()
            .HasOne(p => p.SubquestionCentralPolicyProvince)
            .WithMany(b => b.AnswerSubquestions)
            .HasForeignKey(p => p.SubquestionCentralPolicyProvinceId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AnswerSubquestionOutsider>()
            .HasOne(p => p.SubquestionCentralPolicyProvince)
            .WithMany(b => b.AnswerSubquestionOutsiders)
            .HasForeignKey(p => p.SubquestionCentralPolicyProvinceId)
            .OnDelete(DeleteBehavior.Cascade);

            //ElectronicBook Cascade//
            builder.Entity<ElectronicBookFile>()
            .HasOne(p => p.ElectronicBook)
            .WithMany(b => b.ElectronicBookFiles)
            .HasForeignKey(p => p.ElectronicBookId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ElectronicBookSuggestGroup>()
            .HasOne(p => p.ElectronicBook)
            .WithMany(b => b.ElectronicBookSuggestGroups)
            .HasForeignKey(p => p.ElectronicBookId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ElectronicBookGroup>()
            .HasOne(p => p.ElectronicBook)
            .WithMany(b => b.ElectronicBookGroups)
            .HasForeignKey(p => p.ElectronicBookId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ElectronicBookAccept>()
            .HasOne(p => p.ElectronicBook)
            .WithMany(b => b.ElectronicBookAccepts)
            .HasForeignKey(p => p.ElectronicBookId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ElectronicBookInvite>()
           .HasOne(p => p.ElectronicBook)
           .WithMany(b => b.ElectronicBookInvites)
           .HasForeignKey(p => p.ElectronicBookId)
           .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ElectronicBookOtherAccept>()
           .HasOne(p => p.ElectronicBookProvincialDepartment)
           .WithMany(b => b.ElectronicBookOtherAccepts)
           .HasForeignKey(p => p.ElectronicBookProvincialDepartmentId)
           .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ElectronicBookProvincialDepartment>()
           .HasOne(p => p.ElectronicBook)
           .WithMany(b => b.ElectronicBookProvincialDepartments)
           .HasForeignKey(p => p.ElectronicBookId)
           .OnDelete(DeleteBehavior.Cascade);

            //InspectionPlanEvent Cascade//
            builder.Entity<CentralPolicyEvent>()
            .HasOne(p => p.InspectionPlanEvent)
            .WithMany(b => b.CentralPolicyEvents)
            .HasForeignKey(p => p.InspectionPlanEventId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CentralPolicyUser>()
            .HasOne(p => p.InspectionPlanEvent)
            .WithMany(b => b.CentralPolicyUsers)
            .HasForeignKey(p => p.InspectionPlanEventId)
            .OnDelete(DeleteBehavior.Cascade);

            //.WithOne(b => b.CentralPolicy)
            //.HasMany();
            //builder.Entity<SubquestionGroup>()
            //.HasKey(m => new { m.SubquestionId, m.ProvincialDepartmentId });

            //builder.Entity<SubjectDateCentralPolicyProvince>()
            //.HasKey(m => new { m.SubjectCentralPolicyProvinceId, m.CentralPolicyDateId });

            //ImportReport Cascade//
            builder.Entity<ImportReportGroup>()
            .HasOne(p => p.ImportReport)
            .WithMany(b => b.ImportReportGroups)
            .HasForeignKey(p => p.ImportReportId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ReportCommander>()
            .HasOne(p => p.ImportReport)
            .WithMany(b => b.ReportCommanders)
            .HasForeignKey(p => p.ImportReportId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ImportReportFile>()
            .HasOne(p => p.ImportReport)
            .WithMany(b => b.ImportReportFiles)
            .HasForeignKey(p => p.ImportReportId)
            .OnDelete(DeleteBehavior.Cascade);

            //subjectgroups cascade//

            builder.Entity<SubjectEventFile>()
            .HasOne(p => p.SubjectGroup)
            .WithMany(b => b.SubjectEventFiles)
            .HasForeignKey(p => p.SubjectGroupId)
            .OnDelete(DeleteBehavior.Cascade);

            //builder.Entity<AnswerSubquestionFile>()
            //.HasOne(p => p.SubjectGroup)
            //.WithMany(b => b.AnswerSubquestionFiles)
            //.HasForeignKey(p => p.SubjectGroupId)
            //.OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SubjectGroup>()
            .HasMany(p => p.SubjectCentralPolicyProvinces)
            .WithOne(b => b.SubjectGroup)
            .HasForeignKey(p => p.SubjectGroupId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SubjectGroupPeopleQuestion>()
            .HasOne(p => p.SubjectGroup)
            .WithMany(b => b.SubjectGroupPeopleQuestions)
            .HasForeignKey(p => p.SubjectGroupId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SubjectGroupPeopleQuestion>()
            .HasOne(p => p.CentralPolicyEvent)
            .WithMany(b => b.SubjectGroupPeopleQuestions)
            .HasForeignKey(p => p.CentralPolicyEventId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AnswerRecommenDationInspector>()
            .HasOne(p => p.SubjectGroup)
            .WithMany(b => b.AnswerRecommenDationInspectors)
            .HasForeignKey(p => p.SubjectGroupId)
            .OnDelete(DeleteBehavior.Cascade);

            //builder.Entity<SubjectCentralPolicyProvince>()
            //.HasOne(p => p.SubjectGroup)
            //.WithMany(b => b.SubjectCentralPolicyProvinces)
            //.HasForeignKey(p => p.SubjectGroupId)
            //.OnDelete(DeleteBehavior.Cascade);



            //builder.Entity<SubjectGroupPeopleQuestion>()
            //.HasOne(p => p.SubjectGroup)
            //.WithMany(b => b.SubjectGroupPeopleQuestions)
            //.HasForeignKey(p => p.SubjectGroupId)
            //.OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CentralPolicyEventQuestion>()
            .HasOne(p => p.CentralPolicyEvent)
            .WithMany(b => b.CentralPolicyEventQuestions)
            .HasForeignKey(p => p.CentralPolicyEventId)
            .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
            //seed data
            // builder.ApplyConfiguration(new SectorSeeder());
            // builder.ApplyConfiguration(new ProvincesGroupSeeder());
            // builder.ApplyConfiguration(new MinistrySeeder());
            // builder.ApplyConfiguration(new DepartmentSeeder());
            // builder.ApplyConfiguration(new ProvinceSeeder());
            // builder.ApplyConfiguration(new RegionSeeder());
            // builder.ApplyConfiguration(new FiscalYearSeeder());
            // builder.ApplyConfiguration(new GovernmentinspectionplanSeeder());
            // builder.ApplyConfiguration(new InspectionOrderSeeder());
            // builder.ApplyConfiguration(new InstructionOrderSeeder());
            // builder.ApplyConfiguration(new DistrictSeeder());
            // builder.ApplyConfiguration(new SubdistrictSeeder());
            // builder.ApplyConfiguration(new RelationSeeder());
            // builder.ApplyConfiguration(new ProvincialDepartmentSeeder());//หน่วยงานส่วนภูมิถาค
            // builder.ApplyConfiguration(new ProvincialDepartmentProvinceSeeder());//หน่วยงานส่วนภูมิถาค เชื่อมจังหวัด
            // builder.ApplyConfiguration(new CabineSeeder());//คณะรัฐมนตรี
            // builder.ApplyConfiguration(new VillageSeeder());//หมู่บ้าน
            // builder.ApplyConfiguration(new FiscalYearNewSeeder());//ปีที่แท้
            // builder.ApplyConfiguration(new TypeexaminationplanSeeder());//ประเภทแผนการตรวจ
            // builder.ApplyConfiguration(new SideSeeder());//ประเภทด้านภาคประชาชน

        }
    }
}
