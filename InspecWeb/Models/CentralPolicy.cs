﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("CentralPolicies")]
    [Description("ตารางนโยบายกลาง")]
    public class CentralPolicy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("FiscalYearNew")]
        [Description("FK: ปีงบประมาณ")]
        public long FiscalYearNewId { get; set; }

        public virtual FiscalYearNew FiscalYearNew { get; set; }

        [ForeignKey("Typeexaminationplan")]
        [Description("FK: ประเภทแผนการตรวจ")]
        public long TypeexaminationplanId { get; set; }

        public virtual Typeexaminationplan Typeexaminationplan { get; set; }

        //[ForeignKey("InspectionPlanEvent")]
        //[Description("FK: Event การตรวจ")]
        //public long InspectionPlanEventId { get; set; }
        //public virtual InspectionPlanEvent InspectionPlanEvent { get; set; }

        [Required]
        [Description("ชื่อนโยบายกลาง")]
        public string Title { get; set; }

        [Description("วันที่เริ่ม")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Description("วันที่สิ้นสุด")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

        [ForeignKey("User")]
        [Description("คนที่สร้างนโยบายกลาง")]
        public string CreatedBy { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Required]
        [Description("สถานะ")]
        public string Status { get; set; }

        //[Required]
        //[Description("ประเภท")]
        //public string Type { get; set; }

        [Required]
        [Description("ประเภทหลัก 1.CentralPolicy 2.Inspectionplan")]
        public string Class { get; set; }

        [Description("วันที่แก้ไข")]
        [DataType(DataType.Date)]
        public DateTime? UpdateAt { get; set; }

        public ICollection<Subject> Subjects { get; set; }
        public ICollection<CentralPolicyFile> CentralPolicyFiles { get; set; }
        public ICollection<CentralPolicyUser> CentralPolicyUser { get; set; }
        public ICollection<CentralPolicyDate> CentralPolicyDates { get; set; }
        public ICollection<CentralPolicyProvince> CentralPolicyProvinces { get; set; }
        public ICollection<SubjectGroup> SubjectGroups { get; set; }
        public ICollection<CentralPolicyEvent> CentralPolicyEvents { get; set; }
        //public ICollection<ExecutiveOrder> ExecutiveOrders { get; set; }
        //public ICollection<RequestOrder> RequestOrders { get; set; }
    }
}
