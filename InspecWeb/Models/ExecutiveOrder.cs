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
    [Table("ExecutiveOrders")]
    [Description("ตารางข้อสั่งการผู้บริหาร")]
    public class ExecutiveOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [Required]
        [Description("คำสั่ง")]
        public string DetailExecutiveOrder { get; set; }

        [ForeignKey("CentralPolicy")]
        [Description("FK: แผนการตรวจ")]
        public long CentralPolicyId { get; set; }

        public virtual CentralPolicy CentralPolicy { get; set; }

        [ForeignKey("ApplicationUser")]
        [Description("FK: ผู้สั่งการ")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }


        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

       
    }
}