using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("ElectronicBookOtherAccepts")]
    [Description("ตารางเชื่อมนโยบาลกลาง")]
    public class ElectronicBookOtherAccept
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("ElectronicBookAccept")]
        [Description("FK: ElectronicBookAccept")]
        public long ElectronicBookAcceptId { get; set; }
        public virtual ElectronicBookAccept ElectronicBookAccept { get; set; }

        [ForeignKey("User")]
        [Description("FK: หน่วยรับตรวจอื่นที่มารับทราบ")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Description("วันที่ส่งให้หน่วยรับตรวจอื่น")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

        [Description("วันที่รับทราบ")]
        [DataType(DataType.Date)]
        public DateTime? AcceptDate { get; set; }

        [ForeignKey("ProvincialDepartment")]
        [Description("FK: หน่วยรับตรวจ")]
        public long ProvincialDepartmentId { get; set; }
        public virtual ProvincialDepartment ProvincialDepartment { get; set; }

        [Description("คำอธิบาย")]
        public string Description { get; set; }

        [Description("สถานะ")]
        public string Status { get; set; }

        [ForeignKey("UserCreate")]
        [Description("FK: UserCreate")]
        public string CreateBy { get; set; }
        public virtual ApplicationUser UserCreate { get; set; }

        [Description("คำอธิบายหน่วยรับตรวจอื่น")]
        public string OtherDescription { get; set; }
    }
}
