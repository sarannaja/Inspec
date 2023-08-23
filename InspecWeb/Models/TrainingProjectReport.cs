using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("TrainingProjectReports")]
    [Description("ตารางรายงานโครงการฝึกอบรม")]
    public class TrainingProjectReport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("User")]
        [Description("FK: User")]
        public string CreatedBy { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Description("ปีงบประมาณ")]
        public string Year { get; set; }
        //public ICollection<CentralPolicyUser> CentralPolicyUsers { get; set; }

        [Description("ประเภทหลักสูตร")]
        public string courseType { get; set; }

        public ICollection<TrainingProjectReportFile> TrainingProjectReportFiles { get; set; }
        public ICollection<TrainingProjectReportModelDirectoryFile> TrainingProjectReportModelDirectoryFiles { get; set; }
        public ICollection<TrainingProjectReportPracticeGuideFile> TrainingProjectReportPracticeGuideFiles { get; set; }
        public ICollection<TrainingProjectReportProjectDocumentFile> TrainingProjectReportProjectDocumentFiles { get; set; }
        public ICollection<TrainingProjectReportTrainingDetailFile> TrainingProjectReportTrainingDetailFiles { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }
    }
}
