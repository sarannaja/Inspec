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
    [Table("Village")]
    [Description("ตารางหมู่บ้าน")]
    public class Village
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("Subdistrict")]
        [Description("FK: ตำบล/แขวง")]
        public long SubdistrictId { get; set; }

        public virtual Subdistrict Subdistrict { get; set; }

        [Description("หมู่ที่")]
        public string No { get; set; }

        [Required]
        [Description("ชื่อหมู่บ้าน")]
        public string Name { get; set; }
    }
}
