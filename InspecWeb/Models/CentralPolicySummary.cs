using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InspecWeb.Models
{
    public class CentralPolicySummary
    {
        [Key]
        public long Id { get; set; }

        [Description("FK: นโยบายส่วนกลาง")]
        public long CentralPolicyId { get; set; }

        [ForeignKey("CentralPolicyId")]
        public virtual CentralPolicy CentralPolicy { get; set; }

        [Description("ผู้สร้าง")]
        public string CreatedBy { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual ApplicationUser CreatedByUser { get; set; }

        [Description("วันที่สร้าง")]
        public DateTime? CreatedAt { get; set; }

        [Description("ผู้แก้ไข")]
        public string UpdatedBy { get; set; }

        [ForeignKey("UpdatedBy")]
        public virtual ApplicationUser UpdatedByUser { get; set; }

        [Description("วันที่แก้ไข")]
        public DateTime? UpdatedAt { get; set; }

        [Description("รายละเอียด")]
        public string Detail { get; set; }
    }
}