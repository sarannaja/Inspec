using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InspecWeb.Models
{
    [Table("AnswerExecutiveOrderFiles")]
    [Description("")]
    public class AnswerExecutiveOrderFile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("ExecutiveOrderAnswerDetail")]
        [Description("FK: ข้อสั่งการ")]
        public long ExecutiveOrderAnswerDetailId { get; set; }

        public virtual ExecutiveOrderAnswerDetail ExecutiveOrderAnswerDetail { get; set; }

        [Required]
        [Description("ชื่อไฟล์")]
        public string Name { get; set; }

    }
}
