using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("CentralPolicyUsers")]
    [Description("ตารางผู้รับผิดชอบกำหนดการตรวจ")]
    public class CentralPolicyUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("CentralPolicy")]
        [Description("FK: แผนการตรวจ")]
        public long CentralPolicyId { get; set; }

        public virtual CentralPolicy CentralPolicy { get; set; }

        [ForeignKey("Province")]
        [Description("FK: จังหวัด")]
        public long ProvinceId { get; set; }
        public virtual Province Province { get; set; }

        [ForeignKey("ElectronicBook")]
        [Description("FK: ElectronicBook")]
        public long ElectronicBookId { get; set; }
        public virtual ElectronicBook ElectronicBook { get; set; }

        [ForeignKey("CentralPolicyGroup")]
        [Description("FK: แผนการตรวจ")]
        public long CentralPolicyGroupId { get; set; }

        public virtual CentralPolicyGroup CentralPolicyGroup { get; set; }

        [ForeignKey("User")]
        [Description("FK: User")]
        public string UserId { get; set; }

        [Required]
        [Description("สถานะ -> 1.ตอบรับ 2.ปฏิเสธ")]
        public string Status { get; set; }

        [Description("สถานะ -> 1.ตอบรับ 2.ปฏิเสธ")]
        public string Report { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
