using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        // [ForeignKey("ElectronicBookGroup")]
        // [Description("FK: ElectronicBookGroup")]
        // public long ElectronicBookGroupId { get; set; }
        // public virtual ElectronicBookGroup ElectronicBookGroup { get; set; }

        [ForeignKey("ElectronicBook")]
        [Description("FK: ElectronicBook")]
        public long ElectronicBookId { get; set; }
        public virtual ElectronicBook ElectronicBook { get; set; }

        [ForeignKey("User")]
        [Description("FK: User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

        [ForeignKey("Province")]
        [Description("FK: จังหวัด")]
        public long ProvinceId { get; set; }
        public virtual Province Province { get; set; }

        [Description("คำอธิบาย")]
        public string Description { get; set; }

        [Description("สถานะ")]
        public string Status { get; set; }

        [ForeignKey("UserCreate")]
        [Description("FK: UserCreate")]
        public string CreateBy { get; set; }
        public virtual ApplicationUser UserCreate { get; set; }

        // [Required]
        public ICollection<ElectronicBookProvinceApproveFile> ElectronicBookProvinceApproveFiles { get; set; }
    }
}
