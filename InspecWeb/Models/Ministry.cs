using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InspecWeb.Models
{
    [Table("Ministries")]
    [Description("ตารางกระทรวง")]
    public class Ministry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [Required]
        [Description("ชื่อกระทรวง")]
        public string Name { get; set; }

        [Description("ชื่อกระทรวงแบบย่อ")]
        public string ShortnameTH { get; set; }

        [Required]
        [Description("ชื่อกระทรวงอังกฤษ")]
        public string NameEN { get; set; }

        [Description("ชื่อกระทรวงอังกฤษแบบย่อ")]
        public string ShortnameEN { get; set; }

        [Description("เรียงรหัสเลข")]
        public long Num { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

        /* กรม */
        public ICollection<Department> Departments { get; set; }

        //public ICollection<ApplicationUser> ApplicationUsers { get; set; }

    }
}
