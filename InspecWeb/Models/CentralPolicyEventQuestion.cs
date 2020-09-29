using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InspecWeb.Models
{
    [Table("CentralPolicyEventQuestions")]
    [Description("ตารางรองความสัมพันธ์ระหว่างนโยบายกลางและ Event")]
    public class CentralPolicyEventQuestion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("CentralPolicyEvent")]
        [Description("FK: นโยบายกลาง")]
        public long CentralPolicyEventId { get; set; }
        public virtual CentralPolicyEvent CentralPolicyEvent { get; set; }

        [Description("")]
        public string QuestionPeople { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? NotificationDate { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? DeadlineDate { get; set; }

        public ICollection<AnswerCentralPolicyProvince> AnswerCentralPolicyProvinces { get; set; }
    }
}
