using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{

    [Table("Notificationcreateby")]
    [Description("ตารางคนสร้างแจ้งเตือน")]
    public class Notificationcreateby
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("Notification")]
        [Description("FK: แจ้งเตือน")]
        public long NotificationId { get; set; }
        public virtual Notification Notification { get; set; }

        [ForeignKey("UserCreate")]
        [Description("FK: UserCreate")]
        public string CreateBy { get; set; }
        public virtual ApplicationUser UserCreate { get; set; }

       

    }
}
