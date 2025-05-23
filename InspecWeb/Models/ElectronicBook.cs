﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("ElectronicBooks")]
    [Description("ตารางเชื่อมนโยบาลกลาง")]
    public class ElectronicBook
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }


        //[ForeignKey("SubjectCentralPolicyProvince")]
        //[Description("FK: SubjectCentralPolicyProvince")]
        //public long SubjectCentralPolicyProvinceId { get; set; }
        //public virtual SubjectCentralPolicyProvince SubjectCentralPolicyProvince { get; set; }

        [ForeignKey("User")]
        [Description("FK: User")]
        public string CreatedBy { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Description("ผลการจตรวจ")]
        public string Detail { get; set; }
        //public ICollection<CentralPolicyUser> CentralPolicyUsers { get; set; }

        [Description("ปัญหา")]
        public string Problem { get; set; }


        [Description("คำแนะนำ")]
        public string Suggestion { get; set; }

        [Required]
        [Description("สถานะ")]
        public string Status { get; set; }

        [Description("วันที่เริ่ม")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Description("วันที่สิ้นสุด")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Description("สถานะจังหวัด")]
        public string ProvinceStatus { get; set; }
        public ICollection<ElectronicBookFile> ElectronicBookFiles { get; set; }
        public ICollection<ElectronicBookSuggestGroup> ElectronicBookSuggestGroups { get; set; }
        public ICollection<ElectronicBookGroup> ElectronicBookGroups { get; set; }

        public ICollection<ElectronicBookInvite> ElectronicBookInvites { get; set; }

        public ICollection<ElectronicBookAccept> ElectronicBookAccepts { get; set; }

        // Test create own
        [Description("ประเภทสมุดตรวจ")]
        public string ElectronicBookType { get; set; }

        [Description("หน่วยรับตรวจ")]
        public long ProvincialDepartmentId { get; set; }

        [Description("ประเด็นนโยบาย")]
        public string CentralPolicy { get; set; }

        [Description("จังหวัดต้องรับทราบมั้ย")]
        public string ProvinceType { get; set; }

        public ICollection<ElectronicBookProvincialDepartment> ElectronicBookProvincialDepartments { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }
    }
}
