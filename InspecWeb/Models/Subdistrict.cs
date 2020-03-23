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
    [Table("Subdistricts")]
    [Description("ตารางตำบล/แขวง")]
    public class Subdistrict
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("District")]
        [Description("FK: อำเภอ/เขต")]
        public long DistrictId { get; set; }

        public virtual District District { get; set; }

        [Required]
        [Description("ชื่อตำบล/แขวง")]
        public string Name { get; set; }

        public ICollection<Village> Village { get; set; }

    }
}
