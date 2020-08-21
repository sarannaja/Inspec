using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("InspectionPlanEvents")]
    [Description("ตารางประเด็น")]
    public class InspectionPlanEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("Province")]
        [Description("FK: จังหวัด")]
        public long ProvinceId { get; set; }
        public virtual Province Province { get; set; }

        [ForeignKey("ProvincialDepartment")]
        [Description("FK: หน่วยงานส่วนภูมิถาค")]
        public long ProvincialDepartmentIdCreatedBy { get; set; }
        public virtual ProvincialDepartment ProvincialDepartment { get; set; }

        [Description("วันที่เริ่ม")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Description("วันที่สิ้นสุด")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

        //[Required]
        //[Description("คนที่สร้าง Event")]
        [ForeignKey("User")]
        [Description("FK: User")]
        public string CreatedBy { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Description("RoleCreatedBy")]
        public string RoleCreatedBy { get; set; }

        [Description("สถานะ")]
        public string Status { get; set; }

        public ICollection<CentralPolicy> CentralPolicies { get; set; }
        public ICollection<CentralPolicyEvent> CentralPolicyEvents { get; set; }
        public ICollection<CentralPolicyUser> CentralPolicyUsers { get; set; }

    }
}
