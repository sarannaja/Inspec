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
    [Table("AnswerCentralPolicyProvinces")]
    [Description("ตารางตอบคำถาม")]
    public class AnswerCentralPolicyProvince
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("CentralPolicyProvince")]
        [Description("FK: นโยบายกลาง")]
        public long CentralPolicyProvinceId { get; set; }
        public virtual CentralPolicyProvince CentralPolicyProvince { get; set; }

        [ForeignKey("CentralPolicyEventQuestion")]
        [Description("")]
        public long CentralPolicyEventQuestionId { get; set; }
        public virtual CentralPolicyEventQuestion CentralPolicyEventQuestion { get; set; }

        [ForeignKey("AnswerCentralPolicyProvinceStatus")]
        [Description("FK:")]
        public long AnswerCentralPolicyProvinceStatusId { get; set; }
        public virtual AnswerCentralPolicyProvinceStatus AnswerCentralPolicyProvinceStatus { get; set; }

        [ForeignKey("User")]
        [Description("FK: User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        [Description("คำตอบ")]
        public string Answer { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }
    }
}