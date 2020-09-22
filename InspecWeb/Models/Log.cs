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
    [Table("Logs")]
    [Description("Log")]
    public class Log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("UserId")]
        [Description("User")]
        public virtual ApplicationUser User { get; set; }
        public string UserId { get; set; }

        public string DatabaseName { get; set; }
        public string EventType { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EventDate { get; set; }
        public string Detail { get; set; }

        public long Allid { get; set; }

        // public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
