using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("SubquestionCentralPolicyProvinces")]
    [Description("ตาราง")]
    public class SubquestionCentralPolicyProvince
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("SubjectCentralPolicyProvince")]
        [Description("FK: ประเด็น")]
        public long SubjectCentralPolicyProvinceId { get; set; }

        public virtual SubjectCentralPolicyProvince SubjectCentralPolicyProvince { get; set; }

        [Required]
        [Description("ชื่อคำถามย่อย")]
        public string Name { get; set; }

        [Required]
        [Description("ประเภทของคำถามย่อย")]
        public string Type { get; set; }

        public ICollection<SubquestionChoiceCentralPolicyProvince> SubquestionChoiceCentralPolicyProvinces { get; set; }
        public ICollection<SubjectCentralPolicyProvinceGroup> SubjectCentralPolicyProvinceGroups { get; set; }

    }
}
