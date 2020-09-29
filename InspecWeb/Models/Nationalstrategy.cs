using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InspecWeb.Models
{
    [Table("Nationalstrategies")]
    [Description("ยุทธศาสตร์ชาติ")]
    public class Nationalstrategy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [Required]
        [Description("หัวเรื่อง")]
        public string Title { get; set; }

        [Required]
        [Description("ชื่อไฟล์")]
        public string File { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }


    }
}
