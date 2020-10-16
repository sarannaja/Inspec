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
    [Table("FiscalYear")]
    [Description("ตารางปีงบประมาณ")]

    public class FiscalYear
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [Required]
        [Description("คำสั่งเขตตรวจ")]
        public string Year { get; set; }

        [Description("ลงวันที่คำสั่ง")]
        [DataType(DataType.Date)]
        public DateTime? Orderdate { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

        [Description("วันที่แก้ไข")]
        [DataType(DataType.Date)]
        public DateTime? UpdateAt { get; set; }

        [Description("วันที่เริ่ม")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Description("วันที่สิ้นสุด")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Required]
        [Description("active")]
        public long Active { get; set; }

        //public ICollection<CentralPolicy> CentralPolicies { get; set; }
        public ICollection<FiscalYearRelation> FiscalYearRelations { get; set; }

        public ICollection<SetinspectionareaFile> SetinspectionareaFiles { get; set; }

    }
}
