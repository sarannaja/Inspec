using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    /// <summary>
    /// เนื้อหาของสื่อสิ่งพิมพ์
    /// </summary>
    [Table("Regions")]
    [Description("ตารางเขตตรวจ")]
    public class Region
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [Required]
        [Description("เขตตรวจราชการ")]
        public string Name { get; set; }

        [Required]
        [Description("วันที่สร้าง")]
        public DateTime CreatedAt { get; set; }

        
    }
}