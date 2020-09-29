using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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


        [ForeignKey("RequestOrderAnswerDetail")]
        [Description("FK: ข้อสั่งการ")]
        public long RequestOrderAnswerDetailId { get; set; }

        public virtual RequestOrderAnswerDetail RequestOrderAnswerDetail { get; set; }

        [Required]
        [Description("ชื่อไฟล์")]
        public string Name { get; set; }

    }
}
