using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("ElectronicBookSuggestGroups")]
    [Description("ตารางเชื่อมนโยบาลกลาง")]
    public class ElectronicBookSuggestGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("ElectronicBook")]
        [Description("FK: ElectronicBook")]
        public long ElectronicBookId { get; set; }
        public virtual ElectronicBook ElectronicBook { get; set; }

        [ForeignKey("SubjectCentralPolicyProvince")]
        [Description("FK: SubjectCentralPolicyProvince")]
        public long SubjectCentralPolicyProvinceId { get; set; }
        public virtual SubjectCentralPolicyProvince SubjectCentralPolicyProvince { get; set; }
    }
}
