using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("SubquestionGroups")]
    [Description("ตารางเชื่อมนโยบาลกลาง")]
    public class SubquestionGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("CentralPolicyProvince")]
        [Description("FK: จังหวัด")]
        public long CentralPolicyProvinceId { get; set; }
        public virtual CentralPolicyProvince CentralPolicyProvince { get; set; }

        [ForeignKey("Subquestion")]
        [Description("FK: คำถามย่อย")]
        public long SubquestionId { get; set; }
        public virtual Subquestion Subquestion { get; set; }

        [ForeignKey("ProvincialDepartment")]
        [Description("FK: ProvincialDepartment")]
        public long ProvincialDepartmentId { get; set; }
        public virtual ProvincialDepartment ProvincialDepartment { get; set; }
    }
}
