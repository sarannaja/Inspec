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
    [Table("Trainings")]
    [Description("ตารางหลักสูตรการอบรม")]
    public class Training
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [Required]
        [Description("ชื่อหลักสูตรการอบรม")]
        public string Name { get; set; }

        [Required]
        [Description("คำอธิบายหลักสูตรการอบรม")]
        public string Detail { get; set; }

        [Description("วันที่เริ่ม")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Description("วันที่สิ้นสุด")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        [Description("ชื่อวิทยากร")]
        public string LecturerName { get; set; }

        [Description("วันที่เริ่มสมัคร")]
        [DataType(DataType.Date)]
        public DateTime RegisStartDate { get; set; }

        [Description("วันที่สิ้นสุดสมัคร")]
        [DataType(DataType.Date)]
        public DateTime RegisEndDate { get; set; }

        [Required]
        [Description("รูปภาพ")]
        public string Image { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

    }
}