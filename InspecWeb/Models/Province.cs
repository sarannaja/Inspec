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
    [Table("Provinces")]
    [Description("ตารางจังหวัด")]
    public class Province
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("Sector")]
        [Description("FK: ภาค")]
        public long SectorId { get; set; }

        public virtual Sector Sectors { get; set; }

        [ForeignKey("ProvincesGroup")]
        [Description("FK: กลุ่มจังหวัด")]
        public long ProvincesGroupId { get; set; }

        public virtual ProvincesGroup ProvincesGroups { get; set; }

        [Required]
        [Description("ชื่อจังหวัด")]
        public string Name { get; set; }

        [Required]
        [Description("ลิ้งค์")]
        public string Link { get; set; }


        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

        //public ICollection<CentralPolicyProvince> CentralPolicyProvinces { get; set; }
    }
}