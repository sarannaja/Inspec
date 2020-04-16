using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("CentralPolicyGroups")]
    [Description("ตารางเชื่อมนโยบาลกลาง")]
    public class CentralPolicyGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        public ICollection<CentralPolicyUser> CentralPolicyUsers { get; set; }
        public ICollection<CentralPolicyUserFile> CentralPolicyUserFiles { get; set; }
    }
}
