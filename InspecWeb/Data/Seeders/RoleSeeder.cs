using InspecWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InspecWeb.Data.Seeders
{
    public class RoleSeeder : IEntityTypeConfiguration<Role>
    {

        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
            new Role { Id = 1, NameTH = "ผู้ดูแลระบบ", NameEN = "superAdmin" },
            new Role { Id = 2, NameTH = "ผู้ดูแลแผนการตรวจราชการ", NameEN = "Centraladmin" },
            new Role { Id = 3, NameTH = "ผู้ตรวจราชการสำนักนายกรัฐมนตรี", NameEN = "Inspector" },
            new Role { Id = 4, NameTH = "ผู้ว่าราชการจังหวัด", NameEN = "Provincialgovernor" },
            new Role { Id = 5, NameTH = "หัวหน้าสำนักงานจังหวัด", NameEN = "Adminprovince" },
            new Role { Id = 6, NameTH = "ผู้ตรวจราชการกระทรวง", NameEN = "InspectorMinistry" },
            new Role { Id = 7, NameTH = "ที่ปรึกษาผู้ตรวจราชการภาคประชาชน", NameEN = "publicsector" },
            new Role { Id = 8, NameTH = "ผู้บริหาร/นายก/รองนายก", NameEN = "president" },
            new Role { Id = 9, NameTH = "หน่วยงานตรวจ", NameEN = "InspectorExamination" },
            new Role { Id = 10, NameTH = "ผู้ตรวจราชการกรม", NameEN = "InspectorDepartment" },
            new Role { Id = 11, NameTH = "บุคคลภายนอก", NameEN = "External" }
            );
        }
    }
}