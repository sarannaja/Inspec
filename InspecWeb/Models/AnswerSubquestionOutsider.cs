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
    [Table("AnswerSubquestionOutsiders")]
    [Description("ตารางตอบคำถาม")]
    public class AnswerSubquestionOutsider
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("SubquestionCentralPolicyProvince")]
        [Description("FK: คำถามย่อย")]
        public long SubquestionCentralPolicyProvinceId { get; set; }

        public virtual SubquestionCentralPolicyProvince SubquestionCentralPolicyProvince { get; set; }

        [Required]
        [Description("ชื่อ")]
        public string Name { get; set; }

        [Required]
        [Description("ตำแหน่ง")]
        public string Position { get; set; }

        [Required]
        [Description("เบอร์โทร")]
        public string Phonenumber { get; set; }

        [Required]
        [Description("คำตอบ")]
        public string Answer { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }
    }
}