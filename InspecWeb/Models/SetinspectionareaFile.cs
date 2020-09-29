using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InspecWeb.Models
{
    [Table("SetinspectionareaFiles")]
    [Description("ตารางไฟล์ดำเนินการตามคำร้องขอ")]
    public class SetinspectionareaFile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }


        [ForeignKey("RequestOrderAnswerDetail")]
        [Description("FK: ข้อสั่งการ")]
        public long FiscalYearId { get; set; }

        public virtual FiscalYear FiscalYear { get; set; }

        [Required]
        [Description("ชื่อไฟล์")]
        public string Name { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

    }
}
