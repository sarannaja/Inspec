using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    /// <summary>
    /// เนื้อหาของสื่อสิ่งพิมพ์
    /// </summary>
    [Table("ExecutiveOrders")]
    [Description("ตารางข้อสั่งการผู้บริหาร")]
    public class ExecutiveOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("ApplicationUser")]
        [Description("FK: User")]
        public string UserID { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        [Description("ประเด็น/เรื่อง")]
        public string Subject { get; set; }

        [Required]
        [Description("รายละเอียดประเด็น/เรื่อง")]
        public string Subjectdetail { get; set; }
       
        [Description("วันที่สั่งการ")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

        [Description("วันที่มีข้อสั่งการ")]
        [DataType(DataType.Date)]
        public DateTime? Commanded_date { get; set; }
   
        [Description("publics")]
        public long publics { get; set; }

        public ICollection<ExecutiveOrderFile> ExecutiveOrderFiles { get; set; }

        public ICollection<ExecutiveOrderAnswer> ExecutiveOrderAnswers { get; set; }
    }
}