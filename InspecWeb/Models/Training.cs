using System;
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
    [Table("Trainings")]
    [Description("ตารางหลักสูตรการอบรม")]
    public class Training
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [Required]
        [Description("ชื่อหลักสูตรการอบรม")]
        public string Name { get; set; }

        [Required]
        [Description("คำอธิบายหลักสูตรการอบรม")]
        public string Detail { get; set; }

        [Description("สถานที่จัด")]
        public string Location { get; set; }

        [Description("รุ่น")]
        public int Generation { get; set; }

        [Description("ปี")]
        public int Year { get; set; }

        [Description("รหัสหลักสูตร")]
        public int CourseCode { get; set; }

        [Description("วันที่เริ่ม")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Description("วันที่สิ้นสุด")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Description("วันที่เริ่มสมัคร")]
        [DataType(DataType.Date)]
        public DateTime RegisStartDate { get; set; }

        [Description("วันที่สิ้นสุดสมัคร")]
        [DataType(DataType.Date)]
        public DateTime RegisEndDate { get; set; }

        [Required]
        [Description("รูปภาพ")]
        public string Image { get; set; }

        [Description("สถานะ 1:ใช้งาน, 2:ปิดใช้งาน")]
        public int Status { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

        public ICollection<TrainingSurvey> TrainingSurveys { get; set; }

    }
}