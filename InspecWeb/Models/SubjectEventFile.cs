using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InspecWeb.Models
{
    [Table("SubjectEventFiles")]
    [Description("ตารางไฟล์สมุดตรวจ")]
    public class SubjectEventFile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("SubjectGroup")]
        [Description("FK: สมุดตรวจ")]
        public long SubjectGroupId { get; set; }
        public virtual SubjectGroup SubjectGroup { get; set; }

        //[ForeignKey("InspectionPlanEvent")]
        //[Description("FK: สมุดตรวจ")]
        //public long InspectionPlanEventId { get; set; }
        //public virtual InspectionPlanEvent InspectionPlanEvent { get; set; }

        [Required]
        [Description("ชื่อไฟล์")]
        public string Name { get; set; }

        [Description("ประเภท")]
        public string Type { get; set; }

        [Description("คำอธิบายรูปภาพ")]
        public string Description { get; set; }
    }
}
