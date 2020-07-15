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
    [Table("TrainingRegisterConditions")]
    [Description("ตารางผู้สมัครหลักสูตรการอบรม")]
    public class TrainingRegisterCondition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("TrainingRegister")]
        [Description("FK: ผู้สมัคร")]
        public long RegisterId { get; set; }
        public virtual TrainingRegister TrainingRegister { get; set; }

        [ForeignKey("TrainingCondition")]
        [Description("FK: ผู้สมัคร")]
        public long ConditionId { get; set; }
        public virtual TrainingCondition TrainingCondition { get; set; }
        public long Status { get; set; }

    }
}