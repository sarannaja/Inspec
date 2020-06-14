using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("ElectronicBookProceeds")]
    [Description("ตารางเชื่อมนโยบาลกลาง")]
    public class ElectronicBookProceed
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }


        [ForeignKey("User")]
        [Description("FK: User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("ElectronicBook")]
        [Description("FK: ElectronicBook")]
        public long ElectronicBookId { get; set; }
        public virtual ElectronicBook ElectronicBook { get; set; }

        [Description("ผลการดำเนินการ")]
        public string Proceed { get; set; }
    }
}
