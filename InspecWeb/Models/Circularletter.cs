using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    
    [Table("Circularletter")]
    [Description("กฎหมาย ระเบียบ หนังสือเวียนต่าง ๆ")]
    public class Circularletter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }    

        [Required]
        [Description("ชื่อ")]
        public string Title { get; set; }

        [Required]
        [Description("ไฟล์")]
        public string Filename { get; set; }

        [Description("วันที่สร้าง")]    
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }
    }
}
