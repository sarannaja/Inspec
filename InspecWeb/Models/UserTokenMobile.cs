using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace InspecWeb.Models
{
    /// <summary>
    /// สำหรับแจ้งเตือนไปยัง
    /// </summary>
    [Table("UserTokenMobile")]
    [Description("สำหรับแจ้งเตือนไปยัง mobile")]
    public class UserTokenMobile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }

        public string Token { get; set; }
        public string Session { get; set; }
        public string DeviceType { get; set; }

    }
}