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
    [Description("ตารางอำเภอ/เขต")]
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
        [Description("ชื่ออำเภอ/เขต")]
        public string Name { get; set; }

        //public ICollection<Subdistrict> Subdistricts { get; set; }
        // public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
