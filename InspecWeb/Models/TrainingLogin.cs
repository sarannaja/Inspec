using System;
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

        [Required]
        [Description("หมายเลขหลักสูตร")]
        public long TrainingPhaseId { get; set; }

        [Description("วันที่ลงทะเบียน")]
        [DataType(DataType.Date)]
        public DateTime? RegisterDate { get; set; }

        [Required]
        [Description("ช่วงการอบรม")]
        public long TrainingProgramLoginId { get; set; }

    }
}