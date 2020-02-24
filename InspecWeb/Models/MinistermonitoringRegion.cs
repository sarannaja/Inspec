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
    [Table("MinistermonitoringRegions")]
    [Description("ตารางรองนายกรัฐมนตรีและรัฐมนตรีประจำสำนักนายกรัฐมนตรีในการกำกับและติดตามการปฏฺบัติราชการในภูมิภาคเขตตรวจราชการ")]
    public class MinistermonitoringRegion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }


        [ForeignKey("Ministermonitoring")]
        [Description("FK: รองนายกรัฐมนตรีและรัฐมนตรีประจำสำนักนายกรัฐมนตรีในการกำกับและติดตามการปฏฺบัติราชการในภูมิภาค")]
        public long MinistermonitoringId { get; set; }

        public virtual Ministermonitoring Ministermonitoring { get; set; }
       

        [ForeignKey("Region")]
        [Description("FK: เขตตรวจ")]
        public long RegionId { get; set; }

        public virtual Region Region { get; set; }




    }
}