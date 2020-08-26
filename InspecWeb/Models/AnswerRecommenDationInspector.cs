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
    [Table("AnswerRecommenDationInspectors")]
    [Description("ตารางตอบข้อเสนอแนะ")]
    public class AnswerRecommenDationInspector
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("SubjectGroup")]
        [Description("FK:")]
        public long SubjectGroupId { get; set; }
        public virtual SubjectGroup SubjectGroup { get; set; }


        [ForeignKey("User")]
        [Description("FK: User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        [Description("ตอบข้อเสนอแนะ")]
        public string Answersuggestion { get; set; }

        [Required]
        [Description("สถานะ")]
        public string Status { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }
    }
}