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
    [Table("AnswerSubquestions")]
    [Description("ตารางตอบคำถาม")]
    public class AnswerSubquestion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("SubquestionCentralPolicyProvince")]
        [Description("FK: คำถามย่อย")]
        public long SubquestionCentralPolicyProvinceId { get; set; }

        public virtual SubquestionCentralPolicyProvince SubquestionCentralPolicyProvince { get; set; }

        [ForeignKey("AnswerSubquestionStatus")]
        [Description("FK:")]
        public long AnswerSubquestionStatusId { get; set; }

        public virtual AnswerSubquestionStatus AnswerSubquestionStatus { get; set; }

        [ForeignKey("User")]
        [Description("FK: User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        [Description("คำตอบ")]
        public string Answer { get; set; }

        [Required]
        [Description("คำอธิบาย")]
        public string Description { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }
    }
}