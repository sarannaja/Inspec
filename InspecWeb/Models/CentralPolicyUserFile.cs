using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("CentralPolicyUserFiles")]
    [Description("ตารางไฟล์นโยบาลกลาง")]
    public class CentralPolicyUserFile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("CentralPolicyGroup")]
        [Description("FK: นโยบายกลาง")]
        public long CentralPolicyGroupId { get; set; }

        public virtual CentralPolicyGroup CentralPolicyGroup { get; set; }

        [Required]
        [Description("ชื่อไฟล์")]
        public string Name { get; set; }

        [Description("ประเภท")]
        public string Type { get; set; }

        [Description("คำอธิบายรูปภาพ")]
        public string Description { get; set; }

    }
}
