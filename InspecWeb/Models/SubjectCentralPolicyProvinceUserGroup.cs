﻿using System;
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

        [ForeignKey("SubquestionCentralPolicyProvince")]
        [Description("FK: ประเด็น")]
        public long SubquestionCentralPolicyProvinceId { get; set; }
        public virtual SubquestionCentralPolicyProvince SubquestionCentralPolicyProvince { get; set; }

        [ForeignKey("User")]
        [Description("FK: User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}