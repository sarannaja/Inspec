using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("Departments")]
    [Description("ตารางกรม/หน่วยงาน")]
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        /* กระทรวง */
        [ForeignKey("Ministry")]
        [Description("FK: กระทรวง")]
        public long MinistryId { get; set; }
        public virtual Ministry Ministries { get; set; }

        [Required]
        [Description("ชื่อกรม/หน่วยงาน")]
        public string Name { get; set; }

        [Description("ชื่อกรมแบบย่อ")]
        public string ShortnameTH { get; set; }

        [Required]
        [Description("ชื่อกรม/หน่วยงานภาษาอังกฤษ")]
        public string NameEN { get; set; }

        [Description("ชื่อกรม/หน่วยงานภาษาอังกฤษแบบย่อ")]
        public string ShortnameEN { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }
    }
}
