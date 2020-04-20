using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("ElectronicBooks")]
    [Description("ตารางเชื่อมนโยบาลกลาง")]
    public class ElectronicBook
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("User")]
        [Description("FK: User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Required]
        [Description("รายละเอียด")]
        public string Detail { get; set; }
        //public ICollection<CentralPolicyUser> CentralPolicyUsers { get; set; }
    }
}
