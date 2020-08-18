using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("SubjectGroupPeopleQuestions")]
    [Description("ตารางเชื่อมนโยบาลกลาง")]
    public class SubjectGroupPeopleQuestion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("SubjectGroup")]
        [Description("FK: SubjectGroup")]
        public long SubjectGroupId { get; set; }
        public virtual SubjectGroup SubjectGroup { get; set; }


        [ForeignKey("CentralPolicyEvent")]
        [Description("FK: CentralPolicyEvent")]
        public long CentralPolicyEventId { get; set; }
        public virtual CentralPolicyEvent CentralPolicyEvent { get; set; }
        
    }
}
