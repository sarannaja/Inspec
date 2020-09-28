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
    [Table("TrainingLecturerJoinSurveys")]
    [Description("ตารางผูกวิทยากรกับเรื่องประเมินการอบรม")]
    public class TrainingLecturerJoinSurvey
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("TrainingSurveyTopic")]
        [Description("FK: เรื่องหลักสูตรอบรม")]
        public long TrainingSurveyTopicId { get; set; }
        public virtual TrainingSurveyTopic TrainingSurveyTopic { get; set; }


        [ForeignKey("TrainingLecturer")]
        [Description("FK: วิทยากร")]
        public long LecturerId { get; set; }
        public virtual TrainingLecturer TrainingLecturer { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

    }
}