using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InspecWeb.Models
{

    [Table("ExportReportHead")]
    [Description("ตาราง Export ทะเบียนรายงาน")]
    public class ExportReportHead
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("Province")]
        [Description("FK: จังหวัด")]
        public long ProvinceId { get; set; }
        public virtual Province Province { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string Ministry { get; set; }
        public string Status { get; set; }
    }
}
