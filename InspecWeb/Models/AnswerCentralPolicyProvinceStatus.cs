using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InspecWeb.Models
{
    /// <summary>
    /// เนื้อหาของสื่อสิ่งพิมพ์
    /// </summary>
    [Table("AnswerCentralPolicyProvinceStatuses")]
    [Description("ตารางสถานะตอบคำถาม")]
    public class AnswerCentralPolicyProvinceStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("CentralPolicyEvent")]
        [Description("")]
        public long CentralPolicyEventId { get; set; }
        public virtual CentralPolicyEvent CentralPolicyEvent { get; set; }

        [ForeignKey("User")]
        [Description("FK: User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        [Description("สถานะ")]
        public string Status { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }
    }
}