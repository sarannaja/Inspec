using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InspecWeb.Models
{
    [Table("CentralPolicyProvinceEvents")]
    [Description("ตาราง")]
    public class CentralPolicyProvinceEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("CentralPolicyProvince")]
        [Description("FK: นโยบายกลาง")]
        public long CentralPolicyProvinceId { get; set; }
        public virtual CentralPolicyProvince CentralPolicyProvince { get; set; }

        [ForeignKey("InspectionPlanEvent")]
        [Description("FK: InspectionPlanEvent")]
        public long InspectionPlanEventId { get; set; }
        public virtual InspectionPlanEvent InspectionPlanEvent { get; set; }
    }
}
