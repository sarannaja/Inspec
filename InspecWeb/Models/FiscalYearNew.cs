﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace InspecWeb.Models
{
    /// <summary>
    /// เนื้อหาของสื่อสิ่งพิมพ์
    /// </summary>
    [Table("FiscalYearNew")]
    [Description("ตารางปีงบประมาณ")]

    public class FiscalYearNew
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [Required]
        [Description("ชื่อปีงบประมาณ")]
        public int Year { get; set; }


        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }


        [Description("วันที่เริ่ม")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Description("วันที่สิ้นสุด")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public ICollection<CentralPolicy> CentralPolicies { get; set; }

    }
}
