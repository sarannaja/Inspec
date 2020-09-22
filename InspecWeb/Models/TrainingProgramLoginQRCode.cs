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
    [Table("TrainingProgramLoginQRCodes")]
    [Description("ตารางช่วงเวลาอบรมหลักสูตรออก QRCode")]
    public class TrainingProgramLoginQRCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [Description("วันที่อบรม")]
        public string ProgramDate { get; set; }

        [Description("ช่วงเช้า")]
        public long Morning { get; set; }

        [Description("ช่วงบ่าย")]
        public long Afternoon { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

        [Description("วันที่อัพเดท")]
        [DataType(DataType.Date)]
        public DateTime? UpdatedAt { get; set; }

    }
}