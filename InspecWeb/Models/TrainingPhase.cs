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
    [Table("TrainingPhases")]
    [Description("ตารางช่วงหลักสูตรการอบรม")]
    public class TrainingPhase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("Training")]
        [Description("FK: หลักสูตรอบรม")]
        public long TrainingId { get; set; }
        public virtual Training Training { get; set; }

        [Required]
        [Description("ช่วงหลักสูตร")]
        public long PhaseNo { get; set; }

        [Required]
        [Description("คำอธิบายช่วงหลักสูตรการอบรม")]
        public string Detail { get; set; }

        [Description("วันที่เริ่ม")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Description("วันที่สิ้นสุด")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Description("สถานที่จัดหลัก")]
        public string Location { get; set; }

        [Description("จำนวนกลุ่ม")]
        public long Group { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

    }
}