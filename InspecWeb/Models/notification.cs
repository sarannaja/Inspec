using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{

    [Table("Notification")]
    [Description("ตารางแจ้งเตือน")]
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("ApplicationUser")]
        [Description("FK: User")]
        public string UserID { get; set; }

        public virtual ApplicationUser User { get; set; }

        [ForeignKey("CentralPolicy")]
        [Description("FK: นโยบายกลาง")]
        public long CentralPolicyId { get; set; }
        public virtual CentralPolicy CentralPolicy { get; set; }

        [Description("เรื่องจากจังหวัดไหน")]
        public long ProvinceId { get; set; }
        
        [Description("สถานะ")]
        public long status { get; set; }

        [Description("การแจ้งเตือน")]
        public long noti { get; set; }

        [Description("เรื่องคำร้อง")]
        public long xe { get; set; }

        [Description("วันที่ส่งแจ้งเตือน")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

        [Description("วันที่ดูแจ้งเตือน")]
        [DataType(DataType.Date)]
        public DateTime? ActiveDate { get; set; }

    }
}
