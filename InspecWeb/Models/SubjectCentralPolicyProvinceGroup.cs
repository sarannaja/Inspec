using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("SubjectCentralPolicyProvinceGroups")]
    [Description("ตารางเชื่อมนโยบาลกลาง")]
    public class SubjectCentralPolicyProvinceGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("SubjectCentralPolicyProvince")]
        [Description("FK: ประเด็น")]
        public long SubjectCentralPolicyProvinceId { get; set; }
        public virtual SubjectCentralPolicyProvince SubjectCentralPolicyProvince { get; set; }

        [ForeignKey("ProvincialDepartment")]
        [Description("FK: ประเด็น")]
        public long ProvincialDepartmentId { get; set; }
        public virtual ProvincialDepartment ProvincialDepartment { get; set; }
    }
}
