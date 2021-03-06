﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{

    [Table("TrainingLogin")]
    [Description("ตารางเข้าสู่ระบบหลักสูตร")]
    public class TrainingLogin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [Required]
        [Description("Username")]
        public string Username { get; set; }

        [Description("รหัสประจำตัวผู้ร่วมอบรม")]
        public String IDCode { get; set; }

        [Required]
        [ForeignKey("Training")]
        [Description("FK: ตารางหลักสูตรอบรม")]
        public long TrainingId { get; set; }

        [Description("วันที่ลงทะเบียน")]
        [DataType(DataType.Date)]
        public DateTime? RegisterDate { get; set; }

        [Required]
        [ForeignKey("TrainingProgramLoginQRCode")]
        [Description("รหัสวันที่ลงชื่ออบรม")]
        public long TrainingProgramLoginId { get; set; }
        public virtual TrainingProgramLoginQRCode TrainingProgramLoginQRCodes { get; set; }

        [Required]
        [Description("ช่วงเช้า/บ่าย")]
        public long DateType { get; set; }

    }
}