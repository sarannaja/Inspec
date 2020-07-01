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
    [Table("TrainingConditions")]
    [Description("ตารางกำหนดเงื่อนไขคุณสมบัติสมัครอบรม")]
    public class TrainingCondition
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
        [Description("เงื่อนไขคุณสมบัติ")]
        public string Name { get; set; }

        [Description("อายุเริ่ม")]
        public int StartYear { get; set; }

        [Description("อายุถึง")]
        public int EndYear { get; set; }

        [Description("ประเภท")]
        public int ConditionType { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

    }
}