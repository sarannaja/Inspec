using System;
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

        [Description("รหัสบุคคล")]
        public string UserId { get; set; }

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

        [Description("กลุ่ม 1")]
        public long Group1 { get; set; }

        [Description("กลุ่ม 2")]
        public long Group2 { get; set; }

        [Description("กลุ่ม 3")]
        public long Group3 { get; set; }

        [Description("กลุ่ม 4")]
        public long Group4 { get; set; }

        [Description("กลุ่ม 5")]
        public long Group5 { get; set; }

        [Description("กลุ่ม 6")]
        public long Group6 { get; set; }

        [Description("กลุ่ม 7")]
        public long Group7 { get; set; }

        [Description("กลุ่ม 8")]
        public long Group8 { get; set; }

        [Description("กลุ่ม 9")]
        public long Group9 { get; set; }

        [Description("กลุ่ม 10")]
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

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }
        public ICollection<TrainingRegisterCondition> TrainingRegisterConditions { get; set; }
    }
}