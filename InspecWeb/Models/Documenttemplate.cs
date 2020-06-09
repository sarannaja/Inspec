using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("Documenttemplates")]
    [Description("Document Template ของแบบขออนุมัติเดินทางไปราชการ แบบขอยืมเงินทดรองราชการ สัญญาขอยืมเงิน และแบบรายงานการเดินทางไปราชการ ฯลฯ")]
    public class Documenttemplate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [Required]
        [Description("ปี")]
        public string Year { get; set; }

        [Required]
        [Description("หัวเรื่อง")]
        public string Title { get; set; }

        [Required]
        [Description("ชื่อไฟล์")]
        public string File { get; set; }
       
    }
}
