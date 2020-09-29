using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InspecWeb.Models
{
    [Table("StatePolicys")]
    [Description("นโยบายรัฐบาล")]
    public class StatePolicy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [Required]
        [Description("คณะรัฐมนตรีที่")]
        public string GangId { get; set; }

        [Required]
        [Description("นาม")]
        public string Name { get; set; }

        [Required]
        [Description("คนที่")]
        public string No { get; set; }

        [Required]
        [Description("สมัยที่")]
        public string RoundNo { get; set; }

        [Description("วันที่เริ่ม")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Description("วันที่สิ้นสุด")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

        [Required]
        [Description("วันแถลงนโยบาย")]
        public DateTime? Date { get; set; }

        [Required]
        [Description("ชื่อไฟล์")]
        public string File { get; set; }

    }
}
