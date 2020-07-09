using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InspecWeb.Models
{
    [Table("ElectronicBookProvinceApproveFiles")]
    [Description("ตารางไฟล์สมุดตรวจ")]
    public class ElectronicBookProvinceApproveFile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("ElectronicBookAcceptId")]
        [Description("FK: สมุดตรวจ")]
        public long ElectronicBookAcceptId { get; set; }
        public virtual ElectronicBookAccept ElectronicBookAccept { get; set; }

        [Required]
        [Description("ชื่อไฟล์")]
        public string Name { get; set; }
    }
}
