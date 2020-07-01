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
    [Table("TrainingRegisterGroups")]
    [Description("ตารางจัดกลุ่มผู้สมัครตามช่วงแผนกิจกรรม")]
    public class TrainingRegisterGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("TrainingRegister")]
        [Description("FK: ผู้สมัคร")]
        public long RegisterId { get; set; }

        [ForeignKey("TrainingPhase")]
        [Description("FK: ช่วงกำหนดการ")]
        public long TrainingPhaseId { get; set; }

        public virtual TrainingRegister TrainingRegister { get; set; }

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

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

    }
}