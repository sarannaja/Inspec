using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InspecWeb.Models
{
    public class CentralDepartmentProvince
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("CentralDepartment")]
        [Description("FK: CentralDepartment")]
        public long CentralDepartmentID { get; set; }

        public virtual CentralDepartment CentralDepartment { get; set; }

        [ForeignKey("Province")]
        [Description("FK: จังหวัด")]
        public long ProvinceId { get; set; }

        public virtual Province Province { get; set; }
    }


}
