using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InspecWeb.Models
{
    [Table("TrainingProjectReportPracticeGuideFiles")]
    [Description("ตารางไฟล์รายงานโครงการฝึกอบรม - รายงานการฝึกปฏิบัติ")]
    public class TrainingProjectReportPracticeGuideFile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("TrainingProjectReport")]
        [Description("FK: รายงานย้อนหลัง")]
        public long TrainingProjectReportId { get; set; }

        public virtual TrainingProjectReport TrainingProjectReport { get; set; }

        [Required]
        [Description("ชื่อไฟล์")]
        public string Name { get; set; }

        [Description("ประเภท")]
        public string Type { get; set; }

        [Description("คำอธิบายรูปภาพ")]
        public string Description { get; set; }
    }
}
