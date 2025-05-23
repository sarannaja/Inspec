﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    /// <summary>
    /// เนื้อหาของสื่อสิ่งพิมพ์
    /// </summary>
    [Table("TrainingRegisters")]
    [Description("ตารางผู้สมัครหลักสูตรการอบรม")]
    public class TrainingRegister
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("Training")]
        [Description("FK: หลักสูตรอบรม")]
        public long TrainingId { get; set; }

        [Description("ประเภทบุคคล")]
        public long UserType { get; set; }

        [ForeignKey("User")]
        [Description("รหัสบุคคล")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [ForeignKey(" ProvincialDepartment")]
        [Description("FK: หน่วยงานส่วนภูมิถาค")]
        public long ProvincialDepartmentId { get; set; }
        public virtual ProvincialDepartment ProvincialDepartments { get; set; }

        public string UserName { get; set; }

        [Required]
        [Description("ชื่อ-นามสกุล")]
        public string Name { get; set; }

        [Description("ตำแหน่ง")]
        public string Position { get; set; }

        [Description("ตำแหน่งสุดท้ายก่อนดำรงตำแหน่งปัจจุบัน")]
        public string PositionBefore { get; set; }

        [Description("หน่วยงาน/สังกัด")]
        public string Department { get; set; }

        [Description("เลขประจำตัวประชาชน")]
        public string CardId { get; set; }

        [Description("เบอร์โทรศัพท์")]
        public string Phone { get; set; }

        [Description("Email")]
        public string Email { get; set; }
        
        [Description("สถานะ")]
        public long Status { get; set; }

        [Description("สถานะ")]
        public string PassportStatus { get; set; }

        [Description("พาสปอร์ตหมดอายุ")]
        public DateTime? PassportExpire { get; set; }

        [Description("ที่อยู่ปัจจุบัน")]
        public string Address { get; set; }

        [Description("หมายเลขโทรศัพท์ที่ทำงาน")]
        public string OfficePhone { get; set; }

        [Description("ศาสนา")]
        public string Religion { get; set; }
        
        [Description("อาหาร")]
        public string Food { get; set; }

        [Description("อาหารที่แพ้")]
        public string Foodallergy { get; set; }

        [Description("หมู่โลหิต")]
        public string Blood { get; set; }

        [Description("โรคประจำตัว")]
        public string CongenitalDisease { get; set; }

         [Description("ตำแหน่งผู้ประสาน")]
        public string CollaboratorPosition { get; set; }

      
        [Description("ช่วง 1")]
        public long Group1 { get; set; }

        [Description("ช่วง 2")]
        public long Group2 { get; set; }

        [Description("ช่วง 3")]
        public long Group3 { get; set; }

        [Description("ช่วง 4")]
        public long Group4 { get; set; }

        [Description("ช่วง 5")]
        public long Group5 { get; set; }

        [Description("ช่วง 6")]
        public long Group6 { get; set; }

        [Description("ช่วง 7")]
        public long Group7 { get; set; }

        [Description("ช่วง 8")]
        public long Group8 { get; set; }

        [Description("ช่วง 9")]
        public long Group9 { get; set; }

        [Description("ช่วง 10")]
        public long Group10 { get; set; }

        public string Type { get; set; }
        public string Nickname { get; set; }
        public DateTime? RetiredDate { get; set; }
        public DateTime? BirthDate { get; set; }
        public string OfficeAddress { get; set; }
        public string Fax { get; set; }
        public string CollaboratorName { get; set; }
        public string CollaboratorPhone { get; set; }
        public string CollaboratorPhoneOffice { get; set; }
        public string CollaboratorEmail { get; set; }
        public virtual Training Training { get; set; }

        [Description("รหัสประจำตัวผู้เข้าอบรม")]
        public string IDCode { get; set; }

        [Description("ลำดับผู้เข้าอบรม")]
        public string NoIDCode { get; set; }

        [Description("วันที่สร้างรหัสประจำตัวผู้เข้าอบรม")]
        [DataType(DataType.Date)]
        public DateTime? IDCodeCreatedAt { get; set; }


        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }
        public ICollection<TrainingRegisterCondition> TrainingRegisterConditions { get; set; }
    }
}