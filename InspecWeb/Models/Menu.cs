using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("Menu")]
    [Description("ตารางกระทรวง")]
    public class Menu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        //[ForeignKey("Role")]
        //[Description("Role_id")]
        public long Role_id { get; set; }
       // public virtual Role Roles { get; set; }

        [Required]
        [Description("M")]
        public long M1 { get; set; }

        [Required]
        [Description("M")]
        public long M2 { get; set; }

        [Required]
        [Description("M")]
        public long M3 { get; set; }

        [Required]
        [Description("M")]
        public long M4 { get; set; }

        [Required]
        [Description("M")]
        public long M5 { get; set; }

        [Required]
        [Description("M")]
        public long M6 { get; set; }

        [Required]
        [Description("M")]
        public long M7 { get; set; }

        [Required]
        [Description("M")]
        public long M8 { get; set; }

        [Required]
        [Description("M")]
        public long M9 { get; set; }

        [Required]
        [Description("M")]
        public long M10 { get; set; }

        [Required]
        [Description("M")]
        public long M11 { get; set; }

        [Required]
        [Description("M")]
        public long M12 { get; set; }

        [Required]
        [Description("M")]
        public long M13 { get; set; }

        [Required]
        [Description("M")]
        public long M14 { get; set; }

        [Required]
        [Description("M")]
        public long M15 { get; set; }

        [Required]
        [Description("M")]
        public long M16 { get; set; }

        [Required]
        [Description("M")]
        public long M17 { get; set; }

        [Required]
        [Description("M")]
        public long M18 { get; set; }

        [Required]
        [Description("M")]
        public long M19 { get; set; }

        [Required]
        [Description("M")]
        public long M20 { get; set; }

        [Required]
        [Description("M")]
        public long M21 { get; set; }

        [Required]
        [Description("M")]
        public long M22 { get; set; }

        [Required]
        [Description("M")]
        public long M23 { get; set; }

        [Required]
        [Description("M")]
        public long M24 { get; set; }

        [Required]
        [Description("M")]
        public long M25 { get; set; }

        [Required]
        [Description("M")]
        public long M26 { get; set; }

        [Required]
        [Description("M")]
        public long M27 { get; set; }

        [Required]
        [Description("M")]
        public long M28 { get; set; }

        [Required]
        [Description("M")]
        public long M29 { get; set; }

        [Required]
        [Description("M")]
        public long M30 { get; set; }

        [Required]
        [Description("M")]
        public long M31 { get; set; }

        [Required]
        [Description("M")]
        public long M32 { get; set; }

        [Required]
        [Description("M")]
        public long M33 { get; set; }

        [Description("M")]
        public long M34 { get; set; }

        [Description("M")]
        public long M35 { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }
    }
}
