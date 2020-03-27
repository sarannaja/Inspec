using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    public class ProvincialDepartmentProvince
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("ProvincialDepartment")]
        [Description("FK: ProvincialDepartment")]
        public long ProvincialDepartmentID { get; set; }

        public virtual ProvincialDepartment ProvincialDepartment { get; set; }

        [ForeignKey("Province")]
        [Description("FK: จังหวัด")]
        public long ProvinceId { get; set; }

        public virtual Province Province { get; set; }
    }
   
 
}
