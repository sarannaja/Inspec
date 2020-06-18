using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    // <summary>
    /// เนื้อหาของสื่อสิ่งพิมพ์
    // </summary>
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
        public virtual Training Training { get; set; }

        [Description("ประเภทบุคคล")]
        public long UserType { get; set; }

        [Description("รหัสบุคคล")]
        public string UserId { get; set; }

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


        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

    }
}