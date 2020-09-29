using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InspecWeb.Models
{
    [Table("SubjectCentralPolicyProvinces")]
    [Description("ตาราง")]
    public class SubjectCentralPolicyProvince
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("CentralPolicy")]
        [Description("FK: นโยบายกลาง")]
        public long CentralPolicyProvinceId { get; set; }
        public virtual CentralPolicyProvince CentralPolicyProvince { get; set; }

        [ForeignKey("SubjectGroup")]
        [Description("FK: นโยบายกลาง")]
        public long SubjectGroupId { get; set; }
        public virtual SubjectGroup SubjectGroup { get; set; }

        [Description("ชื่อประเด็น")]
        public string Name { get; set; }

        [Description("Master & NoMaster")]
        public string Type { get; set; }

        [Description("ร่าง , ใช้งานจริง")]
        public string Status { get; set; }

        [Description("คำชี้แจง")]
        public string Explanation { get; set; }
        //[Description("มอบหมายเขต , มอบหมายจังหวัด , มอบหมายหน่วยงาน")]
        //public string Step { get; set; }
        public long CheckAnswer { get; set; }

        //[Description("ลิ้ง")]
        //public string link { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

        [Required]
        [Description("คนที่สร้างนโยบายกลาง")]
        public string CreatedBy { get; set; }

        [Description("วันที่แก้ไข")]
        [DataType(DataType.Date)]
        public DateTime? UpdateAt { get; set; }

        public ICollection<SubjectDateCentralPolicyProvince> SubjectDateCentralPolicyProvinces { get; set; }
        public ICollection<SubquestionCentralPolicyProvince> SubquestionCentralPolicyProvinces { get; set; }
        //public ICollection<SubjectCentralPolicyProvinceGroup> SubjectCentralPolicyProvinceGroups { get; set; }
        // public ICollection<ElectronicBookSuggestGroup> ElectronicBookSuggestGroups { get; set; }
        public ICollection<SubjectCentralPolicyProvinceFile> SubjectCentralPolicyProvinceFiles { get; set; }
        public ICollection<SuggestionSubject> SuggestionSubjects { get; set; }
        public ICollection<AnswerSubquestionFile> AnswerSubquestionFiles { get; set; }
        public ICollection<AnswerSubquestionStatus> AnswerSubquestionStatuses { get; set; }

    }
}
