using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("AnswerExecutiveOrderFiles")]
    [Description("ตารางไฟล์บันทึกการดำเนินการตามข้อสั่งการ")]
    public class AnswerExecutiveOrderFile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("ExecutiveOrder")]
        [Description("FK: ข้อสั่งการ")]
        public long ExecutiveOrderId { get; set; }

        public virtual ExecutiveOrder ExecutiveOrder { get; set; }

        [Required]
        [Description("ชื่อไฟล์")]
        public string Name { get; set; }

    }
}
