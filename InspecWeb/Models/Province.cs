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
    [Table("Provinces")]
    [Description("ตารางจังหวัด")]
    public class Province
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [Required]
        [Description("ชื่อจังหวัด")]
        public string Name { get; set; }

        [Required]
        [Description("วันที่สร้าง")]
        public DateTime CreatedAt { get; set; }

        public ICollection<District> Districts { get; set; }
    }
}