using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace InspecWeb.Models
{
    [Table("ProvincesGroup")]
    [Description("ตารางกลุ่มจังหวัด")]

    public class ProvincesGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [Required]
        [Description("ชื่อกลุ่มจังหวัด")]
        public string Name { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

      
        public ICollection<Province> Provinces { get; set; }
    
    }
}
