using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("AnswerRequestOrderFiles")]
    [Description("ตารางไฟล์ดำเนินการตามคำร้องขอ")]
    public class AnswerRequestOrderFile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("RequestOrder")]
        [Description("FK: คำร้องขอ")]
        public long RequestOrderId { get; set; }

        public virtual RequestOrder RequestOrder { get; set; }

        [Required]
        [Description("ชื่อไฟล์")]
        public string Name { get; set; }

    }
}
