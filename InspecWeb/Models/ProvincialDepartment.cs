using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("ProvincialDepartments")]
    [Description("หน่วยงานส่วนภูมิถาค")]
    public class ProvincialDepartment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        /* กระทรวง */
        [ForeignKey("Department")]
        [Description("FK: กรม")]
        public long DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [Required]
        [Description("ชื่อหน่วยงานส่วนภูมิถาค")]
        public string Name { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

        // public ICollection<ProvincialDepartmentProvince> ProvincialDepartmentProvince { get; set; }
    }
}
