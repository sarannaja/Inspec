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
    [Table("TrainingSurveyAnswers")]
    [Description("ตารางตอบแบบประเมินการอบรม")]
    public class TrainingSurveyAnswer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("TrainingSurvey")]
        [Description("FK: หัวข้อประเมิน")]
        public long TrainingSurveyId { get; set; }
        public virtual TrainingSurvey TrainingSurvey { get; set; }

        [Description("Username")]
        public string Username { get; set; }

        [Description("รหัสประจำตัวผู้อบรม")]
        public string IDCode { get; set; }

        [Required]
        [Description("ชื่อ-นามสกุล")]
        public string Name { get; set; }

        [Description("ตำแหน่ง")]
        public string Posoition { get; set; }

        [Description("คะแนน")]
        public int Score { get; set; }

        [Description("ปลายเปิด")]
        public string AnswerText { get; set; }

        [Description("ใช่ หรือ ไม่")]
        public int AnswerYorN { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

    }
}