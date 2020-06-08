using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("ElectronicBookAccepts")]
    [Description("ตารางเชื่อมนโยบาลกลาง")]
    public class ElectronicBookAccept
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("ElectronicBookGroup")]
        [Description("FK: ElectronicBookGroup")]
        public long ElectronicBookGroupId { get; set; }
        public virtual ElectronicBookGroup ElectronicBookGroup { get; set; }

        [ForeignKey("User")]
        [Description("FK: User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
