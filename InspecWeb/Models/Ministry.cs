using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

        /* กรม */
        public ICollection<Department> Departments { get; set; }

        //public ICollection<ApplicationUser> ApplicationUsers { get; set; }
        
    }
}
