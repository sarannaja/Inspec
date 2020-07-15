using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    
    [Table("RequestOrderAnswerDetail")]
    [Description("ตารางการตอบกลับข้อสั่งการผู้บริหาร")]
    public class RequestOrderAnswerDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("RequestOrderAnswer")]
        [Description("FK: ข้อสั่งการ")]
        public long RequestOrderAnswerId { get; set; }

        public virtual RequestOrderAnswer RequestOrderAnswer { get; set; }
  
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

        public ICollection<AnswerRequestOrderFile> AnswerRequestOrderFiles { get; set; }

    }
}