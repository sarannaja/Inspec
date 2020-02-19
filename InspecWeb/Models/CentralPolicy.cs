using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("CentralPolicies")]
    [Description("ตารางนโยบายกลาง")]
    public class CentralPolicy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("FiscalYear")]
        [Description("FK: ปีงบประมาณ")]
        public long FiscalYearId { get; set; }

        [Required]
        [Description("ชื่อนโยบายกลาง")]
        public string Title { get; set; }

        [Description("วันที่เริ่ม")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Description("วันที่สิ้นสุด")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

        [Required]
        [Description("คนที่สร้างนโยบายกลาง")]
        public string CreatedBy { get; set; }

        [Required]
        [Description("สถานะ")]
        public string Status { get; set; }

        public ICollection<Subject> Subjects { get; set; }
        public ICollection<CentralPolicyFile> CentralPolicyFiles { get; set; }
    }
}
