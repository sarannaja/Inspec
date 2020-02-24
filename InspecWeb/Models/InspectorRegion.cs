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
    [Table("InspectorRegions")]
    [Description("ตารางรองนายกรัฐมนตรีและรัฐมนตรีประจำสำนักนายกรัฐมนตรีในการกำกับและติดตามการปฏฺบัติราชการในภูมิภาคเขตตรวจราชการ")]
    public class InspectorRegion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }


        [ForeignKey("Inspector")]
        [Description("FK: รองนายกรัฐมนตรีและรัฐมนตรีประจำสำนักนายกรัฐมนตรีในการกำกับและติดตามการปฏฺบัติราชการในภูมิภาค")]
        public long InspectorId { get; set; }

        public virtual Inspector Inspector { get; set; }
       

        [ForeignKey("Region")]
        [Description("FK: เขตตรวจ")]
        public long RegionId { get; set; }

        public virtual Region Region { get; set; }




    }
}