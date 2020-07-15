using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    
    [Table("ExecutiveOrderAnswers")]
    [Description("ตารางการตอบกลับข้อสั่งการผู้บริหาร")]
    public class ExecutiveOrderAnswer
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
        [Description("สถานะ")]
        public string Status { get; set; }

        [ForeignKey("ApplicationUser")]
        [Description("FK: User")]
        public string UserID { get; set; }

        public virtual ApplicationUser User { get; set; }
          
        [Description("วันที่รับทราบข้อสั่งการ")]
        [DataType(DataType.Date)]
        public DateTime? beaware_date { get; set; }

        [Description("publics")]
        public long publics { get; set; }

        public ICollection<ExecutiveOrderAnswerDetail> ExecutiveOrderAnswerDetails { get; set; }
    }
}