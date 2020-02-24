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
    [Table("Ministermonitorings")]
    [Description("ตารางรองนายกรัฐมนตรีและรัฐมนตรีประจำสำนักนายกรัฐมนตรีในการกำกับและติดตามการปฏฺบัติราชการในภูมิภาค")]
    public class Ministermonitoring
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [Required]
        [Description("ชื่อ")]
        public string Name { get; set; }

        [Required]
        [Description("ตำแหน่ง")]
        public string Position { get; set; }

        [Required]
        [Description("รูปภาพ")]
        public string Image { get; set; }


        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }



        public ICollection<MinistermonitoringRegion> MinistermonitoringRegions { get; set; }
    }
}
