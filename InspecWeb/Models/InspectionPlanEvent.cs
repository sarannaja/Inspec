using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("InspectionPlanEvents")]
    [Description("ตารางประเด็น")]
    public class InspectionPlanEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("CentralPolicy")]
        [Description("FK: นโยบายกลาง")]
        public long CentralPolicyId { get; set; }

        public virtual CentralPolicy CentralPolicy { get; set; }

        [Required]
        [Description("ชื่อ Event")]
        public string Name { get; set; }

        [Description("วันที่เริ่ม")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Description("วันที่สิ้นสุด")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

        [Required]
        [Description("คนที่สร้าง Event")]
        public string CreatedBy { get; set; }
    }
}
