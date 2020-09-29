using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InspecWeb.Models
{
    [Table("SubjectGroups")]
    [Description("ตารางเชื่อมนโยบาลกลาง")]
    public class SubjectGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("CentralPolicy")]
        [Description("FK: แผนการตรวจ")]
        public long CentralPolicyId { get; set; }
        public virtual CentralPolicy CentralPolicy { get; set; }

        [ForeignKey("Province")]
        [Description("FK: จังหวัด")]
        public long ProvinceId { get; set; }
        public virtual Province Province { get; set; }

        [ForeignKey("ProvincialDepartment")]
        [Description("FK: หน่วยงานส่วนภูมิถาค")]
        public long ProvincialDepartmentIdCreatedBy { get; set; }
        public virtual ProvincialDepartment ProvincialDepartment { get; set; }

        [ForeignKey("User")]
        [Description("FK: User")]
        public string CreatedBy { get; set; }
        public virtual ApplicationUser User { get; set; }

        public long RoleCreatedBy { get; set; }

        [Description("ร่าง , ใช้งานจริง")]
        public string Status { get; set; }
        public string Type { get; set; }
        public string Land { get; set; }

        public string Suggestion { get; set; }

        [Description("วันที่เริ่ม")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Description("วันที่สิ้นสุด")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Description("วันที่เริ่ม")]
        [DataType(DataType.Date)]
        public DateTime? SubjectNotificationDate { get; set; }

        [Description("วันที่สิ้นสุด")]
        [DataType(DataType.Date)]
        public DateTime? SubjectDeadlineDate { get; set; }

        [Description("วันที่เริ่ม")]
        [DataType(DataType.Date)]
        public DateTime? PeopleQuestionNotificationDate { get; set; }

        [Description("วันที่สิ้นสุด")]
        [DataType(DataType.Date)]
        public DateTime? PeopleQuestionDeadlineDate { get; set; }
        public ICollection<SubjectCentralPolicyProvince> SubjectCentralPolicyProvinces { get; set; }
        public ICollection<SubjectGroupPeopleQuestion> SubjectGroupPeopleQuestions { get; set; }
        public ICollection<AnswerRecommenDationInspector> AnswerRecommenDationInspectors { get; set; }

        public ICollection<SubjectEventFile> SubjectEventFiles { get; set; }
    }
}
