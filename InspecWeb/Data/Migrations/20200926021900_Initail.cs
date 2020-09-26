using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class Initail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Approvaldocuments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    Filename = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approvaldocuments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cabines",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prefix = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Position = table.Column<string>(nullable: false),
                    Image = table.Column<string>(nullable: false),
                    Detail = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cabines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CentralPolicyDateProvinces",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentralPolicyDateProvinces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CentralPolicyGroups",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentralPolicyGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Circularletter",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    Filename = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Circularletter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceCodes",
                columns: table => new
                {
                    UserCode = table.Column<string>(maxLength: 200, nullable: false),
                    DeviceCode = table.Column<string>(maxLength: 200, nullable: false),
                    SubjectId = table.Column<string>(maxLength: 200, nullable: true),
                    ClientId = table.Column<string>(maxLength: 200, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Expiration = table.Column<DateTime>(nullable: false),
                    Data = table.Column<string>(maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCodes", x => x.UserCode);
                });

            migrationBuilder.CreateTable(
                name: "Documenttemplates",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    File = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documenttemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FiscalYear",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<string>(nullable: false),
                    Orderdate = table.Column<DateTime>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscalYear", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FiscalYearNew",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscalYearNew", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Governmentinspectionplans",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    File = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Governmentinspectionplans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Informationinspection",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    Filename = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Informationinspection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Informationoperations",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Detail = table.Column<string>(nullable: false),
                    Tel = table.Column<string>(nullable: false),
                    Province = table.Column<string>(nullable: false),
                    District = table.Column<string>(nullable: false),
                    File = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Informationoperations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InspectionOrders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<string>(nullable: false),
                    Order = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    CreateBy = table.Column<string>(nullable: false),
                    File = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectionOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inspectors",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Phonenumber = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inspectors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InstructionOrders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<string>(nullable: false),
                    Order = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    CreateBy = table.Column<string>(nullable: false),
                    Detail = table.Column<string>(nullable: false),
                    File = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructionOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meetinginformations",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    File = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetinginformations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ministermonitorings",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Position = table.Column<string>(nullable: false),
                    Image = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ministermonitorings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ministries",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    ShortnameTH = table.Column<string>(nullable: true),
                    NameEN = table.Column<string>(nullable: false),
                    ShortnameEN = table.Column<string>(nullable: true),
                    Num = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ministries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nationalstrategies",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    File = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationalstrategies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersistedGrants",
                columns: table => new
                {
                    Key = table.Column<string>(maxLength: 200, nullable: false),
                    Type = table.Column<string>(maxLength: 50, nullable: false),
                    SubjectId = table.Column<string>(maxLength: 200, nullable: true),
                    ClientId = table.Column<string>(maxLength: 200, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Expiration = table.Column<DateTime>(nullable: true),
                    Data = table.Column<string>(maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistedGrants", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Premierorders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    File = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Premierorders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProvincesGroup",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvincesGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sector",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sector", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Side",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    ShortnameTH = table.Column<string>(nullable: true),
                    NameEN = table.Column<string>(nullable: true),
                    ShortnameEN = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Side", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatePolicys",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GangId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    No = table.Column<string>(nullable: false),
                    RoundNo = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    File = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatePolicys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingLecturerJoinSurveys",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingSurveyTopicId = table.Column<long>(nullable: false),
                    LecturerId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingLecturerJoinSurveys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingLecturers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LecturerName = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Education = table.Column<string>(nullable: true),
                    WorkHistory = table.Column<string>(nullable: true),
                    Experience = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingLecturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingLogin",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: false),
                    IDCode = table.Column<string>(nullable: true),
                    TrainingPhaseId = table.Column<long>(nullable: false),
                    RegisterDate = table.Column<DateTime>(nullable: true),
                    TrainingProgramLoginId = table.Column<long>(nullable: false),
                    DateType = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingLogin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trainings",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Detail = table.Column<string>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    Generation = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    CourseCode = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    RegisStartDate = table.Column<DateTime>(nullable: false),
                    RegisEndDate = table.Column<DateTime>(nullable: false),
                    Image = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingSurveyTopics",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    SurveyType = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingSurveyTopics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Typeexaminationplans",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Typeexaminationplans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CentralPolicyUserFiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CentralPolicyGroupId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentralPolicyUserFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CentralPolicyUserFiles_CentralPolicyGroups_CentralPolicyGroupId",
                        column: x => x.CentralPolicyGroupId,
                        principalTable: "CentralPolicyGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SetinspectionareaFiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FiscalYearId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetinspectionareaFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SetinspectionareaFiles_FiscalYear_FiscalYearId",
                        column: x => x.FiscalYearId,
                        principalTable: "FiscalYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinistryId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    ShortnameTH = table.Column<string>(nullable: true),
                    NameEN = table.Column<string>(nullable: false),
                    ShortnameEN = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Ministries_MinistryId",
                        column: x => x.MinistryId,
                        principalTable: "Ministries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InspectorRegions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InspectorId = table.Column<long>(nullable: false),
                    RegionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectorRegions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InspectorRegions_Inspectors_InspectorId",
                        column: x => x.InspectorId,
                        principalTable: "Inspectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InspectorRegions_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MinistermonitoringRegions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinistermonitoringId = table.Column<long>(nullable: false),
                    RegionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinistermonitoringRegions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MinistermonitoringRegions_Ministermonitorings_MinistermonitoringId",
                        column: x => x.MinistermonitoringId,
                        principalTable: "Ministermonitorings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MinistermonitoringRegions_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectorId = table.Column<long>(nullable: false),
                    ProvincesGroupId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    NameEN = table.Column<string>(nullable: false),
                    ShortnameEN = table.Column<string>(nullable: true),
                    ShortnameTH = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    RegionId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Provinces_ProvincesGroup_ProvincesGroupId",
                        column: x => x.ProvincesGroupId,
                        principalTable: "ProvincesGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Provinces_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Provinces_Sector_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sector",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainingConditions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    StartYear = table.Column<int>(nullable: false),
                    EndYear = table.Column<int>(nullable: false),
                    ConditionType = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingConditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingConditions_Trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Trainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainingDocuments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Detail = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingDocuments_Trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Trainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainingPhases",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingId = table.Column<long>(nullable: false),
                    PhaseNo = table.Column<long>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Detail = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    Group = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingPhases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingPhases_Trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Trainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainingSurveys",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingSurveyTopicId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    SurveyType = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    TrainingId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingSurveys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingSurveys_Trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Trainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrainingSurveys_TrainingSurveyTopics_TrainingSurveyTopicId",
                        column: x => x.TrainingSurveyTopicId,
                        principalTable: "TrainingSurveyTopics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CentralDepartments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentralDepartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CentralDepartments_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProvincialDepartments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvincialDepartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProvincialDepartments_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvinceId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Districts_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExportRegistration",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvinceId = table.Column<long>(nullable: false),
                    Date = table.Column<DateTime>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    SendDate = table.Column<DateTime>(nullable: true),
                    ExcutiveOerder = table.Column<string>(nullable: true),
                    ExcutiveOerderDate = table.Column<DateTime>(nullable: true),
                    File = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportRegistration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExportRegistration_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExportReportHead",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvinceId = table.Column<long>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Year = table.Column<string>(nullable: true),
                    Ministry = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportReportHead", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExportReportHead_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FiscalYearRelations",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FiscalYearId = table.Column<long>(nullable: false),
                    RegionId = table.Column<long>(nullable: false),
                    ProvinceId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscalYearRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FiscalYearRelations_FiscalYear_FiscalYearId",
                        column: x => x.FiscalYearId,
                        principalTable: "FiscalYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FiscalYearRelations_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FiscalYearRelations_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainingPrograms",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingPhaseId = table.Column<long>(nullable: false),
                    ProgramDate = table.Column<DateTime>(nullable: false),
                    MinuteStartDate = table.Column<string>(nullable: true),
                    MinuteEndDate = table.Column<string>(nullable: true),
                    ProgramType = table.Column<string>(nullable: true),
                    ProgramTopic = table.Column<string>(nullable: true),
                    ProgramDetail = table.Column<string>(nullable: true),
                    ProgramLocation = table.Column<string>(nullable: true),
                    ProgramToDress = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingPrograms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingPrograms_TrainingPhases_TrainingPhaseId",
                        column: x => x.TrainingPhaseId,
                        principalTable: "TrainingPhases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainingSurveyAnswers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingSurveyId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Posoition = table.Column<string>(nullable: true),
                    Score = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingSurveyAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingSurveyAnswers_TrainingSurveys_TrainingSurveyId",
                        column: x => x.TrainingSurveyId,
                        principalTable: "TrainingSurveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CentralDepartmentProvince",
                columns: table => new
                {
                    CentralDepartmentID = table.Column<long>(nullable: false),
                    ProvinceId = table.Column<long>(nullable: false),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentralDepartmentProvince", x => new { x.CentralDepartmentID, x.ProvinceId });
                    table.ForeignKey(
                        name: "FK_CentralDepartmentProvince_CentralDepartments_CentralDepartmentID",
                        column: x => x.CentralDepartmentID,
                        principalTable: "CentralDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CentralDepartmentProvince_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProvincialDepartmentProvince",
                columns: table => new
                {
                    ProvincialDepartmentID = table.Column<long>(nullable: false),
                    ProvinceId = table.Column<long>(nullable: false),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvincialDepartmentProvince", x => new { x.ProvincialDepartmentID, x.ProvinceId });
                    table.ForeignKey(
                        name: "FK_ProvincialDepartmentProvince_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProvincialDepartmentProvince_ProvincialDepartments_ProvincialDepartmentID",
                        column: x => x.ProvincialDepartmentID,
                        principalTable: "ProvincialDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainingRegisters",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingId = table.Column<long>(nullable: false),
                    UserType = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    ProvincialDepartmentId = table.Column<long>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Position = table.Column<string>(nullable: true),
                    Department = table.Column<string>(nullable: true),
                    CardId = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Status = table.Column<long>(nullable: false),
                    Group1 = table.Column<long>(nullable: false),
                    Group2 = table.Column<long>(nullable: false),
                    Group3 = table.Column<long>(nullable: false),
                    Group4 = table.Column<long>(nullable: false),
                    Group5 = table.Column<long>(nullable: false),
                    Group6 = table.Column<long>(nullable: false),
                    Group7 = table.Column<long>(nullable: false),
                    Group8 = table.Column<long>(nullable: false),
                    Group9 = table.Column<long>(nullable: false),
                    Group10 = table.Column<long>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Nickname = table.Column<string>(nullable: true),
                    RetiredDate = table.Column<DateTime>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: true),
                    OfficeAddress = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    CollaboratorName = table.Column<string>(nullable: true),
                    CollaboratorPhone = table.Column<string>(nullable: true),
                    CollaboratorPhoneOffice = table.Column<string>(nullable: true),
                    CollaboratorEmail = table.Column<string>(nullable: true),
                    IDCode = table.Column<string>(nullable: true),
                    NoIDCode = table.Column<string>(nullable: true),
                    IDCodeCreatedAt = table.Column<DateTime>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingRegisters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingRegisters_ProvincialDepartments_ProvincialDepartmentId",
                        column: x => x.ProvincialDepartmentId,
                        principalTable: "ProvincialDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrainingRegisters_Trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Trainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subdistricts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistrictId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subdistricts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subdistricts_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExportReportBody",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExportReportHeadId = table.Column<long>(nullable: false),
                    Subject = table.Column<string>(nullable: true),
                    Problem = table.Column<string>(nullable: true),
                    Department = table.Column<string>(nullable: true),
                    Suggestion = table.Column<string>(nullable: true),
                    Report = table.Column<string>(nullable: true),
                    File = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportReportBody", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExportReportBody_ExportReportHead_ExportReportHeadId",
                        column: x => x.ExportReportHeadId,
                        principalTable: "ExportReportHead",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainingProgramFiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingProgramId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingProgramFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingProgramFiles_TrainingPrograms_TrainingProgramId",
                        column: x => x.TrainingProgramId,
                        principalTable: "TrainingPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainingProgramLecturers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingProgramId = table.Column<long>(nullable: false),
                    TrainingLecturerId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingProgramLecturers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingProgramLecturers_TrainingLecturers_TrainingLecturerId",
                        column: x => x.TrainingLecturerId,
                        principalTable: "TrainingLecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrainingProgramLecturers_TrainingPrograms_TrainingProgramId",
                        column: x => x.TrainingProgramId,
                        principalTable: "TrainingPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainingProgramLoginQRCodes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramDate = table.Column<DateTime>(nullable: true),
                    TrainingProgramId = table.Column<long>(nullable: false),
                    Morning = table.Column<long>(nullable: false),
                    Afternoon = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingProgramLoginQRCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingProgramLoginQRCodes_TrainingPrograms_TrainingProgramId",
                        column: x => x.TrainingProgramId,
                        principalTable: "TrainingPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainingRegisterConditions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegisterId = table.Column<long>(nullable: false),
                    ConditionId = table.Column<long>(nullable: false),
                    Status = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingRegisterConditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingRegisterConditions_TrainingConditions_ConditionId",
                        column: x => x.ConditionId,
                        principalTable: "TrainingConditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrainingRegisterConditions_TrainingRegisters_RegisterId",
                        column: x => x.RegisterId,
                        principalTable: "TrainingRegisters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainingRegisterFiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegisterId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingRegisterFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingRegisterFiles_TrainingRegisters_RegisterId",
                        column: x => x.RegisterId,
                        principalTable: "TrainingRegisters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainingRegisterGroups",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegisterId = table.Column<long>(nullable: false),
                    TrainingPhaseId = table.Column<long>(nullable: false),
                    Group1 = table.Column<long>(nullable: false),
                    Group2 = table.Column<long>(nullable: false),
                    Group3 = table.Column<long>(nullable: false),
                    Group4 = table.Column<long>(nullable: false),
                    Group5 = table.Column<long>(nullable: false),
                    Group6 = table.Column<long>(nullable: false),
                    Group7 = table.Column<long>(nullable: false),
                    Group8 = table.Column<long>(nullable: false),
                    Group9 = table.Column<long>(nullable: false),
                    Group10 = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingRegisterGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingRegisterGroups_TrainingRegisters_RegisterId",
                        column: x => x.RegisterId,
                        principalTable: "TrainingRegisters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Role_id = table.Column<long>(nullable: false),
                    Position = table.Column<string>(nullable: true),
                    Prefix = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Firstnameth = table.Column<string>(nullable: true),
                    Lastnameth = table.Column<string>(nullable: true),
                    Firstnameen = table.Column<string>(nullable: true),
                    Lastnameen = table.Column<string>(nullable: true),
                    Educational = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: true),
                    Officephonenumber = table.Column<string>(nullable: true),
                    Telegraphnumber = table.Column<string>(nullable: true),
                    SideId = table.Column<long>(nullable: false),
                    MinistryId = table.Column<long>(nullable: false),
                    DepartmentId = table.Column<long>(nullable: false),
                    ProvincialDepartmentId = table.Column<long>(nullable: false),
                    ProvinceId = table.Column<long>(nullable: false),
                    DistrictId = table.Column<long>(nullable: false),
                    SubdistrictId = table.Column<long>(nullable: false),
                    Housenumber = table.Column<string>(nullable: true),
                    Rold = table.Column<string>(nullable: true),
                    Alley = table.Column<string>(nullable: true),
                    Postalcode = table.Column<string>(nullable: true),
                    Img = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    Startdate = table.Column<DateTime>(nullable: true),
                    Enddate = table.Column<DateTime>(nullable: true),
                    Active = table.Column<long>(nullable: false),
                    Signature = table.Column<string>(nullable: true),
                    Commandnumber = table.Column<string>(nullable: true),
                    Commandnumberdate = table.Column<DateTime>(nullable: true),
                    FiscalYearId = table.Column<long>(nullable: false),
                    Autocreateuser = table.Column<long>(nullable: false),
                    Pw = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Ministries_MinistryId",
                        column: x => x.MinistryId,
                        principalTable: "Ministries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_ProvincialDepartments_ProvincialDepartmentId",
                        column: x => x.ProvincialDepartmentId,
                        principalTable: "ProvincialDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Side_SideId",
                        column: x => x.SideId,
                        principalTable: "Side",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Subdistricts_SubdistrictId",
                        column: x => x.SubdistrictId,
                        principalTable: "Subdistricts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Village",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubdistrictId = table.Column<long>(nullable: false),
                    No = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Village", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Village_Subdistricts_SubdistrictId",
                        column: x => x.SubdistrictId,
                        principalTable: "Subdistricts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ElectronicBooks",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    Detail = table.Column<string>(nullable: true),
                    Problem = table.Column<string>(nullable: true),
                    Suggestion = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    ProvinceStatus = table.Column<string>(nullable: true),
                    ElectronicBookType = table.Column<string>(nullable: true),
                    ProvincialDepartmentId = table.Column<long>(nullable: false),
                    CentralPolicy = table.Column<string>(nullable: true),
                    ProvinceType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectronicBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectronicBooks_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExecutiveOrders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: false),
                    Subjectdetail = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    Commanded_date = table.Column<DateTime>(nullable: true),
                    publics = table.Column<long>(nullable: false),
                    Draft = table.Column<long>(nullable: false),
                    Accept = table.Column<long>(nullable: false),
                    Cancel = table.Column<long>(nullable: false),
                    Canceldetail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExecutiveOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExecutiveOrders_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImportReports",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FiscalYearId = table.Column<long>(nullable: false),
                    RegionId = table.Column<long>(nullable: false),
                    ProvinceId = table.Column<long>(nullable: false),
                    DepartmentId = table.Column<long>(nullable: false),
                    ZoneId = table.Column<long>(nullable: false),
                    CentralPolicyType = table.Column<string>(nullable: true),
                    ReportType = table.Column<string>(nullable: true),
                    InspectionRound = table.Column<string>(nullable: true),
                    MonitoringTopics = table.Column<string>(nullable: true),
                    DetailReport = table.Column<string>(nullable: true),
                    Suggestion = table.Column<string>(nullable: true),
                    Command = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreateAt = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    SendCommander = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImportReports_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImportReports_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImportReports_FiscalYear_FiscalYearId",
                        column: x => x.FiscalYearId,
                        principalTable: "FiscalYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImportReports_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImportReports_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImportReports_AspNetUsers_SendCommander",
                        column: x => x.SendCommander,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImportReports_Sector_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Sector",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InspectionPlanEvents",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvinceId = table.Column<long>(nullable: false),
                    ProvincialDepartmentIdCreatedBy = table.Column<long>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    RoleCreatedBy = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectionPlanEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InspectionPlanEvents_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InspectionPlanEvents_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InspectionPlanEvents_ProvincialDepartments_ProvincialDepartmentIdCreatedBy",
                        column: x => x.ProvincialDepartmentIdCreatedBy,
                        principalTable: "ProvincialDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    DatabaseName = table.Column<string>(nullable: true),
                    EventType = table.Column<string>(nullable: true),
                    EventDate = table.Column<DateTime>(nullable: true),
                    Detail = table.Column<string>(nullable: true),
                    Allid = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestOrders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: false),
                    Subjectdetail = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    Commanded_date = table.Column<DateTime>(nullable: true),
                    publics = table.Column<long>(nullable: false),
                    Draft = table.Column<long>(nullable: false),
                    Accept = table.Column<long>(nullable: false),
                    Cancel = table.Column<long>(nullable: false),
                    Canceldetail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestOrders_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProvinces",
                columns: table => new
                {
                    UserID = table.Column<string>(nullable: false),
                    ProvinceId = table.Column<long>(nullable: false),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProvinces", x => new { x.UserID, x.ProvinceId });
                    table.ForeignKey(
                        name: "FK_UserProvinces_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserProvinces_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRegions",
                columns: table => new
                {
                    UserID = table.Column<string>(nullable: false),
                    RegionId = table.Column<long>(nullable: false),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRegions", x => new { x.UserID, x.RegionId });
                    table.ForeignKey(
                        name: "FK_UserRegions_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRegions_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserTokenMobile",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true),
                    Session = table.Column<string>(nullable: true),
                    DeviceType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokenMobile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTokenMobile_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ElectronicBookAccepts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElectronicBookId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    ProvinceId = table.Column<long>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    CreateBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectronicBookAccepts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectronicBookAccepts_AspNetUsers_CreateBy",
                        column: x => x.CreateBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ElectronicBookAccepts_ElectronicBooks_ElectronicBookId",
                        column: x => x.ElectronicBookId,
                        principalTable: "ElectronicBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElectronicBookAccepts_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ElectronicBookAccepts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ElectronicBookFiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElectronicBookId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectronicBookFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectronicBookFiles_ElectronicBooks_ElectronicBookId",
                        column: x => x.ElectronicBookId,
                        principalTable: "ElectronicBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ElectronicBookInvite",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElectronicBookId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Approve = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectronicBookInvite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectronicBookInvite_ElectronicBooks_ElectronicBookId",
                        column: x => x.ElectronicBookId,
                        principalTable: "ElectronicBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElectronicBookInvite_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ElectronicBookProceeds",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    ElectronicBookId = table.Column<long>(nullable: false),
                    Proceed = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectronicBookProceeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectronicBookProceeds_ElectronicBooks_ElectronicBookId",
                        column: x => x.ElectronicBookId,
                        principalTable: "ElectronicBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ElectronicBookProceeds_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ElectronicBookProvincialDepartment",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElectronicBookId = table.Column<long>(nullable: false),
                    ProvincialDepartmentId = table.Column<long>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Approve = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    CreateBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectronicBookProvincialDepartment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectronicBookProvincialDepartment_AspNetUsers_CreateBy",
                        column: x => x.CreateBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ElectronicBookProvincialDepartment_ElectronicBooks_ElectronicBookId",
                        column: x => x.ElectronicBookId,
                        principalTable: "ElectronicBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElectronicBookProvincialDepartment_ProvincialDepartments_ProvincialDepartmentId",
                        column: x => x.ProvincialDepartmentId,
                        principalTable: "ProvincialDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ElectronicBookProvincialDepartment_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExecutiveOrderAnswers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExecutiveOrderId = table.Column<long>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    beaware_date = table.Column<DateTime>(nullable: true),
                    publics = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExecutiveOrderAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExecutiveOrderAnswers_ExecutiveOrders_ExecutiveOrderId",
                        column: x => x.ExecutiveOrderId,
                        principalTable: "ExecutiveOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExecutiveOrderAnswers_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExecutiveOrderFiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExecutiveOrderId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExecutiveOrderFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExecutiveOrderFiles_ExecutiveOrders_ExecutiveOrderId",
                        column: x => x.ExecutiveOrderId,
                        principalTable: "ExecutiveOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImportReportFiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImportReportId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportReportFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImportReportFiles_ImportReports_ImportReportId",
                        column: x => x.ImportReportId,
                        principalTable: "ImportReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportCommanders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImportReportId = table.Column<long>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Command = table.Column<string>(nullable: true),
                    UserCommanderId = table.Column<string>(nullable: true),
                    CreateAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportCommanders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportCommanders_ImportReports_ImportReportId",
                        column: x => x.ImportReportId,
                        principalTable: "ImportReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportCommanders_AspNetUsers_UserCommanderId",
                        column: x => x.UserCommanderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CentralPolicies",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FiscalYearNewId = table.Column<long>(nullable: false),
                    TypeexaminationplanId = table.Column<long>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: false),
                    Class = table.Column<string>(nullable: false),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    InspectionPlanEventId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentralPolicies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CentralPolicies_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CentralPolicies_FiscalYearNew_FiscalYearNewId",
                        column: x => x.FiscalYearNewId,
                        principalTable: "FiscalYearNew",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CentralPolicies_InspectionPlanEvents_InspectionPlanEventId",
                        column: x => x.InspectionPlanEventId,
                        principalTable: "InspectionPlanEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CentralPolicies_Typeexaminationplans_TypeexaminationplanId",
                        column: x => x.TypeexaminationplanId,
                        principalTable: "Typeexaminationplans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestOrderAnswers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestOrderId = table.Column<long>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    beaware_date = table.Column<DateTime>(nullable: true),
                    publics = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestOrderAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestOrderAnswers_RequestOrders_RequestOrderId",
                        column: x => x.RequestOrderId,
                        principalTable: "RequestOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestOrderAnswers_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestOrderFiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestOrderId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestOrderFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestOrderFiles_RequestOrders_RequestOrderId",
                        column: x => x.RequestOrderId,
                        principalTable: "RequestOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ElectronicBookProvinceApproveFiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElectronicBookAcceptId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectronicBookProvinceApproveFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectronicBookProvinceApproveFiles_ElectronicBookAccepts_ElectronicBookAcceptId",
                        column: x => x.ElectronicBookAcceptId,
                        principalTable: "ElectronicBookAccepts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ElectronicBookOtherAccepts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElectronicBookProvincialDepartmentId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    AcceptDate = table.Column<DateTime>(nullable: true),
                    ProvincialDepartmentId = table.Column<long>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    CreateBy = table.Column<string>(nullable: true),
                    OtherDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectronicBookOtherAccepts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectronicBookOtherAccepts_AspNetUsers_CreateBy",
                        column: x => x.CreateBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ElectronicBookOtherAccepts_ElectronicBookProvincialDepartment_ElectronicBookProvincialDepartmentId",
                        column: x => x.ElectronicBookProvincialDepartmentId,
                        principalTable: "ElectronicBookProvincialDepartment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElectronicBookOtherAccepts_ProvincialDepartments_ProvincialDepartmentId",
                        column: x => x.ProvincialDepartmentId,
                        principalTable: "ProvincialDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ElectronicBookOtherAccepts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExecutiveOrderAnswerDetail",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExecutiveOrderAnswerId = table.Column<long>(nullable: false),
                    Answerdetail = table.Column<string>(nullable: true),
                    AnswerProblem = table.Column<string>(nullable: true),
                    AnswerCounsel = table.Column<string>(nullable: true),
                    publics = table.Column<long>(nullable: false),
                    create_at = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExecutiveOrderAnswerDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExecutiveOrderAnswerDetail_ExecutiveOrderAnswers_ExecutiveOrderAnswerId",
                        column: x => x.ExecutiveOrderAnswerId,
                        principalTable: "ExecutiveOrderAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalendarFiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CentralPolicyId = table.Column<long>(nullable: false),
                    InspectionPlanEventId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalendarFiles_CentralPolicies_CentralPolicyId",
                        column: x => x.CentralPolicyId,
                        principalTable: "CentralPolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalendarFiles_InspectionPlanEvents_InspectionPlanEventId",
                        column: x => x.InspectionPlanEventId,
                        principalTable: "InspectionPlanEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CentralPolicyDates",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CentralPolicyId = table.Column<long>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentralPolicyDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CentralPolicyDates_CentralPolicies_CentralPolicyId",
                        column: x => x.CentralPolicyId,
                        principalTable: "CentralPolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CentralPolicyEvents",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CentralPolicyId = table.Column<long>(nullable: false),
                    InspectionPlanEventId = table.Column<long>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    NotificationDate = table.Column<DateTime>(nullable: true),
                    DeadlineDate = table.Column<DateTime>(nullable: true),
                    HaveSubject = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentralPolicyEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CentralPolicyEvents_CentralPolicies_CentralPolicyId",
                        column: x => x.CentralPolicyId,
                        principalTable: "CentralPolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CentralPolicyEvents_InspectionPlanEvents_InspectionPlanEventId",
                        column: x => x.InspectionPlanEventId,
                        principalTable: "InspectionPlanEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CentralPolicyFiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CentralPolicyId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentralPolicyFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CentralPolicyFiles_CentralPolicies_CentralPolicyId",
                        column: x => x.CentralPolicyId,
                        principalTable: "CentralPolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CentralPolicyProvinces",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CentralPolicyId = table.Column<long>(nullable: false),
                    ProvinceId = table.Column<long>(nullable: false),
                    Step = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Active = table.Column<long>(nullable: false),
                    QuestionPeople = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentralPolicyProvinces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CentralPolicyProvinces_CentralPolicies_CentralPolicyId",
                        column: x => x.CentralPolicyId,
                        principalTable: "CentralPolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CentralPolicyProvinces_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CentralPolicyUsers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CentralPolicyId = table.Column<long>(nullable: false),
                    ProvinceId = table.Column<long>(nullable: false),
                    InspectionPlanEventId = table.Column<long>(nullable: false),
                    CentralPolicyGroupId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: false),
                    Report = table.Column<string>(nullable: true),
                    ForwardName = table.Column<string>(nullable: true),
                    ForwardDepartment = table.Column<string>(nullable: true),
                    ForwardPosition = table.Column<string>(nullable: true),
                    ForwardPhone = table.Column<string>(nullable: true),
                    ForwardEmail = table.Column<string>(nullable: true),
                    ForwardCount = table.Column<long>(nullable: false),
                    ForwardExternal = table.Column<long>(nullable: false),
                    InvitedBy = table.Column<string>(nullable: true),
                    DraftStatus = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentralPolicyUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CentralPolicyUsers_CentralPolicyGroups_CentralPolicyGroupId",
                        column: x => x.CentralPolicyGroupId,
                        principalTable: "CentralPolicyGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CentralPolicyUsers_CentralPolicies_CentralPolicyId",
                        column: x => x.CentralPolicyId,
                        principalTable: "CentralPolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CentralPolicyUsers_InspectionPlanEvents_InspectionPlanEventId",
                        column: x => x.InspectionPlanEventId,
                        principalTable: "InspectionPlanEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CentralPolicyUsers_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CentralPolicyUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(nullable: true),
                    CentralPolicyId = table.Column<long>(nullable: false),
                    ProvinceId = table.Column<long>(nullable: false),
                    status = table.Column<long>(nullable: false),
                    noti = table.Column<long>(nullable: false),
                    xe = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    ActiveDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_CentralPolicies_CentralPolicyId",
                        column: x => x.CentralPolicyId,
                        principalTable: "CentralPolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notification_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubjectGroups",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CentralPolicyId = table.Column<long>(nullable: false),
                    ProvinceId = table.Column<long>(nullable: false),
                    ProvincialDepartmentIdCreatedBy = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    RoleCreatedBy = table.Column<long>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Land = table.Column<string>(nullable: true),
                    Suggestion = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    SubjectNotificationDate = table.Column<DateTime>(nullable: true),
                    SubjectDeadlineDate = table.Column<DateTime>(nullable: true),
                    PeopleQuestionNotificationDate = table.Column<DateTime>(nullable: true),
                    PeopleQuestionDeadlineDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectGroups_CentralPolicies_CentralPolicyId",
                        column: x => x.CentralPolicyId,
                        principalTable: "CentralPolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectGroups_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubjectGroups_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubjectGroups_ProvincialDepartments_ProvincialDepartmentIdCreatedBy",
                        column: x => x.ProvincialDepartmentIdCreatedBy,
                        principalTable: "ProvincialDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CentralPolicyId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Answer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subjects_CentralPolicies_CentralPolicyId",
                        column: x => x.CentralPolicyId,
                        principalTable: "CentralPolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestOrderAnswerDetail",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestOrderAnswerId = table.Column<long>(nullable: false),
                    Answerdetail = table.Column<string>(nullable: true),
                    AnswerProblem = table.Column<string>(nullable: true),
                    AnswerCounsel = table.Column<string>(nullable: true),
                    publics = table.Column<long>(nullable: false),
                    create_at = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestOrderAnswerDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestOrderAnswerDetail_RequestOrderAnswers_RequestOrderAnswerId",
                        column: x => x.RequestOrderAnswerId,
                        principalTable: "RequestOrderAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnswerExecutiveOrderFiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExecutiveOrderAnswerDetailId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerExecutiveOrderFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerExecutiveOrderFiles_ExecutiveOrderAnswerDetail_ExecutiveOrderAnswerDetailId",
                        column: x => x.ExecutiveOrderAnswerDetailId,
                        principalTable: "ExecutiveOrderAnswerDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnswerCentralPolicyProvinceStatuses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CentralPolicyEventId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerCentralPolicyProvinceStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerCentralPolicyProvinceStatuses_CentralPolicyEvents_CentralPolicyEventId",
                        column: x => x.CentralPolicyEventId,
                        principalTable: "CentralPolicyEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnswerCentralPolicyProvinceStatuses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CentralPolicyEventQuestions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CentralPolicyEventId = table.Column<long>(nullable: false),
                    QuestionPeople = table.Column<string>(nullable: true),
                    NotificationDate = table.Column<DateTime>(nullable: true),
                    DeadlineDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentralPolicyEventQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CentralPolicyEventQuestions_CentralPolicyEvents_CentralPolicyEventId",
                        column: x => x.CentralPolicyEventId,
                        principalTable: "CentralPolicyEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ElectronicBookGroups",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElectronicBookId = table.Column<long>(nullable: false),
                    CentralPolicyEventId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectronicBookGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectronicBookGroups_CentralPolicyEvents_CentralPolicyEventId",
                        column: x => x.CentralPolicyEventId,
                        principalTable: "CentralPolicyEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ElectronicBookGroups_ElectronicBooks_ElectronicBookId",
                        column: x => x.ElectronicBookId,
                        principalTable: "ElectronicBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ElectronicBookSuggestGroups",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElectronicBookId = table.Column<long>(nullable: false),
                    CentralPolicyEventId = table.Column<long>(nullable: false),
                    Suggestion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectronicBookSuggestGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectronicBookSuggestGroups_CentralPolicyEvents_CentralPolicyEventId",
                        column: x => x.CentralPolicyEventId,
                        principalTable: "CentralPolicyEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ElectronicBookSuggestGroups_ElectronicBooks_ElectronicBookId",
                        column: x => x.ElectronicBookId,
                        principalTable: "ElectronicBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImportReportGroups",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImportReportId = table.Column<long>(nullable: false),
                    CentralPolicyEventId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportReportGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImportReportGroups_CentralPolicyEvents_CentralPolicyEventId",
                        column: x => x.CentralPolicyEventId,
                        principalTable: "CentralPolicyEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImportReportGroups_ImportReports_ImportReportId",
                        column: x => x.ImportReportId,
                        principalTable: "ImportReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CentralPolicyProvinceEvents",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CentralPolicyProvinceId = table.Column<long>(nullable: false),
                    InspectionPlanEventId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentralPolicyProvinceEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CentralPolicyProvinceEvents_CentralPolicyProvinces_CentralPolicyProvinceId",
                        column: x => x.CentralPolicyProvinceId,
                        principalTable: "CentralPolicyProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CentralPolicyProvinceEvents_InspectionPlanEvents_InspectionPlanEventId",
                        column: x => x.InspectionPlanEventId,
                        principalTable: "InspectionPlanEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnswerRecommenDationInspectors",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectGroupId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Answersuggestion = table.Column<string>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerRecommenDationInspectors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerRecommenDationInspectors_SubjectGroups_SubjectGroupId",
                        column: x => x.SubjectGroupId,
                        principalTable: "SubjectGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnswerRecommenDationInspectors_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubjectCentralPolicyProvinces",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CentralPolicyProvinceId = table.Column<long>(nullable: false),
                    SubjectGroupId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Explanation = table.Column<string>(nullable: true),
                    CheckAnswer = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: false),
                    UpdateAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectCentralPolicyProvinces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectCentralPolicyProvinces_CentralPolicyProvinces_CentralPolicyProvinceId",
                        column: x => x.CentralPolicyProvinceId,
                        principalTable: "CentralPolicyProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectCentralPolicyProvinces_SubjectGroups_SubjectGroupId",
                        column: x => x.SubjectGroupId,
                        principalTable: "SubjectGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubjectEventFiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectGroupId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectEventFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectEventFiles_SubjectGroups_SubjectGroupId",
                        column: x => x.SubjectGroupId,
                        principalTable: "SubjectGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectGroupPeopleQuestions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectGroupId = table.Column<long>(nullable: false),
                    CentralPolicyEventId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectGroupPeopleQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectGroupPeopleQuestions_CentralPolicyEvents_CentralPolicyEventId",
                        column: x => x.CentralPolicyEventId,
                        principalTable: "CentralPolicyEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectGroupPeopleQuestions_SubjectGroups_SubjectGroupId",
                        column: x => x.SubjectGroupId,
                        principalTable: "SubjectGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubjectDates",
                columns: table => new
                {
                    SubjectId = table.Column<long>(nullable: false),
                    CentralPolicyDateId = table.Column<long>(nullable: false),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectDates", x => new { x.SubjectId, x.CentralPolicyDateId });
                    table.ForeignKey(
                        name: "FK_SubjectDates_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subquestions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subquestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subquestions_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnswerRequestOrderFiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestOrderAnswerDetailId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerRequestOrderFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerRequestOrderFiles_RequestOrderAnswerDetail_RequestOrderAnswerDetailId",
                        column: x => x.RequestOrderAnswerDetailId,
                        principalTable: "RequestOrderAnswerDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnswerCentralPolicyProvinces",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CentralPolicyProvinceId = table.Column<long>(nullable: false),
                    CentralPolicyEventQuestionId = table.Column<long>(nullable: false),
                    AnswerCentralPolicyProvinceStatusId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Answer = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerCentralPolicyProvinces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerCentralPolicyProvinces_AnswerCentralPolicyProvinceStatuses_AnswerCentralPolicyProvinceStatusId",
                        column: x => x.AnswerCentralPolicyProvinceStatusId,
                        principalTable: "AnswerCentralPolicyProvinceStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnswerCentralPolicyProvinces_CentralPolicyEventQuestions_CentralPolicyEventQuestionId",
                        column: x => x.CentralPolicyEventQuestionId,
                        principalTable: "CentralPolicyEventQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnswerCentralPolicyProvinces_CentralPolicyProvinces_CentralPolicyProvinceId",
                        column: x => x.CentralPolicyProvinceId,
                        principalTable: "CentralPolicyProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnswerCentralPolicyProvinces_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnswerSubquestionFiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectCentralPolicyProvinceId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerSubquestionFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerSubquestionFiles_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                        column: x => x.SubjectCentralPolicyProvinceId,
                        principalTable: "SubjectCentralPolicyProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnswerSubquestionFiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnswerSubquestionStatuses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectCentralPolicyProvinceId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerSubquestionStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerSubquestionStatuses_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                        column: x => x.SubjectCentralPolicyProvinceId,
                        principalTable: "SubjectCentralPolicyProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnswerSubquestionStatuses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubjectCentralPolicyProvinceFiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectCentralPolicyProvinceId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectCentralPolicyProvinceFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectCentralPolicyProvinceFiles_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                        column: x => x.SubjectCentralPolicyProvinceId,
                        principalTable: "SubjectCentralPolicyProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectDateCentralPolicyProvinces",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectCentralPolicyProvinceId = table.Column<long>(nullable: false),
                    CentralPolicyDateProvinceId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectDateCentralPolicyProvinces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectDateCentralPolicyProvinces_CentralPolicyDateProvinces_CentralPolicyDateProvinceId",
                        column: x => x.CentralPolicyDateProvinceId,
                        principalTable: "CentralPolicyDateProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubjectDateCentralPolicyProvinces_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                        column: x => x.SubjectCentralPolicyProvinceId,
                        principalTable: "SubjectCentralPolicyProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubquestionCentralPolicyProvinces",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectCentralPolicyProvinceId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    Box = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubquestionCentralPolicyProvinces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubquestionCentralPolicyProvinces_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                        column: x => x.SubjectCentralPolicyProvinceId,
                        principalTable: "SubjectCentralPolicyProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SuggestionSubject",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectCentralPolicyProvinceId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Suggestion = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuggestionSubject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuggestionSubject_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                        column: x => x.SubjectCentralPolicyProvinceId,
                        principalTable: "SubjectCentralPolicyProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SuggestionSubject_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubquestionChoices",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubquestionId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubquestionChoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubquestionChoices_Subquestions_SubquestionId",
                        column: x => x.SubquestionId,
                        principalTable: "Subquestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnswerSubquestionOutsiders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubquestionCentralPolicyProvinceId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Position = table.Column<string>(nullable: false),
                    Phonenumber = table.Column<string>(nullable: false),
                    Answer = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    SenderUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerSubquestionOutsiders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerSubquestionOutsiders_AspNetUsers_SenderUserId",
                        column: x => x.SenderUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnswerSubquestionOutsiders_SubquestionCentralPolicyProvinces_SubquestionCentralPolicyProvinceId",
                        column: x => x.SubquestionCentralPolicyProvinceId,
                        principalTable: "SubquestionCentralPolicyProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnswerSubquestions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubquestionCentralPolicyProvinceId = table.Column<long>(nullable: false),
                    AnswerSubquestionStatusId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Answer = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerSubquestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerSubquestions_AnswerSubquestionStatuses_AnswerSubquestionStatusId",
                        column: x => x.AnswerSubquestionStatusId,
                        principalTable: "AnswerSubquestionStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnswerSubquestions_SubquestionCentralPolicyProvinces_SubquestionCentralPolicyProvinceId",
                        column: x => x.SubquestionCentralPolicyProvinceId,
                        principalTable: "SubquestionCentralPolicyProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnswerSubquestions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubjectCentralPolicyProvinceGroups",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubquestionCentralPolicyProvinceId = table.Column<long>(nullable: false),
                    ProvincialDepartmentId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectCentralPolicyProvinceGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectCentralPolicyProvinceGroups_ProvincialDepartments_ProvincialDepartmentId",
                        column: x => x.ProvincialDepartmentId,
                        principalTable: "ProvincialDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubjectCentralPolicyProvinceGroups_SubquestionCentralPolicyProvinces_SubquestionCentralPolicyProvinceId",
                        column: x => x.SubquestionCentralPolicyProvinceId,
                        principalTable: "SubquestionCentralPolicyProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectCentralPolicyProvinceUserGroups",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectCentralPolicyProvinceId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    SubquestionCentralPolicyProvinceId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectCentralPolicyProvinceUserGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectCentralPolicyProvinceUserGroups_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                        column: x => x.SubjectCentralPolicyProvinceId,
                        principalTable: "SubjectCentralPolicyProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubjectCentralPolicyProvinceUserGroups_SubquestionCentralPolicyProvinces_SubquestionCentralPolicyProvinceId",
                        column: x => x.SubquestionCentralPolicyProvinceId,
                        principalTable: "SubquestionCentralPolicyProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubjectCentralPolicyProvinceUserGroups_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubquestionChoiceCentralPolicyProvinces",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubquestionCentralPolicyProvinceId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubquestionChoiceCentralPolicyProvinces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubquestionChoiceCentralPolicyProvinces_SubquestionCentralPolicyProvinces_SubquestionCentralPolicyProvinceId",
                        column: x => x.SubquestionCentralPolicyProvinceId,
                        principalTable: "SubquestionCentralPolicyProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerCentralPolicyProvinces_AnswerCentralPolicyProvinceStatusId",
                table: "AnswerCentralPolicyProvinces",
                column: "AnswerCentralPolicyProvinceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerCentralPolicyProvinces_CentralPolicyEventQuestionId",
                table: "AnswerCentralPolicyProvinces",
                column: "CentralPolicyEventQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerCentralPolicyProvinces_CentralPolicyProvinceId",
                table: "AnswerCentralPolicyProvinces",
                column: "CentralPolicyProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerCentralPolicyProvinces_UserId",
                table: "AnswerCentralPolicyProvinces",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerCentralPolicyProvinceStatuses_CentralPolicyEventId",
                table: "AnswerCentralPolicyProvinceStatuses",
                column: "CentralPolicyEventId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerCentralPolicyProvinceStatuses_UserId",
                table: "AnswerCentralPolicyProvinceStatuses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerExecutiveOrderFiles_ExecutiveOrderAnswerDetailId",
                table: "AnswerExecutiveOrderFiles",
                column: "ExecutiveOrderAnswerDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerRecommenDationInspectors_SubjectGroupId",
                table: "AnswerRecommenDationInspectors",
                column: "SubjectGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerRecommenDationInspectors_UserId",
                table: "AnswerRecommenDationInspectors",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerRequestOrderFiles_RequestOrderAnswerDetailId",
                table: "AnswerRequestOrderFiles",
                column: "RequestOrderAnswerDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerSubquestionFiles_SubjectCentralPolicyProvinceId",
                table: "AnswerSubquestionFiles",
                column: "SubjectCentralPolicyProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerSubquestionFiles_UserId",
                table: "AnswerSubquestionFiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerSubquestionOutsiders_SenderUserId",
                table: "AnswerSubquestionOutsiders",
                column: "SenderUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerSubquestionOutsiders_SubquestionCentralPolicyProvinceId",
                table: "AnswerSubquestionOutsiders",
                column: "SubquestionCentralPolicyProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerSubquestions_AnswerSubquestionStatusId",
                table: "AnswerSubquestions",
                column: "AnswerSubquestionStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerSubquestions_SubquestionCentralPolicyProvinceId",
                table: "AnswerSubquestions",
                column: "SubquestionCentralPolicyProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerSubquestions_UserId",
                table: "AnswerSubquestions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerSubquestionStatuses_SubjectCentralPolicyProvinceId",
                table: "AnswerSubquestionStatuses",
                column: "SubjectCentralPolicyProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerSubquestionStatuses_UserId",
                table: "AnswerSubquestionStatuses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DepartmentId",
                table: "AspNetUsers",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DistrictId",
                table: "AspNetUsers",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MinistryId",
                table: "AspNetUsers",
                column: "MinistryId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProvinceId",
                table: "AspNetUsers",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProvincialDepartmentId",
                table: "AspNetUsers",
                column: "ProvincialDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SideId",
                table: "AspNetUsers",
                column: "SideId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SubdistrictId",
                table: "AspNetUsers",
                column: "SubdistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarFiles_CentralPolicyId",
                table: "CalendarFiles",
                column: "CentralPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarFiles_InspectionPlanEventId",
                table: "CalendarFiles",
                column: "InspectionPlanEventId");

            migrationBuilder.CreateIndex(
                name: "IX_CentralDepartmentProvince_ProvinceId",
                table: "CentralDepartmentProvince",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_CentralDepartments_DepartmentId",
                table: "CentralDepartments",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CentralPolicies_CreatedBy",
                table: "CentralPolicies",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CentralPolicies_FiscalYearNewId",
                table: "CentralPolicies",
                column: "FiscalYearNewId");

            migrationBuilder.CreateIndex(
                name: "IX_CentralPolicies_InspectionPlanEventId",
                table: "CentralPolicies",
                column: "InspectionPlanEventId");

            migrationBuilder.CreateIndex(
                name: "IX_CentralPolicies_TypeexaminationplanId",
                table: "CentralPolicies",
                column: "TypeexaminationplanId");

            migrationBuilder.CreateIndex(
                name: "IX_CentralPolicyDates_CentralPolicyId",
                table: "CentralPolicyDates",
                column: "CentralPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_CentralPolicyEventQuestions_CentralPolicyEventId",
                table: "CentralPolicyEventQuestions",
                column: "CentralPolicyEventId");

            migrationBuilder.CreateIndex(
                name: "IX_CentralPolicyEvents_CentralPolicyId",
                table: "CentralPolicyEvents",
                column: "CentralPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_CentralPolicyEvents_InspectionPlanEventId",
                table: "CentralPolicyEvents",
                column: "InspectionPlanEventId");

            migrationBuilder.CreateIndex(
                name: "IX_CentralPolicyFiles_CentralPolicyId",
                table: "CentralPolicyFiles",
                column: "CentralPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_CentralPolicyProvinceEvents_CentralPolicyProvinceId",
                table: "CentralPolicyProvinceEvents",
                column: "CentralPolicyProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_CentralPolicyProvinceEvents_InspectionPlanEventId",
                table: "CentralPolicyProvinceEvents",
                column: "InspectionPlanEventId");

            migrationBuilder.CreateIndex(
                name: "IX_CentralPolicyProvinces_CentralPolicyId",
                table: "CentralPolicyProvinces",
                column: "CentralPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_CentralPolicyProvinces_ProvinceId",
                table: "CentralPolicyProvinces",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_CentralPolicyUserFiles_CentralPolicyGroupId",
                table: "CentralPolicyUserFiles",
                column: "CentralPolicyGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CentralPolicyUsers_CentralPolicyGroupId",
                table: "CentralPolicyUsers",
                column: "CentralPolicyGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CentralPolicyUsers_CentralPolicyId",
                table: "CentralPolicyUsers",
                column: "CentralPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_CentralPolicyUsers_InspectionPlanEventId",
                table: "CentralPolicyUsers",
                column: "InspectionPlanEventId");

            migrationBuilder.CreateIndex(
                name: "IX_CentralPolicyUsers_ProvinceId",
                table: "CentralPolicyUsers",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_CentralPolicyUsers_UserId",
                table: "CentralPolicyUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_MinistryId",
                table: "Departments",
                column: "MinistryId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_DeviceCode",
                table: "DeviceCodes",
                column: "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_Expiration",
                table: "DeviceCodes",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_ProvinceId",
                table: "Districts",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookAccepts_CreateBy",
                table: "ElectronicBookAccepts",
                column: "CreateBy");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookAccepts_ElectronicBookId",
                table: "ElectronicBookAccepts",
                column: "ElectronicBookId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookAccepts_ProvinceId",
                table: "ElectronicBookAccepts",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookAccepts_UserId",
                table: "ElectronicBookAccepts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookFiles_ElectronicBookId",
                table: "ElectronicBookFiles",
                column: "ElectronicBookId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookGroups_CentralPolicyEventId",
                table: "ElectronicBookGroups",
                column: "CentralPolicyEventId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookGroups_ElectronicBookId",
                table: "ElectronicBookGroups",
                column: "ElectronicBookId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookInvite_ElectronicBookId",
                table: "ElectronicBookInvite",
                column: "ElectronicBookId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookInvite_UserId",
                table: "ElectronicBookInvite",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookOtherAccepts_CreateBy",
                table: "ElectronicBookOtherAccepts",
                column: "CreateBy");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookOtherAccepts_ElectronicBookProvincialDepartmentId",
                table: "ElectronicBookOtherAccepts",
                column: "ElectronicBookProvincialDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookOtherAccepts_ProvincialDepartmentId",
                table: "ElectronicBookOtherAccepts",
                column: "ProvincialDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookOtherAccepts_UserId",
                table: "ElectronicBookOtherAccepts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookProceeds_ElectronicBookId",
                table: "ElectronicBookProceeds",
                column: "ElectronicBookId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookProceeds_UserId",
                table: "ElectronicBookProceeds",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookProvinceApproveFiles_ElectronicBookAcceptId",
                table: "ElectronicBookProvinceApproveFiles",
                column: "ElectronicBookAcceptId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookProvincialDepartment_CreateBy",
                table: "ElectronicBookProvincialDepartment",
                column: "CreateBy");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookProvincialDepartment_ElectronicBookId",
                table: "ElectronicBookProvincialDepartment",
                column: "ElectronicBookId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookProvincialDepartment_ProvincialDepartmentId",
                table: "ElectronicBookProvincialDepartment",
                column: "ProvincialDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookProvincialDepartment_UserId",
                table: "ElectronicBookProvincialDepartment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBooks_CreatedBy",
                table: "ElectronicBooks",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookSuggestGroups_CentralPolicyEventId",
                table: "ElectronicBookSuggestGroups",
                column: "CentralPolicyEventId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookSuggestGroups_ElectronicBookId",
                table: "ElectronicBookSuggestGroups",
                column: "ElectronicBookId");

            migrationBuilder.CreateIndex(
                name: "IX_ExecutiveOrderAnswerDetail_ExecutiveOrderAnswerId",
                table: "ExecutiveOrderAnswerDetail",
                column: "ExecutiveOrderAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_ExecutiveOrderAnswers_ExecutiveOrderId",
                table: "ExecutiveOrderAnswers",
                column: "ExecutiveOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ExecutiveOrderAnswers_UserID",
                table: "ExecutiveOrderAnswers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ExecutiveOrderFiles_ExecutiveOrderId",
                table: "ExecutiveOrderFiles",
                column: "ExecutiveOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ExecutiveOrders_UserID",
                table: "ExecutiveOrders",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ExportRegistration_ProvinceId",
                table: "ExportRegistration",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_ExportReportBody_ExportReportHeadId",
                table: "ExportReportBody",
                column: "ExportReportHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_ExportReportHead_ProvinceId",
                table: "ExportReportHead",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_FiscalYearRelations_FiscalYearId",
                table: "FiscalYearRelations",
                column: "FiscalYearId");

            migrationBuilder.CreateIndex(
                name: "IX_FiscalYearRelations_ProvinceId",
                table: "FiscalYearRelations",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_FiscalYearRelations_RegionId",
                table: "FiscalYearRelations",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportReportFiles_ImportReportId",
                table: "ImportReportFiles",
                column: "ImportReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportReportGroups_CentralPolicyEventId",
                table: "ImportReportGroups",
                column: "CentralPolicyEventId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportReportGroups_ImportReportId",
                table: "ImportReportGroups",
                column: "ImportReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportReports_CreatedBy",
                table: "ImportReports",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ImportReports_DepartmentId",
                table: "ImportReports",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportReports_FiscalYearId",
                table: "ImportReports",
                column: "FiscalYearId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportReports_ProvinceId",
                table: "ImportReports",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportReports_RegionId",
                table: "ImportReports",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportReports_SendCommander",
                table: "ImportReports",
                column: "SendCommander");

            migrationBuilder.CreateIndex(
                name: "IX_ImportReports_ZoneId",
                table: "ImportReports",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_InspectionPlanEvents_CreatedBy",
                table: "InspectionPlanEvents",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_InspectionPlanEvents_ProvinceId",
                table: "InspectionPlanEvents",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_InspectionPlanEvents_ProvincialDepartmentIdCreatedBy",
                table: "InspectionPlanEvents",
                column: "ProvincialDepartmentIdCreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_InspectorRegions_InspectorId",
                table: "InspectorRegions",
                column: "InspectorId");

            migrationBuilder.CreateIndex(
                name: "IX_InspectorRegions_RegionId",
                table: "InspectorRegions",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_UserId",
                table: "Logs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MinistermonitoringRegions_MinistermonitoringId",
                table: "MinistermonitoringRegions",
                column: "MinistermonitoringId");

            migrationBuilder.CreateIndex(
                name: "IX_MinistermonitoringRegions_RegionId",
                table: "MinistermonitoringRegions",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_CentralPolicyId",
                table: "Notification",
                column: "CentralPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserID",
                table: "Notification",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_Expiration",
                table: "PersistedGrants",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_ClientId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "ClientId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_ProvincesGroupId",
                table: "Provinces",
                column: "ProvincesGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_RegionId",
                table: "Provinces",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_SectorId",
                table: "Provinces",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProvincialDepartmentProvince_ProvinceId",
                table: "ProvincialDepartmentProvince",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProvincialDepartments_DepartmentId",
                table: "ProvincialDepartments",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportCommanders_ImportReportId",
                table: "ReportCommanders",
                column: "ImportReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportCommanders_UserCommanderId",
                table: "ReportCommanders",
                column: "UserCommanderId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestOrderAnswerDetail_RequestOrderAnswerId",
                table: "RequestOrderAnswerDetail",
                column: "RequestOrderAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestOrderAnswers_RequestOrderId",
                table: "RequestOrderAnswers",
                column: "RequestOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestOrderAnswers_UserID",
                table: "RequestOrderAnswers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestOrderFiles_RequestOrderId",
                table: "RequestOrderFiles",
                column: "RequestOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestOrders_UserID",
                table: "RequestOrders",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_SetinspectionareaFiles_FiscalYearId",
                table: "SetinspectionareaFiles",
                column: "FiscalYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Subdistricts_DistrictId",
                table: "Subdistricts",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectCentralPolicyProvinceFiles_SubjectCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceFiles",
                column: "SubjectCentralPolicyProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectCentralPolicyProvinceGroups_ProvincialDepartmentId",
                table: "SubjectCentralPolicyProvinceGroups",
                column: "ProvincialDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectCentralPolicyProvinceGroups_SubquestionCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceGroups",
                column: "SubquestionCentralPolicyProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectCentralPolicyProvinces_CentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinces",
                column: "CentralPolicyProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectCentralPolicyProvinces_SubjectGroupId",
                table: "SubjectCentralPolicyProvinces",
                column: "SubjectGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectCentralPolicyProvinceUserGroups_SubjectCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceUserGroups",
                column: "SubjectCentralPolicyProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectCentralPolicyProvinceUserGroups_SubquestionCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceUserGroups",
                column: "SubquestionCentralPolicyProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectCentralPolicyProvinceUserGroups_UserId",
                table: "SubjectCentralPolicyProvinceUserGroups",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectDateCentralPolicyProvinces_CentralPolicyDateProvinceId",
                table: "SubjectDateCentralPolicyProvinces",
                column: "CentralPolicyDateProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectDateCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                table: "SubjectDateCentralPolicyProvinces",
                column: "SubjectCentralPolicyProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectEventFiles_SubjectGroupId",
                table: "SubjectEventFiles",
                column: "SubjectGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectGroupPeopleQuestions_CentralPolicyEventId",
                table: "SubjectGroupPeopleQuestions",
                column: "CentralPolicyEventId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectGroupPeopleQuestions_SubjectGroupId",
                table: "SubjectGroupPeopleQuestions",
                column: "SubjectGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectGroups_CentralPolicyId",
                table: "SubjectGroups",
                column: "CentralPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectGroups_CreatedBy",
                table: "SubjectGroups",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectGroups_ProvinceId",
                table: "SubjectGroups",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectGroups_ProvincialDepartmentIdCreatedBy",
                table: "SubjectGroups",
                column: "ProvincialDepartmentIdCreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_CentralPolicyId",
                table: "Subjects",
                column: "CentralPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_SubquestionCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                table: "SubquestionCentralPolicyProvinces",
                column: "SubjectCentralPolicyProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_SubquestionChoiceCentralPolicyProvinces_SubquestionCentralPolicyProvinceId",
                table: "SubquestionChoiceCentralPolicyProvinces",
                column: "SubquestionCentralPolicyProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_SubquestionChoices_SubquestionId",
                table: "SubquestionChoices",
                column: "SubquestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Subquestions_SubjectId",
                table: "Subquestions",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionSubject_SubjectCentralPolicyProvinceId",
                table: "SuggestionSubject",
                column: "SubjectCentralPolicyProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionSubject_UserId",
                table: "SuggestionSubject",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingConditions_TrainingId",
                table: "TrainingConditions",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingDocuments_TrainingId",
                table: "TrainingDocuments",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPhases_TrainingId",
                table: "TrainingPhases",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingProgramFiles_TrainingProgramId",
                table: "TrainingProgramFiles",
                column: "TrainingProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingProgramLecturers_TrainingLecturerId",
                table: "TrainingProgramLecturers",
                column: "TrainingLecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingProgramLecturers_TrainingProgramId",
                table: "TrainingProgramLecturers",
                column: "TrainingProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingProgramLoginQRCodes_TrainingProgramId",
                table: "TrainingProgramLoginQRCodes",
                column: "TrainingProgramId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPrograms_TrainingPhaseId",
                table: "TrainingPrograms",
                column: "TrainingPhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingRegisterConditions_ConditionId",
                table: "TrainingRegisterConditions",
                column: "ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingRegisterConditions_RegisterId",
                table: "TrainingRegisterConditions",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingRegisterFiles_RegisterId",
                table: "TrainingRegisterFiles",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingRegisterGroups_RegisterId",
                table: "TrainingRegisterGroups",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingRegisters_ProvincialDepartmentId",
                table: "TrainingRegisters",
                column: "ProvincialDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingRegisters_TrainingId",
                table: "TrainingRegisters",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingSurveyAnswers_TrainingSurveyId",
                table: "TrainingSurveyAnswers",
                column: "TrainingSurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingSurveys_TrainingId",
                table: "TrainingSurveys",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingSurveys_TrainingSurveyTopicId",
                table: "TrainingSurveys",
                column: "TrainingSurveyTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProvinces_ProvinceId",
                table: "UserProvinces",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRegions_RegionId",
                table: "UserRegions",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTokenMobile_UserID",
                table: "UserTokenMobile",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Village_SubdistrictId",
                table: "Village",
                column: "SubdistrictId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerCentralPolicyProvinces");

            migrationBuilder.DropTable(
                name: "AnswerExecutiveOrderFiles");

            migrationBuilder.DropTable(
                name: "AnswerRecommenDationInspectors");

            migrationBuilder.DropTable(
                name: "AnswerRequestOrderFiles");

            migrationBuilder.DropTable(
                name: "AnswerSubquestionFiles");

            migrationBuilder.DropTable(
                name: "AnswerSubquestionOutsiders");

            migrationBuilder.DropTable(
                name: "AnswerSubquestions");

            migrationBuilder.DropTable(
                name: "Approvaldocuments");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Cabines");

            migrationBuilder.DropTable(
                name: "CalendarFiles");

            migrationBuilder.DropTable(
                name: "CentralDepartmentProvince");

            migrationBuilder.DropTable(
                name: "CentralPolicyDates");

            migrationBuilder.DropTable(
                name: "CentralPolicyFiles");

            migrationBuilder.DropTable(
                name: "CentralPolicyProvinceEvents");

            migrationBuilder.DropTable(
                name: "CentralPolicyUserFiles");

            migrationBuilder.DropTable(
                name: "CentralPolicyUsers");

            migrationBuilder.DropTable(
                name: "Circularletter");

            migrationBuilder.DropTable(
                name: "DeviceCodes");

            migrationBuilder.DropTable(
                name: "Documenttemplates");

            migrationBuilder.DropTable(
                name: "ElectronicBookFiles");

            migrationBuilder.DropTable(
                name: "ElectronicBookGroups");

            migrationBuilder.DropTable(
                name: "ElectronicBookInvite");

            migrationBuilder.DropTable(
                name: "ElectronicBookOtherAccepts");

            migrationBuilder.DropTable(
                name: "ElectronicBookProceeds");

            migrationBuilder.DropTable(
                name: "ElectronicBookProvinceApproveFiles");

            migrationBuilder.DropTable(
                name: "ElectronicBookSuggestGroups");

            migrationBuilder.DropTable(
                name: "ExecutiveOrderFiles");

            migrationBuilder.DropTable(
                name: "ExportRegistration");

            migrationBuilder.DropTable(
                name: "ExportReportBody");

            migrationBuilder.DropTable(
                name: "FiscalYearRelations");

            migrationBuilder.DropTable(
                name: "Governmentinspectionplans");

            migrationBuilder.DropTable(
                name: "ImportReportFiles");

            migrationBuilder.DropTable(
                name: "ImportReportGroups");

            migrationBuilder.DropTable(
                name: "Informationinspection");

            migrationBuilder.DropTable(
                name: "Informationoperations");

            migrationBuilder.DropTable(
                name: "InspectionOrders");

            migrationBuilder.DropTable(
                name: "InspectorRegions");

            migrationBuilder.DropTable(
                name: "InstructionOrders");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Meetinginformations");

            migrationBuilder.DropTable(
                name: "MinistermonitoringRegions");

            migrationBuilder.DropTable(
                name: "Nationalstrategies");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "PersistedGrants");

            migrationBuilder.DropTable(
                name: "Premierorders");

            migrationBuilder.DropTable(
                name: "ProvincialDepartmentProvince");

            migrationBuilder.DropTable(
                name: "ReportCommanders");

            migrationBuilder.DropTable(
                name: "RequestOrderFiles");

            migrationBuilder.DropTable(
                name: "SetinspectionareaFiles");

            migrationBuilder.DropTable(
                name: "StatePolicys");

            migrationBuilder.DropTable(
                name: "SubjectCentralPolicyProvinceFiles");

            migrationBuilder.DropTable(
                name: "SubjectCentralPolicyProvinceGroups");

            migrationBuilder.DropTable(
                name: "SubjectCentralPolicyProvinceUserGroups");

            migrationBuilder.DropTable(
                name: "SubjectDateCentralPolicyProvinces");

            migrationBuilder.DropTable(
                name: "SubjectDates");

            migrationBuilder.DropTable(
                name: "SubjectEventFiles");

            migrationBuilder.DropTable(
                name: "SubjectGroupPeopleQuestions");

            migrationBuilder.DropTable(
                name: "SubquestionChoiceCentralPolicyProvinces");

            migrationBuilder.DropTable(
                name: "SubquestionChoices");

            migrationBuilder.DropTable(
                name: "SuggestionSubject");

            migrationBuilder.DropTable(
                name: "TrainingDocuments");

            migrationBuilder.DropTable(
                name: "TrainingLecturerJoinSurveys");

            migrationBuilder.DropTable(
                name: "TrainingLogin");

            migrationBuilder.DropTable(
                name: "TrainingProgramFiles");

            migrationBuilder.DropTable(
                name: "TrainingProgramLecturers");

            migrationBuilder.DropTable(
                name: "TrainingProgramLoginQRCodes");

            migrationBuilder.DropTable(
                name: "TrainingRegisterConditions");

            migrationBuilder.DropTable(
                name: "TrainingRegisterFiles");

            migrationBuilder.DropTable(
                name: "TrainingRegisterGroups");

            migrationBuilder.DropTable(
                name: "TrainingSurveyAnswers");

            migrationBuilder.DropTable(
                name: "UserProvinces");

            migrationBuilder.DropTable(
                name: "UserRegions");

            migrationBuilder.DropTable(
                name: "UserTokenMobile");

            migrationBuilder.DropTable(
                name: "Village");

            migrationBuilder.DropTable(
                name: "AnswerCentralPolicyProvinceStatuses");

            migrationBuilder.DropTable(
                name: "CentralPolicyEventQuestions");

            migrationBuilder.DropTable(
                name: "ExecutiveOrderAnswerDetail");

            migrationBuilder.DropTable(
                name: "RequestOrderAnswerDetail");

            migrationBuilder.DropTable(
                name: "AnswerSubquestionStatuses");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "CentralDepartments");

            migrationBuilder.DropTable(
                name: "CentralPolicyGroups");

            migrationBuilder.DropTable(
                name: "ElectronicBookProvincialDepartment");

            migrationBuilder.DropTable(
                name: "ElectronicBookAccepts");

            migrationBuilder.DropTable(
                name: "ExportReportHead");

            migrationBuilder.DropTable(
                name: "Inspectors");

            migrationBuilder.DropTable(
                name: "Ministermonitorings");

            migrationBuilder.DropTable(
                name: "ImportReports");

            migrationBuilder.DropTable(
                name: "CentralPolicyDateProvinces");

            migrationBuilder.DropTable(
                name: "SubquestionCentralPolicyProvinces");

            migrationBuilder.DropTable(
                name: "Subquestions");

            migrationBuilder.DropTable(
                name: "TrainingLecturers");

            migrationBuilder.DropTable(
                name: "TrainingPrograms");

            migrationBuilder.DropTable(
                name: "TrainingConditions");

            migrationBuilder.DropTable(
                name: "TrainingRegisters");

            migrationBuilder.DropTable(
                name: "TrainingSurveys");

            migrationBuilder.DropTable(
                name: "CentralPolicyEvents");

            migrationBuilder.DropTable(
                name: "ExecutiveOrderAnswers");

            migrationBuilder.DropTable(
                name: "RequestOrderAnswers");

            migrationBuilder.DropTable(
                name: "ElectronicBooks");

            migrationBuilder.DropTable(
                name: "FiscalYear");

            migrationBuilder.DropTable(
                name: "SubjectCentralPolicyProvinces");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "TrainingPhases");

            migrationBuilder.DropTable(
                name: "TrainingSurveyTopics");

            migrationBuilder.DropTable(
                name: "ExecutiveOrders");

            migrationBuilder.DropTable(
                name: "RequestOrders");

            migrationBuilder.DropTable(
                name: "CentralPolicyProvinces");

            migrationBuilder.DropTable(
                name: "SubjectGroups");

            migrationBuilder.DropTable(
                name: "Trainings");

            migrationBuilder.DropTable(
                name: "CentralPolicies");

            migrationBuilder.DropTable(
                name: "FiscalYearNew");

            migrationBuilder.DropTable(
                name: "InspectionPlanEvents");

            migrationBuilder.DropTable(
                name: "Typeexaminationplans");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ProvincialDepartments");

            migrationBuilder.DropTable(
                name: "Side");

            migrationBuilder.DropTable(
                name: "Subdistricts");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Ministries");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropTable(
                name: "ProvincesGroup");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Sector");
        }
    }
}
