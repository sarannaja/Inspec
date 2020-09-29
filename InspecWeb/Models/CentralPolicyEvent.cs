using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InspecWeb.Models
{
    [Table("CentralPolicyEvents")]
    [Description("ตารางรองความสัมพันธ์ระหว่างนโยบายกลางและ Event")]
    public class CentralPolicyEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("CentralPolicy")]
        [Description("FK: นโยบายกลาง")]
        public long CentralPolicyId { get; set; }
        public virtual CentralPolicy CentralPolicy { get; set; }

        [ForeignKey("InspectionPlanEvent")]
        [Description("FK: Event การตรวจ")]
        public long InspectionPlanEventId { get; set; }
        public virtual InspectionPlanEvent InspectionPlanEvent { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }


        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? NotificationDate { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? DeadlineDate { get; set; }

        public long HaveSubject { get; set; }

        public ICollection<CentralPolicyEventQuestion> CentralPolicyEventQuestions { get; set; }

        public ICollection<SubjectGroupPeopleQuestion> SubjectGroupPeopleQuestions { get; set; }
        //public ICollection<AnswerCentralPolicyProvince> AnswerCentralPolicyProvinces { get; set; }
        //[ForeignKey("ElectronicBook")]
        //[Description("FK: Event การตรวจ")]
        //public long ElectronicBookId { get; set; }
        //public virtual ElectronicBook ElectronicBook { get; set; }
    }
}
