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
    [Table("Districts")]
    [Description("ตารางอำเภอ")]
    public class District
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("Province")]
        [Description("FK: จังหวัด")]
        public long ProvinceId { get; set; }

        public virtual Province Province { get; set; }

        [Required]
        [Description("ชื่ออำเภอ")]
        public string Name { get; set; }
    }
}
