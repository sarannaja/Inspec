﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("ElectronicBookGroups")]
    [Description("ตารางเชื่อมนโยบาลกลาง")]
    public class ElectronicBookGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("ElectronicBook")]
        [Description("FK: ElectronicBook")]
        public long ElectronicBookId { get; set; }
        public virtual ElectronicBook ElectronicBook { get; set; }

        [ForeignKey("CentralPolicyProvince")]
        [Description("FK: จังหวัด")]
        public long CentralPolicyProvinceId { get; set; }
        public virtual CentralPolicyProvince CentralPolicyProvince { get; set; }

        //public ICollection<CentralPolicyUser> CentralPolicyUsers { get; set; }
        //public ICollection<CentralPolicyUserFile> CentralPolicyUserFiles { get; set; }
    }
}