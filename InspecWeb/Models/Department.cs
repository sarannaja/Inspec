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
        public Ministry Ministry { get; set; }

        [Required]
        [Description("ชื่อกรม/หน่วยงาน")]
        public string Name { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }
    }
}
