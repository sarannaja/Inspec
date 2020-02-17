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
        [Description("ชื่อปีงบประมาณ")]
        public int Year { get; set; }


        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

        public ICollection<CentralPolicy> CentralPolicies { get; set; }
        //public ICollection<District> Districts { get; set; }
    }
}
