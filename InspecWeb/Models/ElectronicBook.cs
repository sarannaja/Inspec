using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("ElectronicBooks")]
    [Description("ตารางเชื่อมนโยบาลกลาง")]
    public class ElectronicBook
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }


        //[ForeignKey("SubjectCentralPolicyProvince")]
        //[Description("FK: SubjectCentralPolicyProvince")]
        //public long SubjectCentralPolicyProvinceId { get; set; }
        //public virtual SubjectCentralPolicyProvince SubjectCentralPolicyProvince { get; set; }
        public string CreatedBy { get; set; }
        //public virtual ApplicationUser User { get; set; }

        [Description("ผลการจตรวจ")]
        public string Detail { get; set; }
        //public ICollection<CentralPolicyUser> CentralPolicyUsers { get; set; }

        [Description("ปัญหา")]
        public string Problem { get; set; }


        [Description("คำแนะนำ")]
        public string Suggestion { get; set; }

        [Required]
        [Description("สถานะ")]
        public string Status { get; set; }

        public ICollection<ElectronicBookFile> ElectronicBookFiles { get; set; }
        public ICollection<ElectronicBookSuggestGroup> ElectronicBookSuggestGroups { get; set; }
    }
}
