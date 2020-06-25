using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    public class ApplicationUser : IdentityUser
    {
        //20
        [Description("สิทธิ์การใช้งาน")]
        public long Role_id { get; set; }

        [Description("ตำแหน่ง")]
        public string Position { get; set; }

        [Description("คำนำหน้า")]
        public string Prefix { get; set; }

      
        [Description("ชื่อ")]
        public string Name { get; set; }
      
        
        [Description("การศึกษา")]
        public string Educational { get; set; }

        [Description("วันเกิด")]
        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        [Description("เบอร์โทรศัพท์ที่ทำงาน")]
        public string Officephonenumber { get; set; }

      
        [Description("เบอร์โทรเลข")]
        public string Telegraphnumber { get; set; }

        [Description("ด้านการทำงาน")]
        public string Side { get; set; }

        [ForeignKey("Ministry")]
        [Description("FK: กระทรวง")]
        public long MinistryId { get; set; }
        public virtual Ministry Ministries { get; set; }

        [ForeignKey("Department")]
        [Description("FK: กรมหน่วยงาน")]
        public long DepartmentId { get; set; }
        public virtual Department Departments { get; set; }

        // ***
        //เพื่มเมื่อ 21-06-2020
        [ForeignKey(" ProvincialDepartment")]
        [Description("FK: หน่วยงานส่วนภูมิถาค")]
        public long ProvincialDepartmentId { get; set; }
        public virtual ProvincialDepartment ProvincialDepartments { get; set; }
        // ***

        [ForeignKey("Province")]
        [Description("FK: จังหวัด")]
        public long ProvinceId { get; set; }
        public virtual Province Province { get; set; }

        [ForeignKey("District")]
        [Description("FK: อำเภอ")]
        public long DistrictId { get; set; }
        public virtual District District { get; set; }

        [ForeignKey("Subdistrict")]
        [Description("FK: ตำบล")]
        public long SubdistrictId { get; set; }
        public virtual Subdistrict Subdistricts { get; set; }
        
        [Description("บ้านเลขที่")]
        public string Housenumber { get; set; }

        [Description("ถนน")]
        public string Rold { get; set; }

        [Description("ซอย")]
        public string Alley { get; set; }
   
        [Description("รหัสไปรษณี")]
        public string Postalcode { get; set; }

        [Description("รูปโปรไฟล์")]
        public string Img { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

        [Description("วันที่เริ่ม")]
        [DataType(DataType.Date)]
        public DateTime? Startdate { get; set; }

        [Description("วันที่สินสุด")]
        [DataType(DataType.Date)]
        public DateTime? Enddate { get; set; }

        [Description("การใช้งาน")]
        public long Active { get; set; }

        public ICollection<UserRegion> UserRegion { get; set; }
        public ICollection<CentralPolicyUser> CentralPolicyUser { get; set; }

        public ICollection<UserProvince> UserProvince { get; set; }

        public ICollection<Notification> notification { get; set; } // tb แจ้งเตือน


    }

    public class UserArray
    {
        public ApplicationUser[] user { get; set; }
    }
}
