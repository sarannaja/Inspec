using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InspecWeb.Models
{
    [Table("CentralDepartments")]
    [Description("หน่วยงานราชการส่วนกลางภูมิภาค")]
    public class CentralDepartment
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
        [Description("หน่วยงานราชการส่วนกลางภูมิภาค")]
        public string Name { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

        //public ICollection<ProvincialDepartmentProvince> ProvincialDepartmentProvince { get; set; }
    }
}
