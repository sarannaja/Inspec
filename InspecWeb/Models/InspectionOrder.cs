using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("InspectionOrders")]
    [Description("")]
    public class InspectionOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [Required]
        [Description("ปี")]
        public string Year { get; set; }
      
        [Required]
        [Description("คำสั่ง")]
        public string Order { get; set; }

        [Required]
        [Description("เรื่อง")]
        public string Name { get; set; }

        [Required]
        [Description("ของใคร")]
        public string CreateBy { get; set; }

        [Required]
        [Description("ไฟล์แนบ")]
        public string? File { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }
    }
}
