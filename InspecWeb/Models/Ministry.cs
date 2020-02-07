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

        [Required]
        [Description("วันที่สร้าง")]
        public DateTime CreatedAt { get; set; }

        //public ICollection<District> Districts { get; set; }
    }
}
