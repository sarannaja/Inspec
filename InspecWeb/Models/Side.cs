using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InspecWeb.Models
{
    [Table("Side")]
    [Description("ด้าน")]
    public class Side
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [Required]
        [Description("ชื่อ")]
        public string Name { get; set; }

        [Description("ชื่อแบบย่อ")]
        public string ShortnameTH { get; set; }

        [Description("ชื่ออังกฤษ")]
        public string NameEN { get; set; }

        [Description("ชื่ออังกฤษแบบย่อ")]
        public string ShortnameEN { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }




    }
}
