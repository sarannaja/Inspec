using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("SubjectCentralPolicyProvinceUserGroups")]
    [Description("ตารางเชื่อมนโยบาลกลาง")]
    public class SubjectCentralPolicyProvinceUserGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("SubjectCentralPolicyProvince")]
        [Description("FK: ประเด็น")]
        public long SubjectCentralPolicyProvinceId { get; set; }
        public virtual SubjectCentralPolicyProvince SubjectCentralPolicyProvince { get; set; }

        [ForeignKey("User")]
        [Description("FK: User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
