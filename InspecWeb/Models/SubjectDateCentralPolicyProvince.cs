using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("SubjectDateCentralPolicyProvinces")]
    [Description("ตารางประเด็นเวลา")]
    public class SubjectDateCentralPolicyProvince
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("SubjectCentralPolicyProvince")]
        [Description("FK: ประเด็น")]
        public long SubjectCentralPolicyProvinceId { get; set; }

        public virtual SubjectCentralPolicyProvince SubjectCentralPolicyProvince { get; set; }

        [ForeignKey("CentralPolicyDateProvince")]
        [Description("FK: ปีงบประมาณ")]
        public long CentralPolicyDateProvinceId { get; set; }

        public virtual CentralPolicyDateProvince CentralPolicyDateProvince { get; set; }
    }
}
