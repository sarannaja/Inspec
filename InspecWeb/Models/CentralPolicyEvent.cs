using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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
    }
}
