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
    [Table("AnswerSubquestionFiles")]
    [Description("ตารางตอบคำถาม")]
    public class AnswerSubquestionFile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("SubjectCentralPolicyProvince")]
        [Description("FK: นโยบายกลาง")]
        public long SubjectCentralPolicyProvinceId { get; set; }

        public virtual SubjectCentralPolicyProvince SubjectCentralPolicyProvince { get; set; }

        [Required]
        [Description("ชื่อไฟล์")]
        public string Name { get; set; }

        [Description("ประเภท")]
        public string Type { get; set; }

        [Description("คำอธิบายรูปภาพ")]
        public string Description { get; set; }
    }
}