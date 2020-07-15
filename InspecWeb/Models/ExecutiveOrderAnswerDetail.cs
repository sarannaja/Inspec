using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    
    [Table("ExecutiveOrderAnswerDetail")]
    [Description("ตารางการตอบกลับข้อสั่งการผู้บริหาร")]
    public class ExecutiveOrderAnswerDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("ExecutiveOrderAnswer")]
        [Description("FK: ข้อสั่งการ")]
        public long ExecutiveOrderAnswerId { get; set; }

        public virtual ExecutiveOrderAnswer ExecutiveOrderAnswer { get; set; }
  
        [Description("รายละเอียดของผู้ตอบ")]
        public string Answerdetail { get; set; }

        [Description("ปัญหา/อุปสรรค")]
        public string AnswerProblem { get; set; }

        [Description("ข้อเสนอแนะ")]
        public string AnswerCounsel { get; set; }
             
        [Description("publics")]
        public long publics { get; set; }
        [Description("วันที่เพิ่ม")]
        [DataType(DataType.Date)]
        public DateTime? create_at { get; set; }

        public ICollection<AnswerExecutiveOrderFile> AnswerExecutiveOrderFiles { get; set; }

    }
}