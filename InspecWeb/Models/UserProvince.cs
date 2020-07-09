using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    public class UserProvince
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("ApplicationUser")]
        [Description("FK: User")]
        public string UserID { get; set; }

        public virtual ApplicationUser User { get; set; }

        [ForeignKey("Province")]
        [Description("FK: จังหวัด")]
        public long ProvinceId { get; set; }
        public virtual Province Province { get; set; }
    }
   
 
}
