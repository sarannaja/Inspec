﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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

        [ForeignKey("CentralPolicyProvince")]
        [Description("FK: นโยบายกลาง")]
        public long CentralPolicyProvinceId { get; set; }

        public virtual CentralPolicyProvince CentralPolicyProvince { get; set; }


        [Description("ชื่อประเด็น")]
        public string Name { get; set; }

        public ICollection<SubjectDateCentralPolicyProvince> SubjectDateCentralPolicyProvinces { get; set; }
        public ICollection<SubquestionCentralPolicyProvince> SubquestionCentralPolicyProvinces { get; set; }
    }
}