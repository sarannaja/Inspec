using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("ElectronicBookProvincialDepartment")]
    [Description("ตารางหน่วยรับตรวจ")]
    public class ElectronicBookProvincialDepartment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("ElectronicBook")]
        [Description("FK: ElectronicBook")]
        public long ElectronicBookId { get; set; }
        public virtual ElectronicBook ElectronicBook { get; set; }

        // [ForeignKey("ProvincialDepartment")]
        // [Description("FK: ProvincialDepartment")]
        // public string ProvincialDepartmentId { get; set; }
        // public virtual ProvincialDepartment ProvincialDepartment { get; set; }

        [ForeignKey(" ProvincialDepartment")]
        [Description("FK: หน่วยรับตรวจ")]
        public long ProvincialDepartmentId { get; set; }
        public virtual ProvincialDepartment ProvincialDepartments { get; set; }
        public string Description { get; set; }

        public string Status { get; set; }
        public string Approve { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreateDate { get; set; }

        [ForeignKey("UserProvincialDepartment")]
        [Description("FK: UserProvincialDepartment")]
        public string UserId { get; set; }
        public virtual ApplicationUser UserProvincialDepartment { get; set; }

        [ForeignKey("UserCreate")]
        [Description("FK: UserCreate")]
        public string CreateBy { get; set; }
        public virtual ApplicationUser UserCreate { get; set; }

        public ICollection<ElectronicBookOtherAccept> ElectronicBookOtherAccepts { get; set; }
    }
}
