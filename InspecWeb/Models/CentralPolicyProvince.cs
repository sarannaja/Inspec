﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("CentralPolicyProvinces")]
    [Description("ตารางความสัมพันธ์กำหนดการตรวจราชการประจำปี")]
    public class CentralPolicyProvince
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("CentralPolicy")]
        [Description("FK: แผนการตรวจ")]
        public long CentralPolicyId { get; set; }
        public virtual CentralPolicy CentralPolicy { get; set; }

        [ForeignKey("Province")]
        [Description("FK: จังหวัด")]
        public long ProvinceId { get; set; }
        public virtual Province Province { get; set; }
    }
}